namespace ExpenseManagement.Persistence.Options.App
{
    public sealed class AppSettings
    {
        public required string BasePath { get; init; }

        public required ConnectionString ConnectionStrings { get; init; }
    }
}
