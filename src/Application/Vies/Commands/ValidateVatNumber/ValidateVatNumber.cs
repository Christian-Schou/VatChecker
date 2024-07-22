using VatChecker.Application.Interfaces;
using VatChecker.Application.Models.Vies;

namespace VatChecker.Application.Vies.Commands.ValidateVatNumber;

public record ValidateVatNumberCommand : IRequest<ViesValidationResultDto>
{
    public string Country { get; init; }
    
    public string VatNumberToValidate { get; init; }

    public string? RequestingValidatorCountry { get; init; }

    public string? RequestingValidatorVatNumber { get; init; }
}

public class ValidateVatNumberCommandValidator : AbstractValidator<ValidateVatNumberCommand>
{
    public ValidateVatNumberCommandValidator()
    {
        RuleFor(v => v.VatNumberToValidate)
            .NotEmpty().WithMessage("The VAT number to validate cannot be empty.")
            .NotNull().WithMessage("The VAT number to validate cannot be null.")
            .MinimumLength(4).WithMessage("The VAT number must be greater than 4 characters.")
            .MaximumLength(15).WithMessage("The Vat number cannot be greater than 15 characters.");

        RuleFor(v => v.Country)
            .NotEmpty().WithMessage("The Vat Number country to validate cannot be empty.")
            .NotNull().WithMessage("The Vat Number country to validate cannot be null.")
            .Length(2).WithMessage("The VAT number country code must be only 2 characters.");

        RuleFor(v => v.RequestingValidatorCountry)
            .NotEmpty().When(v => !string.IsNullOrEmpty(v.RequestingValidatorVatNumber)).WithMessage("The country code has to be supplied when a requesting VAT no. is supplied.")
            .Length(2).When(v => !string.IsNullOrEmpty(v.RequestingValidatorVatNumber)).WithMessage("The VAT number country code must be only 2 characters.");
    }
}

public class ValidateVatNumberCommandHandler(IViesService viesService, IMapper mapper)
    : IRequestHandler<ValidateVatNumberCommand, ViesValidationResultDto>
{
    public async Task<ViesValidationResultDto> Handle(ValidateVatNumberCommand request, CancellationToken cancellationToken)
    {
        var viesValidationRequest = mapper.Map<ViesValidationRequestModel>(request);
        
        ViesVatValidationResponseModel? viesResult;
        
        if (!string.IsNullOrEmpty(request.RequestingValidatorVatNumber))
        {
            Guard.Against.NullOrWhiteSpace(request.RequestingValidatorCountry);
            viesResult = await viesService.ValidateVatNumberWithRequestingVatNumberAsync(viesValidationRequest);
        }
        else
        {
            viesResult = await viesService.ValidateVatNumberWithoutRequestingVatNumberAsync(viesValidationRequest);
        }
        
        return mapper.Map<ViesValidationResultDto>(viesResult);
    }
}
