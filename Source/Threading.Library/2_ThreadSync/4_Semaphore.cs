using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadSyncSemaphore : DemoBase
    {
        readonly SemaphoreSlim _sem = new SemaphoreSlim(3); // Capacity of 3

        public override string Name => "ThreadSync.Semaphore";

        public override string ShortSummary => "Sync with help of Semaphore (Computer wide), SemaphoreSlim (local to process); Club like stagged entry";

        public override Task Run()
        {
            List<Task> tasks = [];
            for (int i = 1; i <= 5; i++)
            {
                int temp = i;
                tasks.Add(Task.Run(() => Enter(temp)));
            }

            return Task.WhenAll(tasks);
        }

        void Enter(int id)
        {
            ConsoleWriter.WriteLine(id + " wants to enter");
            _sem.Wait();
            ConsoleWriter.WriteLine(id + " is in!"); // Only three threads
            Thread.Sleep(1000 * id); // can be here at
            ConsoleWriter.WriteLine(id + " is leaving"); // a time.
            _ = _sem.Release();
        }
    }
}
