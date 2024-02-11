namespace Design_Patterns.Behavioral
{
    public class VisitorDemo : IDemonstratePattern
    {
        public string Name => "Behavioral.Visitor";

        public string ShortSummary => "adding new behaviors to existing class hierarchy without altering any existing code.";

        public void Run()
        {
            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(new Book("Book 1", 30.0));
            cart.AddItem(new Electronic("Laptop", 800.0));
            cart.AddItem(new Clothing("Shirt", 25.0));

            DiscountCalculator discountCalculator = new DiscountCalculator();
            double totalCost = cart.CalculateTotalCost(discountCalculator);

            Console.WriteLine($"Total cost after discounts: {totalCost:C}");
        }

        interface IDiscountVisitor
        {
            void Visit(Book book);
            void Visit(Electronic electronic);
            void Visit(Clothing clothing);
        }

        interface ICartItem
        {
            double Accept(IDiscountVisitor visitor);
        }

        class Book : ICartItem
        {
            public string Title { get; }
            public double Price { get; set; }

            public Book(string title, double price)
            {
                Title = title;
                Price = price;
            }

            public double Accept(IDiscountVisitor visitor)
            {
                visitor.Visit(this);
                return Price;
            }
        }

        class Electronic : ICartItem
        {
            public string Model { get; }
            public double Price { get; set; }

            public Electronic(string model, double price)
            {
                Model = model;
                Price = price;
            }

            public double Accept(IDiscountVisitor visitor)
            {
                visitor.Visit(this);
                return Price;
            }
        }

        class Clothing : ICartItem
        {
            public string Type { get; }
            public double Price { get; set; }

            public Clothing(string type, double price)
            {
                Type = type;
                Price = price;
            }

            public double Accept(IDiscountVisitor visitor)
            {
                visitor.Visit(this);
                return Price;
            }
        }

        class DiscountCalculator : IDiscountVisitor
        {
            public void Visit(Book book)
            {
                book.Price *= 0.9;
                Console.WriteLine($"Applying 10% discount to book: {book.Title}");
            }

            public void Visit(Electronic electronic)
            {
                electronic.Price *= .95;
                Console.WriteLine($"Applying 5% discount to electronic: {electronic.Model}");
            }

            public void Visit(Clothing clothing)
            {
                clothing.Price *= .8;
                Console.WriteLine($"Applying 20% discount to clothing: {clothing.Type}");
            }
        }

        class ShoppingCart
        {
            private readonly List<ICartItem> items = [];

            public void AddItem(ICartItem item) => items.Add(item);

            public double CalculateTotalCost(IDiscountVisitor discountVisitor)
            {
                double totalCost = 0;
                foreach (ICartItem item in items)
                {
                    totalCost += item.Accept(discountVisitor);
                }
                return totalCost;
            }
        }
    }
}
