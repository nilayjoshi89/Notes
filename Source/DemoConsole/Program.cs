using Demo.Common.Library;

using Design_Patterns.CreationalPatterns;

using Spectre.Console;

using Threading.Library;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IDictionary<string, DemoBase> demos = GetDemos();

        var demoSelection = new SelectionPrompt<string>()
                                .Title("[bold green]Select any item:[/]")
                                .PageSize(10)
                                .AddChoices(demos.Select(p => p.Key).ToArray());

        IConsoleWriter cw = new ConsoleWriter();
        while (true)
        {
            AnsiConsole.Clear();
            var name = AnsiConsole.Prompt(demoSelection);
            var instance = demos[name];

            cw.WriteHeaderLine(instance.Name);
            cw.WriteHeader2Line(instance.ShortSummary);
            cw.WriteLine("");

            await instance.Run();
            instance.Dispose();

            demos[name] = (DemoBase)Activator.CreateInstance(instance.GetType())!;

            cw.WriteLine($"{Environment.NewLine}[bold gray]Press any key to continue...[/]");
            cw.ReadKey();
        }
    }

    private static IDictionary<string, DemoBase> GetDemos()
    {

        var forceLoadPatternCollection = typeof(SimpleThreading);
        forceLoadPatternCollection = typeof(FactoryDemo);

        IDictionary<string, DemoBase> patterns = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                    .Where(p => typeof(DemoBase).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                    .Select(t =>
                    {
                        DemoBase instance = (DemoBase)Activator.CreateInstance(t)!;
                        return new KeyValuePair<string, DemoBase>(instance.Name, instance);
                    })
                    .ToDictionary();
        return patterns;
    }
}