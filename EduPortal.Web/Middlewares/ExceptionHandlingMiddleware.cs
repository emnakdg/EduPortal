using FluentValidation;

namespace EduPortal.Web.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation hatası: {Errors}",
                string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));

            var errorMessages = string.Join(" | ", ex.Errors.Select(e => e.ErrorMessage));
            context.Response.Redirect($"/Home/Error?message={Uri.EscapeDataString(errorMessages)}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Beklenmeyen hata oluştu: {Message}", ex.Message);

            context.Response.Redirect("/Home/Error");
        }
    }
}