using Swashbuckle.AspNetCore.Annotations;

namespace VatChecker.Models
{
    /// <summary>
    /// Represents a VIES VAT Result
    /// </summary>
    public class ViesVatResult
    {
        /// <summary>
        /// Name of the organization
        /// </summary>
        [SwaggerSchema(
            Title = "Company Name",
            Description = "Full Company Name as registered in system.",
            Format = "string"
            )]
        public string Name { get; set; }

        /// <summary>
        /// Country Code the organisation belongs to
        /// </summary>
        [SwaggerSchema(
            Title = "Country Code",
            Description = "Country Code (2-digit. DK/FR/BG).",
            Format = "string",
            Required = new string[] { "true" })]
        public string CountryCode { get; set; }

        /// <summary>
        /// Registered address for the VAT No.
        /// </summary>
        [SwaggerSchema(
            Title = "Company Address",
            Description = "Full Company Address as registered in system.",
            Format = "string"
            )]
        public string Address { get; set; }

        /// <summary>
        /// Is the VAT valid?
        /// </summary>
        [SwaggerSchema(
            Title = "Is Vat No. Valid?",
            Description = "Status about Vat No. validation.",
            Format = "string"
            )]
        public bool Valid { get; set; }

        /// <summary>
        /// The VAT Number for the organisation
        /// </summary>
        [SwaggerSchema(
            Title = "Company Vat No.",
            Description = "Vat No. registered in system for Company.",
            Format = "string",
            Required = new string[] { "true" })]
        public string VatNumber { get; set; }
    }
}
