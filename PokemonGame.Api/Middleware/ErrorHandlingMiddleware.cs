namespace PokemonGame.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(
            RequestDelegate next)
        {
            _next = next;
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
                await HandleException(
                    context,
                    ex);
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
