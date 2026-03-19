namespace ExpenseManagement.Domain.Core.Exceptions
{
    public class BadRequestException : ArgumentException
    {
        public HashSet<string> Errors { get; }
        public static HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public override string Message
        {
            get
            {
                return Errors.Any()
                    ? Environment.NewLine + string.Join(Environment.NewLine, Errors)
                    : base.Message;
            }
        }

        public BadRequestException(HashSet<string> errors)
        {
            Errors = errors;
        }
    }
}
