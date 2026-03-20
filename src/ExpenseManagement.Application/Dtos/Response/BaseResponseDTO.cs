namespace ExpenseManagement.Application.Dtos.Response
{
    public class BaseResponseDTO
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
