using System.Text;
using System.Xml.Linq;
using VatChecker.Interfaces;
using VatChecker.Models;

namespace VatChecker.Repositories
{
    /// <summary>
    /// Implementation of EU Vat Check Operations
    /// </summary>
    public class EUVatChecker : IEUVatChecker
    {
        // Cache the HTTP Client
        static HttpClient defaultClient = new HttpClient
        {
            BaseAddress = new Uri(Endpoint)
        };

        // URL to post VAT check request to
        const string Endpoint = "https://ec.europa.eu/taxation_customs/vies/services/checkVatService";

        /// <summary>
        /// Check VAT number for a specific country and VAT number.
        /// </summary>
        /// <param name="country">2 digit Country Code (i.e. DK/FR/IE ...)</param>
        /// <param name="vatNumber">VAT number to check (without country code)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A valid VatCheckerResult. Check "Valid" field for success/failure.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ViesVatResult> CheckEuVatNumberAsync(string country, string vatNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(country))
                throw new ArgumentNullException(nameof(country));

            if (string.IsNullOrEmpty(vatNumber))
                throw new ArgumentNullException(nameof(vatNumber));

            XNamespace rootNs = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace checkVatNs = "urn:ec.europa.eu:taxud:vies:services:checkVat:types";

            var envelope = new XElement(rootNs + "Envelope");
            var body = new XElement(rootNs + "Body");
            var checkVat = new XElement(checkVatNs + "checkVat");

            checkVat.Add(new XElement(checkVatNs + "countryCode", country));
            checkVat.Add(new XElement(checkVatNs + "vatNumber", vatNumber));

            body.Add(checkVat);
            envelope.Add(body);

            var strContent = new StringContent(envelope.ToString(), Encoding.UTF8, "text/xml");

            using (var resp = await defaultClient.PostAsync("", strContent, cancellationToken))
            {
                var rStr = await resp.Content.ReadAsStringAsync();

                var returnElem = XElement.Parse(rStr);

                var checkVatResponse = returnElem?.Element(rootNs + "Body")?.Element(checkVatNs + "checkVatResponse");

                return new ViesVatResult
                {
                    CountryCode = checkVatResponse?.Element(checkVatNs + "countryCode")?.Value,
                    VatNumber = checkVatResponse?.Element(checkVatNs + "vatNumber")?.Value,
                    Valid = bool.TryParse(checkVatResponse?.Element(checkVatNs + "valid")?.Value, out var b) && b,
                    Name = checkVatResponse?.Element(checkVatNs + "name")?.Value,
                    Address = checkVatResponse?.Element(checkVatNs + "address")?.Value
                };
            }

        }
    }
}
