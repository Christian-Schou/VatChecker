using VatChecker.Application.Interfaces;
using VatChecker.Application.Models.Vies;

namespace VatChecker.Infrastructure.Services.Vies;

public class ViesService : IViesService
{
    public Task<ViesVatValidationResponseModel> ValidateVatNumberWithoutRequestingVatNumberAsync(ViesValidationRequestModel validationRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ViesVatValidationResponseModel> ValidateVatNumberWithRequestingVatNumberAsync(ViesValidationRequestModel validationRequest)
    {
        throw new NotImplementedException();
    }
}