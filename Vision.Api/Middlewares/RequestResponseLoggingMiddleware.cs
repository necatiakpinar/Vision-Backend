namespace Vision.Api.Middlewares;
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        
        // Request'i loglamak istiyorsan
        context.Request.EnableBuffering();
        var requestBodyStream = new StreamReader(context.Request.Body);
        var requestBodyText = await requestBodyStream.ReadToEndAsync();
        context.Request.Body.Position = 0;  // Stream'i yeniden konumlandır

        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} Body: {requestBodyText}");

        // Response'u yakalamak için bir stream oluştur
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        // Sonraki middleware'e devam et
        await _next(context);

        // Response'u logla
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        _logger.LogInformation($"Response: {responseBodyText}");

        await responseBody.CopyToAsync(originalBodyStream);
    }
}
