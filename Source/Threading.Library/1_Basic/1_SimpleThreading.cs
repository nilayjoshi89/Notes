using Demo.Common.Library;

namespace Threading.Library
{
    public class SimpleThreading : DemoBase
    {
        public override string Name => "Threading.Simple";

        public override string ShortSummary => "Demonstrates the spill over effect in theading";

        public override Task Run()
        {
            return Task.Run(() =>
            {
                Thread t1 = new Thread((object cw) =>
                {
                    IConsoleWriter consoleWriter = (IConsoleWriter)cw;
                    for (int i = 0; i < 1000; i++)
                    {
                        consoleWriter.Write("[fuchsia]y[/]");
                    }
                });

                t1.Start(ConsoleWriter);

                for (int i = 0; i < 100; i++) ConsoleWriter.Write("[yellow]x[/]");

                return Task.CompletedTask;
            });
        }
    }
}
