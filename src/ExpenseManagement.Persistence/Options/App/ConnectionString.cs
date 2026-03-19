namespace ExpenseManagement.Persistence.Options.App
{
    public sealed class ConnectionString
    {
        public required string Db { get; init; }
        public int MaxRetryCount { get; init; }
        public int MaxRetryDelay { get; init; }
        public bool EnableLogging { get; init; }
    }
}
