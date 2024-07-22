using VatChecker.Application.Models.Vies;

namespace VatChecker.Application.Interfaces;

public interface IViesService
{
    Task<ViesVatValidationResponseModel> ValidateVatNumberWithoutRequestingVatNumberAsync(ViesValidationRequestModel validationRequest);
    Task<ViesVatValidationResponseModel> ValidateVatNumberWithRequestingVatNumberAsync(ViesValidationRequestModel validationRequest);
}