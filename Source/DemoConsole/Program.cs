using Design_Patterns;
using Design_Patterns.Behavioral;
using Design_Patterns.CreationalPatterns;
using Design_Patterns.StructuralPatterns;

using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        IList<IDemonstratePattern> patterns = GetPatterns();
        var patternSelection = new SelectionPrompt<string>()
                                .Title("[Bold]Select any pattern:[/]")
                                .PageSize(30)
                                .AddChoices(patterns.Select(p => p.Name).ToArray());
        while (true)
        {
            AnsiConsole.Clear();
            var patternNameToShow = AnsiConsole.Prompt(patternSelection);
            var selectedPattern = patterns.First(p => p.Name == patternNameToShow);

            selectedPattern.Run();

            AnsiConsole.MarkupLine($"{Environment.NewLine}[Gray Bold]Press any key to continue...[/]");
            Console.ReadKey();
        }
    }

    private static IList<IDemonstratePattern> GetPatterns() =>
        [
            new FactoryDemo(),
            new AbstractFactoryDemo(),
            new BuilderDemo(),
            new PrototypeDemo(),
            new SingletonDemo(),
            new AdapterDemo(),
            new BridgeDemo(),
            new CompositeDemo(),
            new DecoratorDemo(),
            new FacadeDemo(),
            new FlyweightDemo(),
            new ProxyDemo(),
            new ChainOfResponsibilityDemo(),
            new CommandDemo(),
            new InterpreterDemo(),
            new IteratorDemo(),
            new MediatorDemo(),
            new MementoDemo(),
            new ObserverDemo(),
            new StateDemo(),
            new StrategyDemo(),
            new TemplateDemo(),
            new VisitorDemo()
        ];
}