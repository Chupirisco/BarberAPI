namespace Barbearia.API.Middlewares
{
    public class ApiKeyMiddleware
    {
        private const string API_KEY_HEADER = "X-Api-Key";
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key ausente.");
                return;
            }

            var apiKey = _configuration.GetValue<string>("ApiKey");
            if (!apiKey!.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key inválida.");
                return;
            }

            await _next(context);
        }
    }
}