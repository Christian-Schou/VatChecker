using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using VatChecker.Interfaces;
using VatChecker.Models;

namespace VatChecker.Controllers
{
    /// <summary>
    /// Controller to handle EU Vat No Validation Checks.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Validate EU Companies VAT No. against VAT Information Exchange System (VIES).")]
    public class EUVatController : ControllerBase
    {
        private readonly IEUVatChecker _euVatChecker;

        /// <summary>
        /// EU Vat Controller Constructor for DI
        /// </summary>
        /// <param name="euVatChecker"></param>
        public EUVatController(IEUVatChecker euVatChecker)
        {
            _euVatChecker = euVatChecker;
        }

        /// <summary>
        /// This endpoint will validate the given Vat No. in the specified Country and return true if valid or false if not.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="vatNo"></param>
        /// <returns></returns>
        [HttpGet(Name = "CheckEuVat")]
        [SwaggerOperation(
            Summary = "Validate EU Organisation VAT No.",
            Description = "This endpoint will validate the given Vat No. in the specified Country and return true if valid or false if not.",
            OperationId = "CheckVatNo"
        )]
        [SwaggerResponse(200, "The VIES Result", typeof(ViesVatResult))]
        public  ActionResult<ViesVatResult> CheckVatNo( [FromQuery, SwaggerParameter("Country Code (2-digit. DK/FR/BG)", Required = true)]string countryCode = "dk", 
                                                        [FromQuery, SwaggerParameter("Company Vat No. i.e. 13612870)", Required = true)] string vatNo = "13612870")
        {
            // Try / Catch
            try
            {
                // Check Company Vat No. Async
                var viesResult = _euVatChecker.CheckEuVatNumberAsync(countryCode, vatNo);

                // If result not equals null, return it.
                if (viesResult != null)
                {
                    Log.Information($"Returning VIES result for {viesResult.Result.Name}. status for #{viesResult.Result.VatNumber} is {viesResult.Result.Valid}");
                    return StatusCode(StatusCodes.Status200OK, viesResult.Result);
                }
                else
                {
                    Log.Warning("No Company could be found or VAT was not valid.");
                    // If result is null, return false (not valid) vat no.
                    return StatusCode(StatusCodes.Status500InternalServerError, new ViesVatResult
                    {
                        VatNumber = vatNo,
                        CountryCode = countryCode,
                        Valid = false,
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error($"An error occured: {ex.Message}");
                throw;
            }
        }
    }
}
