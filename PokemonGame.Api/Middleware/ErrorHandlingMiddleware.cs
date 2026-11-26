namespace PokemonGame.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                exception switch
                {
                    InvalidOperationException =>
                        StatusCodes.Status400BadRequest,

                    KeyNotFoundException =>
                        StatusCodes.Status404NotFound,

                    _ =>
                        StatusCodes.Status500InternalServerError
                };

            await context.Response.WriteAsJsonAsync(
                new
                {
                    error = exception.Message
                });
        }
    }
}
