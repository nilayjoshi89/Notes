
namespace Design_Patterns.StructuralPatterns
{
    public class AdapterDemo : IDemonstratePattern
    {
        public string Name => "Structural.Adapter";

        public string ShortSummary => @"Adapter is a structural design pattern, which allows incompatible objects to collaborate.
Converts the interface of a class into another interface clients expect";

        public void Run()
        {
            IPrinter printer = new ConsolePrinterAdapter(new ColorConsolePrinter());
            printer.Print($"{Environment.NewLine}Hi, this is printed using adapter.{Environment.NewLine}");
        }
    }

    internal interface IPrinter
    {
        void Print(string text);
    }

    internal class ConsolePrinterAdapter : IPrinter
    {
        private readonly ColorConsolePrinter consolePrinter;

        public ConsolePrinterAdapter(ColorConsolePrinter consolePrinter) => this.consolePrinter = consolePrinter;

        public void Print(string text)
        {
            consolePrinter.ForegroundColor ??= ConsoleColor.Green;
            consolePrinter.BackgroundColor ??= ConsoleColor.Black;

            consolePrinter.Write(text);
        }
    }

    public class ColorConsolePrinter
    {
        public ConsoleColor? ForegroundColor { get; set; }
        public ConsoleColor? BackgroundColor { get; set; }

        public void Write(string message)
        {
            ForegroundColor = ForegroundColor ?? throw new ArgumentNullException(nameof(ForegroundColor));
            BackgroundColor = BackgroundColor ?? throw new ArgumentNullException(nameof(BackgroundColor));

            Console.ForegroundColor = ForegroundColor!.Value;
            Console.BackgroundColor = BackgroundColor!.Value;

            Console.WriteLine(message);

            Console.ResetColor();
        }
    }
}
