using Demo.Common.Library;

namespace Design_Patterns.CreationalPatterns
{
    public class AbstractFactoryDemo : DemoBase
    {
        public override string Name => "Creational.AbstractFactory";
        public override string ShortSummary => @"Lets you produce families of related objects without specifying their concrete classes.";

        public override Task Run()
        {
            Console.WriteLine("Abstract Factory Option 1");
            IAbstractFactory abstractFactory = new AbstractFactory_Option1();
            var cc = abstractFactory.GetCreditCard();
            var cp = abstractFactory.GetConsolePrinter();

            cp.WritePrimary($"Credit Card Info:{Environment.NewLine}");
            cp.WriteSecondary(cc.ToString());

            Console.WriteLine();

            Console.WriteLine("Abstract Factory Option 2");

            abstractFactory = new AbstractFactory_Option2();
            cc = abstractFactory.GetCreditCard();
            cp = abstractFactory.GetConsolePrinter();

            cp.WritePrimary($"Credit Card Info:{Environment.NewLine}");
            cp.WriteSecondary(cc.ToString());

            return Task.CompletedTask;
        }
    }

    public interface IAbstractFactory
    {
        CreditCard GetCreditCard();
        ConsolePrinter GetConsolePrinter();
    }

    public class AbstractFactory_Option1 : IAbstractFactory
    {
        public ConsolePrinter GetConsolePrinter() => new ConsolePrinterFactory().Get("Dark");
        public CreditCard GetCreditCard() => new CreditCardFactory2024().Get("Platinum");
    }

    public class AbstractFactory_Option2 : IAbstractFactory
    {
        public ConsolePrinter GetConsolePrinter() => new ConsolePrinterFactory().Get("Light");
        public CreditCard GetCreditCard() => new CreditCardFactory2024().Get("MoneyBack");
    }
}
