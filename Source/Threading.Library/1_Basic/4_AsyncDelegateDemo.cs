using Demo.Common.Library;

namespace Threading.Library
{
    //Not supported in dotnet core
    public abstract class AsyncDelegateDemo : DemoBase
    {
        public override string Name => "Threading.AsyncDelegate";

        public override string ShortSummary => "Usage of Async Delegate - BegingInvoke, EndInvoke";

        public override Task Run()
        {
            Func<string, int> myFunc = new Func<string, int>((n) =>
            {
                ConsoleWriter.WriteLine("Async delegate start");
                Thread.Sleep(2000);
                ConsoleWriter.WriteLine("Async delegate End");
                return 5;
            });

            IAsyncResult asyncResult = myFunc.BeginInvoke("Nilay", null, null);

            ConsoleWriter.WriteLine("Do something in main thread.");

            int result = myFunc.EndInvoke(asyncResult);

            ConsoleWriter.WriteLine($"Result from thread: {result}");

            return Task.CompletedTask;
        }
    }
}
