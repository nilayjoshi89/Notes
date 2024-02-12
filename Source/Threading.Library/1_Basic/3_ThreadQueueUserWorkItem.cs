using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadPoolQueueUserWorkItem : DemoBase
    {
        public override string Name => "Threading.ThreadPoolQueueUserWorkItem";

        public override string ShortSummary => "Usage of ThreadPool.QueueUserWorkItem";

        public override Task Run()
        {
            DateTime data = DateTime.Now;
            _ = ThreadPool.QueueUserWorkItem(CallbackMethod, ConsoleWriter);
            ConsoleWriter.WriteLine("No way to track/wait for Queued item.");
            return Task.CompletedTask;
        }

        private void CallbackMethod(object? state)
        {
            IConsoleWriter cw = (IConsoleWriter)state;
            cw?.WriteLine("Entered thread. Before sleep.");
            Thread.Sleep(2000);
            cw?.WriteLine("Finished thread.");
        }
    }
}
