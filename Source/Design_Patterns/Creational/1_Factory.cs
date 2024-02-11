namespace Design_Patterns.CreationalPatterns
{

    public class FactoryDemo : IDemonstratePattern
    {
        public string Name => "Creational.Factory";
        public string ShortSummary => @"Provides an interface for creating objects in a superclass,
but allows subclasses to alter the type of objects that will be created.";

        public void Run()
        {
            Console.WriteLine("Platinum Credit Card:");
            ICreditCardFactory factory = new CreditCardFactory2024();
            CreditCard card = factory.Get("Platinum");
            Console.WriteLine(card.ToString());

            Console.WriteLine("MoneyBack Credit Card:");
            factory = new CreditCardFactory2023();
            card = factory.Get("MoneyBack");
            Console.WriteLine(card.ToString());
        }
    }

    public interface ICreditCardFactory
    {
        CreditCard Get(string name);
    }

    public class CreditCardFactory2023 : ICreditCardFactory
    {
        public CreditCard Get(string name)
        {
            switch (name)
            {
                case "MoneyBack":
                    return new MoneyBack();
                case "Platinum":
                    return new Platinum();
            }

            throw new NotImplementedException();
        }
    }
    public class CreditCardFactory2024 : ICreditCardFactory
    {
        public CreditCard Get(string name)
        {
            switch (name)
            {
                case "MoneyBack":
                    return new MoneyBack();
                case "Titanium":
                    return new Titanium();
                case "Platinum":
                    return new Platinum();
            }

            throw new NotImplementedException();
        }
    }

    public abstract class CreditCard
    {
        public abstract string GetCardType();
        public abstract int GetCreditLimit();
        public abstract int GetAnnualCharge();
        public override string ToString() => $"Type: {GetCardType()};{Environment.NewLine}Credit Limit: {GetCreditLimit()};{Environment.NewLine}Annual Charge: {GetAnnualCharge()}";
    }

    public class MoneyBack : CreditCard
    {
        public override string GetCardType() => "MoneyBack";

        public override int GetCreditLimit() => 15000;

        public override int GetAnnualCharge() => 500;
    }
    public class Titanium : CreditCard
    {
        public override string GetCardType() => "Titanium Edge";
        public override int GetCreditLimit() => 25000;
        public override int GetAnnualCharge() => 1500;
    }
    public class Platinum : CreditCard
    {
        public override string GetCardType() => "Platinum Plus";
        public override int GetCreditLimit() => 35000;
        public override int GetAnnualCharge() => 2000;
    }

    //Example-2

    public interface IConsolePrinterFactory
    {
        ConsolePrinter Get(string name);
    }

    public class ConsolePrinterFactory : IConsolePrinterFactory
    {
        public ConsolePrinter Get(string name)
        {
            switch (name)
            {
                case "Dark":
                    return new DarkConsolePrinter();
                case "Light":
                    return new LightConsolePrinter();
            }

            throw new NotImplementedException();
        }
    }

    public abstract class ConsolePrinter
    {
        public abstract ConsoleColor PrimaryForegroudColor { get; }
        public abstract ConsoleColor SecondaryForegroudColor { get; }
        public abstract ConsoleColor PrimaryBackgroundColor { get; }
        public abstract ConsoleColor SecondaryBackgroundColor { get; }

        public virtual void WritePrimary(string content)
        {
            Console.ForegroundColor = PrimaryForegroudColor;
            Console.BackgroundColor = PrimaryBackgroundColor;

            Console.Write(content);

            Console.ResetColor();
        }
        public virtual void WriteSecondary(string content)
        {
            Console.ForegroundColor = SecondaryForegroudColor;
            Console.BackgroundColor = SecondaryBackgroundColor;

            Console.Write(content);

            Console.ResetColor();
        }
    }

    public class DarkConsolePrinter : ConsolePrinter
    {
        public override ConsoleColor PrimaryForegroudColor => ConsoleColor.Green;

        public override ConsoleColor SecondaryForegroudColor => ConsoleColor.Cyan;

        public override ConsoleColor PrimaryBackgroundColor => ConsoleColor.Black;

        public override ConsoleColor SecondaryBackgroundColor => ConsoleColor.Black;
    }
    public class LightConsolePrinter : ConsolePrinter
    {
        public override ConsoleColor PrimaryForegroudColor => ConsoleColor.Magenta;

        public override ConsoleColor SecondaryForegroudColor => ConsoleColor.Black;

        public override ConsoleColor PrimaryBackgroundColor => ConsoleColor.Gray;

        public override ConsoleColor SecondaryBackgroundColor => ConsoleColor.Gray;
    }
}
