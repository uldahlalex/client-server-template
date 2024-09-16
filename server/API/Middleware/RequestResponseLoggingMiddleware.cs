namespace API.Middleware;

public class RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await LogRequest(context.Request);

        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await LogResponse(context.Response);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
    }

    private async Task LogRequest(HttpRequest request)
    {
        request.EnableBuffering();

        var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        request.Body.Position = 0;

        var message = "\n\n" +
                      $"HTTP Request Information:{Environment.NewLine}" +
                      $"Schema:{request.Scheme}{Environment.NewLine}" +
                      $"Host: {request.Host}{Environment.NewLine}" +
                      $"Path: {request.Path}{Environment.NewLine}" +
                      $"QueryString: {request.QueryString}{Environment.NewLine}" +
                      $"Request Body: {requestBody}";

        logger.LogInformation(message);
    }

    private async Task LogResponse(HttpResponse response)
    {
        var responseBodyText = await new StreamReader(response.Body).ReadToEndAsync();

        var message = $"HTTP Response Information:{Environment.NewLine}" +
                      $"StatusCode: {response.StatusCode}{Environment.NewLine}" +
                      $"Response Body: {responseBodyText}\n\n";

        logger.LogInformation(message);
    }
}