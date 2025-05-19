
using System.Text.Json;

namespace ExpenseTrackerCLI
{
    public class ExpenseManager
    {
        private const string FilePath = "expenses.json";

        private List<Expense> _expenses;

        public ExpenseManager()
        {
            _expenses = LoadExpenses() ?? new List<Expense>();
        }

        public void AddExpense(string description, float amount)
        {
            // TODO
        }

        private void SaveExpenses()
        {
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var jsonData = JsonSerializer.Serialize(_expenses, new JsonSerializerOptions { WriteIndented = true });
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.Write(jsonData);
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("# Error saving expenses.");
                Console.WriteLine(ex.Message);
            }
        }

        private List<Expense>? LoadExpenses()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    File.WriteAllText(FilePath, "[]");
                    return new List<Expense>();
                }

                var jsonData = File.ReadAllText(FilePath);

                if(string.IsNullOrEmpty(jsonData))
                {
                    return new List<Expense>();
                }

                return JsonSerializer.Deserialize<List<Expense>>(jsonData);
            }
            catch(JsonException ex)
            {
                Console.WriteLine("# Error loading expenses: Invalid JSON format. Resetting expenses.json.");
                Console.WriteLine(ex.Message);
                File.WriteAllText(FilePath, "[]");

                return new List<Expense>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("# Error loading expenses.");
                Console.WriteLine(ex.Message);

                return new List<Expense>();
            }
        }
    }
}
