namespace Demo.Common.Library
{
    public abstract class DemoBase : IDisposable
    {
        private bool disposedValue;

        public abstract string Name { get; }
        public abstract string ShortSummary { get; }
        public IConsoleWriter ConsoleWriter { get; set; } = new ConsoleWriter();

        public abstract Task Run();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ConsoleWriter = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DemoBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
