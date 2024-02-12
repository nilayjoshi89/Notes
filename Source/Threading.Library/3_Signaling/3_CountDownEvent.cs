using Demo.Common.Library;

namespace Threading.Library
{
    public class ThreadSignalingCountDownEvent : DemoBase
    {
        public override string Name => "ThreadSignaling.CountDownEvent";
        public override string ShortSummary => "Signaling between threads. Lets you wait on more than one thread.";

        public override Task Run()
        {
            List<Task> orderTasks = [];
            for (int i = 1; i <= 10; i++)
            {
                int temp = i;
                orderTasks.Add(Order.Generate(temp, ConsoleWriter).Prepare());
            }

            return Task.WhenAll(orderTasks);
        }

        class Order
        {
            readonly CountdownEvent itemCountDown = new CountdownEvent(3);

            public int Id { get; private set; }
            public Curry Curry { get; private set; }
            public RotiBread Roti { get; private set; }
            public Drink Side { get; private set; }
            public IConsoleWriter ConsoleWriter { get; private set; }
            public static Order Generate(int id, IConsoleWriter consoleWriter)
            {
                return new Order()
                {
                    Id = id,
                    ConsoleWriter = consoleWriter,
                    Curry = GetRandomCurry(id, consoleWriter),
                    Roti = GetRandomRoti(id, consoleWriter),
                    Side = GetRandomSide(id, consoleWriter)
                };
            }
            private static Drink GetRandomSide(int id, IConsoleWriter cw)
            {
                int option = Random.Shared.Next(0, 1000);
                switch (option % 2)
                {
                    case 0:
                        return new CokeZero(cw) { OrderId = id };
                    case 1:
                        return new Lemonade(cw) { OrderId = id };
                }

                throw new NotSupportedException();
            }
            private static RotiBread GetRandomRoti(int id, IConsoleWriter cw)
            {
                int option = Random.Shared.Next(0, 1000);
                switch (option % 3)
                {
                    case 0:
                        return new Naan(cw) { OrderId = id };
                    case 1:
                        return new Roti(cw) { OrderId = id };
                    case 2:
                        return new StuffedParatha(cw) { OrderId = id };
                }

                throw new NotSupportedException();
            }
            private static Curry GetRandomCurry(int id, IConsoleWriter cw)
            {
                int option = Random.Shared.Next(0, 1000);
                switch (option % 3)
                {
                    case 0:
                        return new PaneerTikka(cw) { OrderId = id };
                    case 1:
                        return new VegMix(cw) { OrderId = id };
                    case 2:
                        return new PaneerBhurji(cw) { OrderId = id };
                }

                throw new NotSupportedException();
            }

            public async Task Prepare()
            {
                ConsoleWriter.WriteLine($"Starting Order {Id}; {Curry.Name}, {Roti.Name}, {Side.Name};");
                _ = Curry.Prepare(() => itemCountDown.Signal());
                _ = Roti.Prepare(() => itemCountDown.Signal());
                _ = Side.Prepare(() => itemCountDown.Signal());

                await Task.Run(() => itemCountDown.Wait());
                ConsoleWriter.WriteLine($"Finished Order {Id}; {Curry.Name}, {Roti.Name}, {Side.Name};");
            }
        }

        abstract class FoodItem
        {
            private readonly IConsoleWriter cw;
            public FoodItem(IConsoleWriter cw) => this.cw = cw;
            public int OrderId { get; set; }
            public abstract string Name { get; }
            public abstract int PrepareTime { get; }
            public async Task Prepare(Action signal)
            {
                cw.WriteLine($"Preparing {Name} for {OrderId}.");
                await Task.Delay(PrepareTime * 200);
                cw.WriteLine($"Finished preparing {Name} for {OrderId}.");
                signal();
            }
        }
        abstract class Curry : FoodItem
        {
            public Curry(IConsoleWriter cw) : base(cw) { }
        }

        abstract class RotiBread : FoodItem
        {
            public RotiBread(IConsoleWriter cw) : base(cw) { }
        }

        abstract class Drink : FoodItem
        {
            public Drink(IConsoleWriter cw) : base(cw) { }
        }

        class PaneerTikka : Curry
        {
            public PaneerTikka(IConsoleWriter cw) : base(cw)
            {
            }

            public override string Name => "Paneed Tikka";

            public override int PrepareTime => 7;
        }
        class VegMix : Curry
        {
            public VegMix(IConsoleWriter cw) : base(cw)
            {
            }

            public override string Name => "Veg Mix";

            public override int PrepareTime => 6;
        }
        class PaneerBhurji : Curry
        {
            public PaneerBhurji(IConsoleWriter cw) : base(cw)
            {
            }

            public override string Name => "Paneer Bhurji";

            public override int PrepareTime => 4;
        }

        class Naan : RotiBread
        {
            public Naan(IConsoleWriter cw) : base(cw)
            {
            }

            public override string Name => "Naan";

            public override int PrepareTime => 2;
        }
        class Roti : RotiBread
        {
            public Roti(IConsoleWriter cw) : base(cw)
            {
            }

            public override string Name => "Roti";

            public override int PrepareTime => 3;
        }
        class StuffedParatha : RotiBread
        {
            public StuffedParatha(IConsoleWriter cw) : base(cw)
            {
            }

            public override string Name => "Stuffed Paratha";

            public override int PrepareTime => 5;
        }

        class CokeZero : Drink
        {
            public CokeZero(IConsoleWriter cw) : base(cw)
            {
            }

            public override string Name => "Coke Zero";

            public override int PrepareTime => 1;
        }
        class Lemonade : Drink
        {
            public Lemonade(IConsoleWriter cw) : base(cw)
            {
            }

            public override string Name => "Lemonade";

            public override int PrepareTime => 2;
        }
    }
}
