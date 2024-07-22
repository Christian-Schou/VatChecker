using VatChecker.Application.Vies.Commands.ValidateVatNumber;

namespace VatChecker.Application.Models.Vies;

public class ViesValidationRequestModel
{
    /// <summary>
    ///     Gets or inits the country code for the vat no. to validate.
    /// </summary>
    public string Country { get; init; }
    
    /// <summary>
    ///     Gets or inits the vat. no to validate.
    /// </summary>
    public string VatNumberToValidate { get; init; }

    /// <summary>
    ///     Gets or inits the requesting validator country, if any is represented.
    /// </summary>
    /// <remarks>This is optional.</remarks>
    public string? RequestingValidatorCountry { get; init; }

    /// <summary>
    ///     Gets or inits the requesting validator vat. no., if any is represented.
    /// </summary>
    /// <remarks>This is optional.</remarks>
    public string? RequestingValidatorVatNumber { get; init; }

    private class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<ValidateVatNumberCommand, ViesValidationRequestModel>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.VatNumberToValidate, opt => opt.MapFrom(src => src.VatNumberToValidate))
                .ForMember(dest => dest.RequestingValidatorCountry, opt => opt.MapFrom(src => src.RequestingValidatorCountry))
                .ForMember(dest => dest.RequestingValidatorVatNumber, opt => opt.MapFrom(src => src.RequestingValidatorVatNumber));
        }
    }
}