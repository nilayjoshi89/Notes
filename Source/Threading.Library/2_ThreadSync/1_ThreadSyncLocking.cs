using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadSyncLocking : DemoBase
    {
        public override string Name => "ThreadSync.Locking";

        public override string ShortSummary => "Sync with help of lock";

        public override async Task Run()
        {
            ConsoleWriter.WriteLine("Thread Unsafe demo:");

            ThreadUnsafe unsafeInstance = new ThreadUnsafe();

            Task t1 = Task.Run(async () =>
            {
                await Task.Delay(100);
                unsafeInstance.Go(ConsoleWriter);
            });
            Task t2 = Task.Run(() => unsafeInstance.Go(ConsoleWriter));

            await Task.WhenAll(t1, t2);

            ConsoleWriter.WriteLine("Thread Safe demo:");

            ThreadSafe safeInstance = new ThreadSafe();

            t1 = Task.Run(async () =>
            {
                await Task.Delay(100);
                safeInstance.Go(ConsoleWriter);
            });
            t2 = Task.Run(() => safeInstance.Go(ConsoleWriter));

            await Task.WhenAll(t1, t2);
        }

        class ThreadUnsafe
        {
            private readonly int _val1 = 1;
            private int _val2 = 1;

            public void Go(IConsoleWriter cw)
            {
                try
                {
                    if (_val2 != 0)
                    {
                        Thread.Sleep(1000);
                        cw.WriteLine((_val1 / _val2).ToString());
                    }
                    _val2 = 0;
                }
                catch
                {
                    cw.WriteLine("One of the thread will throw this exception");
                }
            }
        }

        class ThreadSafe
        {
            readonly object _locker = new object();
            private readonly int _val1 = 1;
            private int _val2 = 1;

            public void Go(IConsoleWriter cw)
            {
                lock (_locker)
                {
                    if (_val2 != 0)
                    {
                        Thread.Sleep(1000);
                        cw.WriteLine((_val1 / _val2).ToString());
                    }
                    _val2 = 0;
                }
            }
        }
    }
}
