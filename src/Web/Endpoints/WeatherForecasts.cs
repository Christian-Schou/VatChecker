using VatChecker.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace VatChecker.Web.Endpoints;

public class WeatherForecasts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetWeatherForecasts);
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(ISender sender)
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }
}
