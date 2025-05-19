namespace ExpenseTrackerCLI
{
    public class Expense
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public float Amount { get; set; }

        public override string ToString()
        {
            return $"# {Id}   {Date}  {Description}        ${Amount}";
        }
    }
}
