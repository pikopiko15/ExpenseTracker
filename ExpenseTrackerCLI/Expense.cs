namespace ExpenseTrackerCLI
{
    public class Expense
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public float Amount { get; set; }

        public Expense(int id, string description, float amount)
        {
            this.Id = id;
            this.Description = description;
            this.Amount = amount;
            this.Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"# {Id}   {Date:yyyy-MM-dd}  {Description}        ${Amount}";
        }
    }
}
