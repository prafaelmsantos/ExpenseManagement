namespace ExpenseManagement.API.Core.Swagger.SchemaFilters
{
    public sealed class SwaggerEnumSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema is not OpenApiSchema editableSchema || !context.Type.IsEnum)
            {
                return;
            }

            var values = Enum.GetValues(context.Type)
                .Cast<int>()
                .ToList();

            editableSchema.Type = JsonSchemaType.Integer;
            editableSchema.Format = null;
            editableSchema.Enum = values.Select(x => System.Text.Json.Nodes.JsonValue.Create(x))
                .Cast<System.Text.Json.Nodes.JsonNode>()
                .ToList();

            var enumVarNames = new System.Text.Json.Nodes.JsonObject();
            foreach (var curValue in values)
            {
                var name = Enum.GetName(context.Type, curValue);

                if (!string.IsNullOrEmpty(name))
                {
                    enumVarNames.Add(name, System.Text.Json.Nodes.JsonValue.Create(curValue));
                }
            }

            editableSchema.Extensions ??= new Dictionary<string, IOpenApiExtension>();
            editableSchema.Extensions.Add("x-enum-varnames", new JsonNodeExtension(enumVarNames));
        }
    }
}
