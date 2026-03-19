namespace ExpenseManagement.API.Core.Swagger.Options
{
    public sealed class SwaggerSettings
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public bool Enabled { get; init; }
    }
}
