using VatChecker.Models;

namespace VatChecker.Interfaces
{
    /// <summary>
    /// Interface defining the basic VAT Query operations for EU Vat Numbers.
    /// </summary>
    public interface IEUVatChecker
    {
        /// <summary>
        /// Check a EU Vat Number
        /// </summary>
        /// <param name="country">2 digit country code (i.e. DE/DK/BG ... )</param>
        /// <param name="vatNumber">VAT Number to check (without country code)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>VIES Vat Result</returns>
        Task<ViesVatResult> CheckEuVatNumberAsync(string country, string vatNumber, CancellationToken cancellationToken = default);
    }
}
