using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadSignalingAutoResetEvent : DemoBase
    {
        public override string Name => "ThreadSignaling.AutoResetEvent";
        public override string ShortSummary => "Signaling between threads (Computer wide). Like a ticket turnstile. If Producer and Consumer speed is different, may miss few updates.";

        public override async Task Run()
        {
            await new OneWaySignaling(ConsoleWriter).Run();
            
            ConsoleWriter.WriteLine("");
            
            await new TwoWaySignaling(ConsoleWriter).Run();
        }

        class OneWaySignaling
        {
            readonly AutoResetEvent waitHandler = new AutoResetEvent(false);
            int data;
            private readonly IConsoleWriter consoleWriter;

            public OneWaySignaling(IConsoleWriter consoleWriter) => this.consoleWriter = consoleWriter;
            public Task Run()
            {
                consoleWriter.WriteLine("One way signaling demo (with package drop)");
                return Task.WhenAll(Producer(), Consumer());
            }
            public async Task Producer()
            {
                for (int i = 0; i < 5; i++)
                {
                    consoleWriter.WriteLine("Producing data...");
                    await Task.Delay(500);
                    data = Random.Shared.Next();
                    consoleWriter.WriteLine($"Produced data {data}");
                    _ = waitHandler.Set();
                }
            }
            public async Task Consumer()
            {
                for (int i = 0; i < 5; i++)
                {
                    consoleWriter.WriteLine($"Waiting for data...");
                    bool signalReceived = waitHandler.WaitOne(4000);
                    if (!signalReceived)
                    {
                        consoleWriter.WriteLine($"Consumer timed out! May have missed few packages.");

                        return;
                    }

                    consoleWriter.WriteLine($"Consuming data {data}");
                    await Task.Delay(1000);
                }
            }
        }

        class TwoWaySignaling
        {
            readonly AutoResetEvent producerWaitHandler = new AutoResetEvent(true);
            readonly AutoResetEvent consumerWaitHandler = new AutoResetEvent(false);
            int data;
            private readonly IConsoleWriter consoleWriter;

            public TwoWaySignaling(IConsoleWriter consoleWriter) => this.consoleWriter = consoleWriter;
            public Task Run()
            {
                consoleWriter.WriteLine("Two way signaling demo");
                return Task.WhenAll(Producer(), Consumer());
            }
            public async Task Producer()
            {
                for (int i = 0; i < 5; i++)
                {
                    consoleWriter.WriteLine("Producing data...");
                    await Task.Delay(500);
                    _ = producerWaitHandler.WaitOne();
                    data = Random.Shared.Next();
                    consoleWriter.WriteLine($"Produced data {data}");
                    _ = consumerWaitHandler.Set();
                }
            }
            public async Task Consumer()
            {
                for (int i = 0; i < 5; i++)
                {
                    consoleWriter.WriteLine($"Waiting for data...");
                    bool signalReceived = consumerWaitHandler.WaitOne(4000);
                    if (!signalReceived)
                    {
                        consoleWriter.WriteLine($"Consumer timed out! May have missed few packages.");
                        return;
                    }
                    consoleWriter.WriteLine($"Consuming data {data}");
                    await Task.Delay(1000);
                    _ = producerWaitHandler.Set();
                }
            }
        }
    }
}
