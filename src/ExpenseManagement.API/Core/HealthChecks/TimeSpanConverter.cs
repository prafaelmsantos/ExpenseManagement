namespace ExpenseManagement.API.Core.HealthChecks
{
    internal sealed class TimeSpanConverter : System.Text.Json.Serialization.JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(
            ref System.Text.Json.Utf8JsonReader reader,
            Type typeToConvert,
            System.Text.Json.JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(
            System.Text.Json.Utf8JsonWriter writer,
            TimeSpan value,
            System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
