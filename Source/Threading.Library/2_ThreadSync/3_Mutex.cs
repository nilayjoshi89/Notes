using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadSyncMutex : DemoBase
    {
        public override string Name => "ThreadSync.Mutex";

        public override string ShortSummary => "Sync with help of Mutex (Computer wide)";

        public override async Task Run()
        {
            ConsoleWriter.WriteLine("Thread Safe demo:");

            ThreadSafe safeInstance = new ThreadSafe(ConsoleWriter);

            Task t1 = Task.Run(async () =>
            {
                await Task.Delay(100);
                safeInstance.Go(ConsoleWriter);
            });
            Task t2 = Task.Run(() => safeInstance.Go(ConsoleWriter));

            await Task.WhenAll(t1, t2);
        }

        class ThreadSafe
        {
            private readonly IConsoleWriter consoleWriter;

            public ThreadSafe(IConsoleWriter consoleWriter) => this.consoleWriter = consoleWriter;

            public void Go(IConsoleWriter cw)
            {
                using (Mutex mutex = new Mutex(false, "demoapp.threadsync"))
                {
                    // Wait a few seconds if contended, in case another instance
                    // of the program is still in the process of shutting down.
                    if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                    {
                        consoleWriter.WriteLine("Another instance of the app is running. Bye!");
                        return;
                    }

                    consoleWriter.WriteLine("Lock acquired successfully.");
                    Thread.Sleep(5000);
                    consoleWriter.WriteLine("End of Execution.");
                }
            }
        }
    }
}
