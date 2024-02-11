using Design_Patterns;
using Design_Patterns.CreationalPatterns;
using Design_Patterns.StructuralPatterns;

internal class Program
{
    private static void Main(string[] args)
    {
        IList<IDemonstratePattern> patterns = GetPatterns();

        int i = 1;
        foreach (IDemonstratePattern pattern in patterns)
        {
            Console.WriteLine($"------ Start ({i} of {patterns.Count}) ----------------------");
            Console.WriteLine($"{pattern.Name} : {pattern.ShortSummary}");
            Console.WriteLine();

            pattern.Run();

            Console.WriteLine();
            Console.WriteLine($"------ End ({i++} of {patterns.Count}) ----------------------");

            _ = Console.ReadKey();
        }
    }

    private static IList<IDemonstratePattern> GetPatterns() => [new FactoryDemo(),
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
        new ProxyDemo()];
}