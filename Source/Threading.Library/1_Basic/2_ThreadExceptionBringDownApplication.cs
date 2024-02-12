using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadExceptionBringDownApplication : DemoBase
    {
        public override string Name => "Threading.ThreadException";

        public override string ShortSummary => "Unhandled exception will bring down the application.";

        public override Task Run()
        {
            return Task.Run(() =>
            {
                Thread t1 = new Thread((object cw) =>
                {
                    IConsoleWriter consoleWriter = (IConsoleWriter)cw;
                    consoleWriter.WriteLine("Before throwing exception. Press any key to continue.");
                    consoleWriter.ReadKey();
                    throw new Exception("Some error in thread");
                });

                t1.Start(ConsoleWriter);
                t1.Join();
            });
        }
    }
}
