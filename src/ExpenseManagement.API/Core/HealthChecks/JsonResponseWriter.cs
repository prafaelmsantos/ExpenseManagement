namespace ExpenseManagement.API.Core.HealthChecks
{
    public static class JsonResponseWriter
    {
        private static readonly System.Text.Json.JsonSerializerOptions _serializerOptions = CreateSerializerOptions();

        public static async Task WriteJsonResponseAsync(
            HttpContext httpContext,
            HealthReport report)
        {
            if (report is not null)
            {
                httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

                await System.Text.Json.JsonSerializer.SerializeAsync(httpContext.Response.Body, report, _serializerOptions);
            }
            else
            {
                var emptyResponse = new byte[] { (byte)'{', (byte)'}' };
                await httpContext.Response.BodyWriter.WriteAsync(emptyResponse);
            }
        }

        private static System.Text.Json.JsonSerializerOptions CreateSerializerOptions()
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            };

            options.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            options.Converters.Add(new TimeSpanConverter());
            options.Converters.Add(new ExceptionConverter());

            return options;
        }
    }
}
