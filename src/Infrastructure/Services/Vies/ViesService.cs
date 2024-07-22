using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using VatChecker.Application.Interfaces;
using VatChecker.Application.Models.Vies;

namespace VatChecker.Infrastructure.Services.Vies;

public class ViesService(IConfiguration configuration) : IViesService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<ViesVatValidationResponseModel?> ValidateVatNumberWithoutRequestingVatNumberAsync(ViesValidationRequestModel validationRequest)
    {
        var validationPath = $"{validationRequest.Country.ToUpper()}/vat/{validationRequest.VatNumberToValidate}";
        return await RequestViesValidationAsync(validationPath);
    }

    public async Task<ViesVatValidationResponseModel?> ValidateVatNumberWithRequestingVatNumberAsync(ViesValidationRequestModel validationRequest)
    {
        var validationPath = $"{validationRequest.Country}/vat/{validationRequest.VatNumberToValidate}?requesterMemberStateCode={validationRequest.RequestingValidatorCountry!.ToUpper()}&requesterNumber={validationRequest.RequestingValidatorVatNumber}";
        return await RequestViesValidationAsync(validationPath);
    }
    
    /// <summary>
    /// Request VIES REST API to validate vat-number with or without requesting vat number
    /// </summary>
    /// <param name="urlRequest">Path to request. Will be prepended to base URL</param>
    /// <returns>Vies Validation Result</returns>
    private async Task<ViesVatValidationResponseModel?> RequestViesValidationAsync(string urlRequest)
    {
        using var client = new HttpClient();

        var host = configuration["ViesSettings:Host"];
        Guard.Against.NullOrWhiteSpace(host);
        
        client.BaseAddress = new Uri(host);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var response = await client.GetAsync(urlRequest);
        if (!response.IsSuccessStatusCode) return null;
        
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ViesVatValidationResponseModel>(responseContent, JsonOptions);
    }
}