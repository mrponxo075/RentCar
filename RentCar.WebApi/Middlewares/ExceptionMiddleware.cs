using System.Net;
using System.Text.Json;

namespace RentCar.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                Guid trace = Guid.NewGuid();

                _logger.LogError(ex, message: $"Unexpected error. Trace: {trace}");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1",
                    title = "Internal Server Error",
                    status = context.Response.StatusCode,
                    traceId = trace.ToString()
                };

                var json = JsonSerializer.Serialize(errorResponse);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
