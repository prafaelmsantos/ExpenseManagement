namespace ExpenseManagement.Domain.Core.Exceptions
{
    public class UnauthorizedException : ArgumentException
    {
        public HashSet<string> Errors { get; }
        public static HttpStatusCode HttpStatusCode => HttpStatusCode.Unauthorized;

        public override string Message
        {
            get
            {
                return Errors.Any()
                    ? Environment.NewLine + string.Join(Environment.NewLine, Errors)
                    : base.Message;
            }
        }

        public UnauthorizedException(HashSet<string> errors)
        {
            Errors = errors;
        }
    }
}
