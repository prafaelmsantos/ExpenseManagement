namespace ExpenseManagement.API.Core.Swagger.SchemaFilters
{
    public sealed class SwaggerRequiredSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema is not OpenApiSchema editableSchema ||
                editableSchema.Properties is null)
            {
                return;
            }

            editableSchema.Required ??= new HashSet<string>();

            foreach (var schemaProperty in editableSchema.Properties)
            {
                if (schemaProperty.Value.Type == JsonSchemaType.Null)
                {
                    continue;
                }

                if (!editableSchema.Required.Contains(schemaProperty.Key))
                {
                    editableSchema.Required.Add(schemaProperty.Key);
                }
            }
        }
    }
}
