
using System.Globalization;
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
            int id = _expenses.Any() ? _expenses.Max(x => x.Id) + 1 : 1;
            Expense expense = new Expense(id, description, amount);

            _expenses.Add(expense);
            SaveExpenses();

            Console.WriteLine($"# Expense added successfully (ID: {id})");
        }

        public void DeleteExpense(int id)
        {
            var expense = _expenses.Find(e => e.Id == id);

            if(expense != null)
            {
                _expenses.Remove(expense);
                SaveExpenses();
                Console.WriteLine("# Expense deleted successfully");
            }
            else
            {
                Console.WriteLine($"# Expense with ID {id} not found.");
            }
        }

        public void ListExpenses()
        {
            if(_expenses.Any())
            {
                Console.WriteLine("# ID  Date       Description  Amount");
                foreach(var expense in _expenses)
                {
                    Console.WriteLine(expense);
                }
            }
            else
            {
                Console.WriteLine("# There are no expenses to display");
            }
        }

        public void Summary()
        {
            if(_expenses.Any())
            {
                float total = 0;
                foreach (var expense in _expenses)
                {
                    total += expense.Amount;
                }
                Console.WriteLine($"Total expenses: ${total}");
            }
            else
            {
                Console.WriteLine("# There are no expenses to summarize.");
            }
        }

        public void Summary(int month)
        {
            if (_expenses.Any())
            {
                float total = 0;
                foreach (var expense in _expenses.Where(x => x.Date.Month == month))
                {
                    total += expense.Amount;
                }
                Console.WriteLine($"# Total expenses for {DateTimeFormatInfo.CurrentInfo.GetMonthName(month)}: ${total}");
            }
            else
            {
                Console.WriteLine("# There are no expenses to summarize.");
            }
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
