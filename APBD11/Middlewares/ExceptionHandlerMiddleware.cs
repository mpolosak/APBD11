namespace APBD11.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next,  ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleNotFoundExceptionAsync(context, ex);
        }
    }

    private static async Task HandleNotFoundExceptionAsync(HttpContext context,  NotFoundException ex)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/text";
        await context.Response.WriteAsync(ex.Message);
    }
}