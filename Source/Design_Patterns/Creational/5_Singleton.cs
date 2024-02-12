
using Demo.Common.Library;

namespace Design_Patterns.CreationalPatterns
{

    public class SingletonDemo : DemoBase
    {
        public override string Name => "Cretional.Singleton";

        public override string ShortSummary => "Lets you ensure that a class has only one instance, while providing a global access point to this instance.";

        public override Task Run()
        {
            var i1 = SingleInstanceClass.Instance;
            var i2 = SingleInstanceClass.Instance;

            Console.WriteLine($"i1 == i2 -> {i1 == i2}");

            return Task.CompletedTask;
        }
    }

    public class SingleInstanceClass
    {
        private static object lockObj = new object();
        private static SingleInstanceClass instance;

        private SingleInstanceClass() { }
        public static SingleInstanceClass Instance => GetInstance();

        private static SingleInstanceClass GetInstance()
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new SingleInstanceClass();
                    }
                }
            }

            return instance;
        }
    }
}
