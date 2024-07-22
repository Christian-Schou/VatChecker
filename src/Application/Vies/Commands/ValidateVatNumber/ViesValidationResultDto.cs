using VatChecker.Application.Models.Vies;

namespace VatChecker.Application.Vies.Commands.ValidateVatNumber;

/// <summary>
///     A DTO representing a VIES Validation Response.
/// </summary>
public class ViesValidationResultDto
{
    /// <summary>
    ///     Gets or inits the value indicating if the VAT no. is valid.
    /// </summary>
    public bool IsValid { get; init; }
    
    /// <summary>
    ///     Gets or inits the request date for the validation.
    /// </summary>
    public DateTime RequestDate { get; init; }
    
    /// <summary>
    ///     Gets or inits any included response message associated with the validation.
    /// </summary>
    public string? Message { get; init; }
    
    /// <summary>
    ///     Gets or inits the company name for the validated VAT number.
    /// </summary>
    public string? Company { get; init; }
    
    /// <summary>
    ///     Gets or inits the company address for the validated VAT number.
    /// </summary>
    public string? Address { get; init; }
    
    /// <summary>
    ///     Gets or inits the request identifier associated with the validation task.
    ///     This value is returned from VIES and can be used as proof of validation, if asked.
    /// </summary>
    public string? RequestIdentifier { get; init; }
    
    /// <summary>
    ///     Gets or inits the VAT number returned from VIES for the validation.
    /// </summary>
    public string? VatNumber { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ViesVatValidationResponseModel, ViesValidationResultDto>()
                .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => src.IsValid))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.UserError))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.RequestIdentifier, opt => opt.MapFrom(src => src.RequestIdentifier))
                .ForMember(dest => dest.VatNumber, opt => opt.MapFrom(src => src.VatNumber));
        }
    }
}