namespace ExpenseManagement.Application.Mappers
{
    public static class SavingMapper
    {
        public static SavingDTO ToSavingDTO(this Saving saving)
        {
            if (saving is null)
            {
                return null!;
            }

            return new SavingDTO()
            {
                Id = saving.Id,
                Name = saving.Name,
                Amount = saving.Amount,
                Category = saving.Category,
                Date = saving.Date,
                Description = saving.Description
            };
        }

        public static SavingTableDTO ToSavingTableDTO(this Saving saving)
        {
            if (saving is null)
            {
                return null!;
            }

            return new SavingTableDTO()
            {
                Id = saving.Id,
                Amount = saving.Amount,
                Date = saving.Date.ToString("dd-MM-yyyy HH:mm:ss")
            };
        }
    }
}
