using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadSyncMonitor : DemoBase
    {
        public override string Name => "ThreadSync.Monitor";

        public override string ShortSummary => "Sync with help of Monitor.Enter/Exit";

        public override async Task Run()
        {
            ConsoleWriter.WriteLine("Thread Safe demo:");

            ThreadSafe safeInstance = new ThreadSafe();

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
            readonly object _locker = new object();
            private readonly int _val1 = 1;
            private int _val2 = 1;
            bool lockAquired = false;

            public void Go(IConsoleWriter cw)
            {
                Monitor.Enter(_locker, ref lockAquired);
                try
                {
                    if (_val2 != 0)
                    {
                        Thread.Sleep(1000);
                        cw.WriteLine((_val1 / _val2).ToString());
                    }
                    _val2 = 0;
                }
                finally
                {
                    if(lockAquired)
                    {
                        Monitor.Exit(_locker);
                    }
                }
            }
        }
    }
}
