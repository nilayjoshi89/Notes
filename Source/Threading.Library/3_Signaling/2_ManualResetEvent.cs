using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadSignalingManualResetEvent : DemoBase
    {
        readonly int showDurationSec = 5;
        readonly int totalShows = 5;
        readonly ManualResetEvent resetEvent = new ManualResetEvent(false);

        public override string Name => "ThreadSignaling.ManualResetEvent";
        public override string ShortSummary => "Signaling between threads (Computer wide). Like an ordinary gate. Calling Set opens the gate, allowing any number of threads calling WaitOne to be let through. Calling Reset closes the gate.";

        public override Task Run()
        {
            ParkGate parkInstance = new ParkGate(ConsoleWriter, totalShows, showDurationSec,
                () =>
                {
                    _ = resetEvent.Set();
                    _ = resetEvent.Reset();
                });

            return Task.WhenAll(VisitTourist(), parkInstance.Run());
        }

        private async Task VisitTourist()
        {
            List<Task> tasks = [];
            int id = 1;

            for (int j = 1; j <= 50; j++)
            {
                int temp = id++;
                tasks.Add(Task.Run(() => new Tourist(ConsoleWriter, temp, (id) =>
                {
                    ConsoleWriter.WriteLine($"{id} waiting.");
                    return resetEvent.WaitOne(TimeSpan.FromSeconds(10));
                }).Run()));

                await Task.Delay(Random.Shared.Next(100, 800));
            }

            await Task.WhenAll(tasks);
        }

        class ParkGate
        {
            private readonly IConsoleWriter consoleWriter;
            private readonly int shows;
            private readonly int duration;
            private readonly Action notifyAction;

            public ParkGate(IConsoleWriter consoleWriter, int shows, int duration, Action notifyAction)
            {
                this.consoleWriter = consoleWriter;
                this.shows = shows;
                this.duration = duration;
                this.notifyAction = notifyAction;
            }

            public async Task Run()
            {
                await Task.Delay(1000);

                for (int showIndex = 1; showIndex <= shows; showIndex++)
                {
                    notifyAction();
                    consoleWriter.WriteLine($"Starting show {showIndex} of {shows}. Admitted tourists.");
                    await Task.Delay(TimeSpan.FromSeconds(duration));
                    consoleWriter.WriteLine($"Show {showIndex} of {shows} finished.");
                }
            }
        }

        class Tourist
        {
            private readonly IConsoleWriter consoleWriter;
            private readonly int id;
            private readonly Func<int, bool> waitHandler;

            public Tourist(IConsoleWriter consoleWriter, int id, Func<int, bool> waitHandler)
            {
                this.consoleWriter = consoleWriter;
                this.id = id;
                this.waitHandler = waitHandler;
            }

            public void Run()
            {
                bool success = waitHandler(id);

                if (!success)
                {
                    consoleWriter.WriteLine($"{id} failed to enter.");
                    return;
                }

                consoleWriter.WriteLine($"{id} entered.");
            }
        }
    }
}
