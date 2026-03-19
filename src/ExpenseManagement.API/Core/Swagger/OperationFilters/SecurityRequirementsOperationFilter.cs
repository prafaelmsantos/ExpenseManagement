namespace ExpenseManagement.API.Core.Swagger.OperationFilters
{
    public sealed class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public static readonly IReadOnlyList<OpenApiSecurityScheme> SecuritySchemes =
        [
            new OpenApiSecurityScheme
            {
                Name = JwtBearerDefaults.AuthenticationScheme,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Provide JWT Bearer token in the text input below."
            }
        ];

        #region Public methods
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var endpointMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;

            var hasAuthorize = endpointMetadata
                .OfType<AuthorizeAttribute>()
                .Any();

            var hasAllowAnonymous = endpointMetadata
                .OfType<AllowAnonymousAttribute>()
                .Any();

            if (!hasAuthorize || hasAllowAnonymous)
            {
                return;
            }

            operation.Responses ??= [];
            AddResponse(HttpStatusCode.Unauthorized, operation.Responses);
            AddResponse(HttpStatusCode.Forbidden, operation.Responses);

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                GetSecurityRequirements(context)
            };
        }
        #endregion

        #region Private methods
        private static OpenApiSecurityRequirement GetSecurityRequirements(OperationFilterContext context)
        {
            List<string> requiredScopes = context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<AuthorizeAttribute>()
                .Select(attribute => attribute.Policy!)
                .Distinct()
                .ToList();

            var securityRequirements = new OpenApiSecurityRequirement();

            foreach (var curSecurityScheme in SecuritySchemes)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(curSecurityScheme.Scheme);

                securityRequirements.Add(
                    new OpenApiSecuritySchemeReference(curSecurityScheme.Scheme, context.Document),
                    requiredScopes);
            }

            return securityRequirements;
        }

        private static void AddResponse(HttpStatusCode statusCode, OpenApiResponses responses)
        {
            responses.TryAdd(
                ((int)statusCode).ToString(System.Globalization.CultureInfo.InvariantCulture),
                new OpenApiResponse
                {
                    Description = statusCode.ToString()
                });
        }
        #endregion
    }
}
