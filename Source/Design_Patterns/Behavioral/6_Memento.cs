using Demo.Common.Library;

namespace Design_Patterns.Behavioral
{
    public class MementoDemo : DemoBase
    {
        public override string Name => "Behavioral.Memento";

        public override string ShortSummary => "allows making snapshots of an object’s state and restoring it in future.";

        public override Task Run()
        {
            SalesProspect s = new SalesProspect
            {
                Name = "Noel van Halen",
                Phone = "(412) 256-0990",
                Budget = 25000.0
            };

            Memento state = s.SaveMemento();

            s.Name = "Leo Welch";
            s.Phone = "(310) 209-7111";
            s.Budget = 1000000.0;

            s.RestoreMemento(state);
            return Task.CompletedTask;
        }

        public class SalesProspect
        {
            string name;
            string phone;
            double budget;
            // Gets or sets name
            public string Name
            {
                get => name;
                set
                {
                    name = value;
                    Console.WriteLine("Name:   " + name);
                }
            }
            public string Phone
            {
                get => phone;
                set
                {
                    phone = value;
                    Console.WriteLine("Phone:  " + phone);
                }
            }
            public double Budget
            {
                get => budget;
                set
                {
                    budget = value;
                    Console.WriteLine("Budget: " + budget);
                }
            }
            public Memento SaveMemento()
            {
                Console.WriteLine("\nSaving state --\n");
                return new Memento(name, phone, budget);
            }
            public void RestoreMemento(Memento memento)
            {
                Console.WriteLine("\nRestoring state --\n");
                Name = memento.Name;
                Phone = memento.Phone;
                Budget = memento.Budget;
            }
        }
        public class Memento
        {
            public Memento(string name, string phone, double budget)
            {
                Name = name;
                Phone = phone;
                Budget = budget;
            }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Budget { get; set; }
        }
    }
}
