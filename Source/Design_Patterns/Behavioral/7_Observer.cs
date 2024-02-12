using Demo.Common.Library;

namespace Design_Patterns.Behavioral
{
    public class ObserverDemo : DemoBase
    {
        public override string Name => "Behavioral.Observer";

        public override string ShortSummary => "Observer is a behavioral design pattern that allows some objects to notify other objects about changes in their state.";

        public override Task Run()
        {
            IBM ibm = new IBM("IBM", 120.00);
            var sorros = new Investor("Sorros");
            ibm.Attach(sorros);
            ibm.Attach(new Investor("Berkshire"));

            Console.WriteLine();

            ibm.Price = 120.10;
            ibm.Price = 121.00;

            ibm.Detach(sorros);

            Console.WriteLine();

            ibm.Price = 120.50;
            ibm.Price = 120.75;

            return Task.CompletedTask;
        }

        public abstract class Stock
        {
            private double price;
            private readonly List<IInvestor> investors = [];
            public Stock(string symbol, double price)
            {
                this.Symbol = symbol;
                this.price = price;
            }
            public void Attach(IInvestor investor)
            {
                Console.WriteLine($"Attaching investor {investor.Name} to {Symbol}");
                investors.Add(investor);
            }
            public void Detach(IInvestor investor)
            {
                Console.WriteLine($"Detaching investor {investor.Name} from {Symbol}");
                investors.Remove(investor);
            }
            public void Notify()
            {
                foreach (IInvestor investor in investors)
                {
                    investor.Update(this);
                }
                Console.WriteLine("");
            }
            public double Price
            {
                get => price;
                set
                {
                    if (price != value)
                    {
                        price = value;
                        Notify();
                    }
                }
            }
            public string Symbol { get; }
        }
        public class IBM : Stock
        {
            public IBM(string symbol, double price)
                : base(symbol, price)
            {
            }
        }
        public interface IInvestor
        {
            string Name { get; }
            void Update(Stock stock);
        }
        public class Investor : IInvestor
        {
            private readonly string name;

            public Investor(string name) => this.name = name;
            public string Name => name;
            public Stock Stock { get; set; }
            public void Update(Stock stock)
            {
                Console.WriteLine("Notified {0} of {1}'s " +
                    "change to {2:C}", name, stock.Symbol, stock.Price);
            }

        }
    }
}
