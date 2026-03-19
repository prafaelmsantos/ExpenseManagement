namespace ExpenseManagement.API.Core.HealthChecks
{
    internal sealed class ExceptionConverter : System.Text.Json.Serialization.JsonConverter<Exception>
    {
        public override Exception? Read(
            ref System.Text.Json.Utf8JsonReader reader,
            Type typeToConvert,
            System.Text.Json.JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(
            System.Text.Json.Utf8JsonWriter writer,
            Exception? value,
            System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.Message.ToString());
        }
    }
}
