using System.Net;

namespace Reverse_Analytics.Api.Middlewares;

public class ErrorHandlerMiddeware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlerMiddeware(RequestDelegate next, ILogger<ErrorHandlerMiddeware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }

    private async Task HandleAsync(HttpContext context, Exception error)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        string message = "Internal error. Something went wrong.";

        await context.Response.WriteAsync(message);
    }

    private static List<string> GetErrorsList(Exception? error, List<string> errorsList)
    {
        if (error is not null)
        {
            errorsList.Add($"{error.GetType()}: '{error.Message}'");

            return GetErrorsList(error.InnerException, errorsList);
        }

        return errorsList;
    }
}
