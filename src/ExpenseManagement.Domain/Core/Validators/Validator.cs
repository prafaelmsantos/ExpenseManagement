namespace ExpenseManagement.Domain.Core.Validators
{
    /// <summary> Class to handle if the rules are valid or not. </summary>
    public sealed class Validator
    {
        private readonly HashSet<string> _errors;
        private Validator() => _errors = [];

        public static Validator New() => new();


        public Validator When(bool isError, string error)
        {
            if (isError && !_errors.Contains(error))
            {
                _errors.Add(error);
            }

            return this;
        }

        public void TriggerBadRequestExceptionIfExist()
        {
            if (_errors.Any())
            {
                throw new BadRequestException(_errors);
            }
        }

        public void TriggerUnauthorizedExceptionIfExist()
        {
            if (_errors.Any())
            {
                throw new UnauthorizedException(_errors);
            }
        }
    }
}
