using VatChecker.Application.Vies.Commands.ValidateVatNumber;

namespace VatChecker.Web.Endpoints;

public class Vies : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(ValidateVatNumberAsync);
    }
    
    private static async Task<ViesValidationResultDto> ValidateVatNumberAsync(ISender sender, ValidateVatNumberCommand command)
    {
        return await sender.Send(command);
    }
}