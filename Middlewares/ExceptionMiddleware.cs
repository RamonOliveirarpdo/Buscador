using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Buscador.Middlewares
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

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Ocorreu um erro não tratado.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Define o Status Code com base no tipo de erro
            context.Response.StatusCode = exception switch
            {
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)StatusCodes.Status500InternalServerError
            };

            var response = new
            {
                error = (exception is InvalidOperationException || exception is KeyNotFoundException)
                    ? exception.Message
                    : "Ocorreu um erro interno no servidor. Tente novamente mais tarde."
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }
}
