using System.Text;

namespace RESTApiVerticalSlice.Common.Logging;

public static class RequestLoggingMiddleware
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var endpoint = context.GetEndpoint();
            var logMeta = endpoint?.Metadata.GetMetadata<LogAttribute>();
            if (logMeta is not null)
            {
                Console.WriteLine($"[LOG - Start] [{logMeta.Level}] {logMeta.Operation} {context.Request.Method} {context.Request.Path}");
                var sw = System.Diagnostics.Stopwatch.StartNew();

                // Read request body
                string requestBody = string.Empty;
                try
                {
                    context.Request.EnableBuffering();
                    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LOG - Warning] Could not read request body: {ex.Message}");
                }

                if (!string.IsNullOrEmpty(requestBody))
                {
                    Console.WriteLine($"[LOG - RequestBody] {requestBody}");
                }

                // Capture response
                var originalBodyStream = context.Response.Body;
                await using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                try
                {
                    await next();

                    // Read response
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var responseText = string.Empty;
                    try
                    {
                        using var respReader = new StreamReader(context.Response.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
                        responseText = await respReader.ReadToEndAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[LOG - Warning] Could not read response body: {ex.Message}");
                    }

                    // Reset position and copy back to original stream
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    await context.Response.Body.CopyToAsync(originalBodyStream);

                    sw.Stop();
                    Console.WriteLine($"[LOG - End] [{logMeta.Level}] {logMeta.Operation} - Status: {context.Response?.StatusCode} - Elapsed: {sw.ElapsedMilliseconds}ms");

                    if (!string.IsNullOrEmpty(responseText))
                    {
                        Console.WriteLine($"[LOG - ResponseBody] {responseText}");
                    }
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    Console.WriteLine($"[LOG - Error] [{logMeta.Level}] {logMeta.Operation} - {ex.Message} - Elapsed: {sw.ElapsedMilliseconds}ms");
                    // Ensure response body is copied back on error
                    context.Response.Body = originalBodyStream;
                    throw;
                }
                finally
                {
                    context.Response.Body = originalBodyStream;
                }
            }
            else
            {
                await next();
            }
        });
    }
}
