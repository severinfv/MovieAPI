using Domain.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Movies.API.Extensions;

public static class ExceptionExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {

                    var problemDetailsFactory = app.Services.GetRequiredService<ProblemDetailsFactory>();

                    ProblemDetails problemDetails;
                    int statusCode;

                    switch (contextFeature.Error)
                    {

                        case MovieNotFoundException companyNotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                context,
                                statusCode,
                                title: companyNotFoundException.Title,
                                detail: companyNotFoundException.Message,
                                instance: context.Request.Path);
                            break;

                        default:
                            statusCode = StatusCodes.Status500InternalServerError;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                context,
                                statusCode,
                                title: "Internal Server Error",
                                detail: contextFeature.Error.Message,
                                instance: context.Request.Path);
                            break;
                    };
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }
            });
        });
    }
}
