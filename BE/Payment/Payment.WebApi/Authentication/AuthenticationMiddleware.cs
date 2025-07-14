namespace Payment.WebApi.Authentication;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (!context.Request.Headers.TryGetValue(AuthenticationConstant.ApiKeyHeaderName, out var headerApiKey))
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("El header X-Api-Key no se envio");
                return;
            }

            var apiKey = _configuration["apiKey"];
            if (apiKey != headerApiKey)
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("El valor del API key no es valido");
                return;
            }


            await _next(context);
        }
        catch (Exception)
        {

            throw;
        }
    }
}