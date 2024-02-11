using System.IO;

namespace Design_Patterns.Behavioral
{
    public class ChainOfResponsibilityDemo : IDemonstratePattern
    {
        public string Name => "Behavioral.ChainOfResponsibility";

        public string ShortSummary => @"lets you pass requests along a chain of handlers. Upon receiving a request, each handler decides either to process the request or to pass it to the next handler in the chain.";

        public void Run()
        {
            Approver larry = new Officer();
            Approver sam = new VicePresident();
            Approver tammy = new President();
            larry.SetSuccessor(sam);
            sam.SetSuccessor(tammy);
            
            Purchase p = new Purchase(2034, 350.00, "Supplies");
            larry.ProcessRequest(p);
            p = new Purchase(2035, 32590.10, "Project X");
            larry.ProcessRequest(p);
            p = new Purchase(2036, 122100.00, "Project Y");
            larry.ProcessRequest(p);
        }
    }

    public abstract class Approver
    {
        protected Approver successor;
        public void SetSuccessor(Approver successor) => this.successor = successor;
        public abstract void ProcessRequest(Purchase purchase);
    }
    public class Officer : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 10000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                    GetType().Name, purchase.Number);
            }
            else
            {
                successor?.ProcessRequest(purchase);
            }
        }
    }
    public class VicePresident : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 25000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                    GetType().Name, purchase.Number);
            }
            else
            {
                successor?.ProcessRequest(purchase);
            }
        }
    }
    public class President : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 100000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                    GetType().Name, purchase.Number);
            }
            else
            {
                Console.WriteLine(
                    "Request# {0} requires an executive meeting!",
                    purchase.Number);
            }
        }
    }
    public class Purchase
    {
        public Purchase(int number, double amount, string purpose)
        {
            Number = number;
            Amount = amount;
            Purpose = purpose;
        }
        public int Number { get; set; }
        public double Amount { get; set; }
        public string Purpose { get; set; }
    }
}
