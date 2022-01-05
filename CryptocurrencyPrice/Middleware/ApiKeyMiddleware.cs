namespace CryptocurrencyPrice.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var apiKeyName = _config.GetValue<string>("ApiKeyName");
            if (!context.Request.Headers.TryGetValue(apiKeyName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided");
                return;
            }
            var apiKey = _config.GetValue<string>(apiKeyName);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }

            await _next(context);
        }
    }
}