namespace VatChecker.Application.Models.Vies;

public class ViesVatValidationResponseModel
{
    /// <summary>
    ///     Gets or sets the value indicating if this VAT number is valid.
    /// </summary>
    public bool IsValid { get; set; }
    
    /// <summary>
    ///     Gets or sets the request date for the validation.
    /// </summary>
    public DateTime RequestDate { get; set; }
    
    /// <summary>
    ///     Gets or sets any user error encountered during the request.
    /// </summary>
    public string? UserError { get; set; }
    
    /// <summary>
    ///     Gets or sets the name of the validation.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    ///     Gets or sets the address returned from VIES during the validation.
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    ///     Gets or sets the request identifier returned from VIES during the validation.
    /// </summary>
    public string? RequestIdentifier { get; set; }
    
    /// <summary>
    ///     Gets or sets the VAT number returned from VIES during the validation.
    /// </summary>
    public string? VatNumber { get; set; }
}