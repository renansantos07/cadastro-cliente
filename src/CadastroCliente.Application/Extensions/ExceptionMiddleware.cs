using CadastroCliente.Application.DTO;
using Newtonsoft.Json;

namespace CadastroCliente.Application.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext httpContext,
            ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
                HandleExceptionAsync(httpContext, ex, logger);
            }
        }

        private async static void HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            ILogger<ExceptionMiddleware> logger)
        {
            string hashCode = context.GetHashCode().ToString();
            string hash = "Hash: [" + hashCode + "]";
            string response = string.Empty;

            logger.LogError(exception, hash);

            response = JsonConvert.SerializeObject(new ErrorResponseDTO
            {
                Success = false,
                Hash = hashCode,
                Message = "Estamos com problemas! Entre em contato com o suporte e informe o código a seguir: " + context.GetHashCode().ToString() + "/n Exception: " + exception
            });

            context.Response.Headers.Add("Content-Type", "application/json");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(response);

            await context.Response.CompleteAsync();
        }
    }
}