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

                        case MovieNotFoundException movieNotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                context,
                                statusCode,
                                title: movieNotFoundException.Title,
                                detail: movieNotFoundException.Message,
                                instance: context.Request.Path);
                            break;

                        case DtoBadRequestException dtoBadRequestException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                context,
                                statusCode,
                                title: dtoBadRequestException.Title,
                                detail: dtoBadRequestException.Message,
                                instance: context.Request.Path);
                            break;

                        case BusinessRuleException businessRuleException:
                            statusCode = StatusCodes.Status422UnprocessableEntity;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                context,
                                statusCode,
                                title: businessRuleException.Title,
                                detail: businessRuleException.Message,
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
