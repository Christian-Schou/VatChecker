﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace VatChecker.Application.FunctionalTests;

[SetUpFixture]
public partial class Testing
{
    private static IServiceScopeFactory _scopeFactory = null!;

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }
}
