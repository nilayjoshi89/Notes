namespace Design_Patterns.Behavioral
{
    public class StrategyDemo : IDemonstratePattern
    {
        public string Name => "Behavioral.Strategy";

        public string ShortSummary => "The Strategy design pattern defines a family of algorithms, encapsulate each one, and make them interchangeable. This pattern lets the algorithm vary independently from clients that use it.";

        public void Run()
        {
            PaymentProcessor paymentProcessor = new PaymentProcessor();

            paymentProcessor.SetPaymentStrategy(new CreditCardPaymentStrategy("1234-5678-9012-3456", "12/23", "123"));
            paymentProcessor.ProcessPayment(100.00f);

            paymentProcessor.SetPaymentStrategy(new PayPalPaymentStrategy("example@example.com"));
            paymentProcessor.ProcessPayment(50.00f);

            paymentProcessor.SetPaymentStrategy(new BankTransferPaymentStrategy("123-456-789"));
            paymentProcessor.ProcessPayment(75.00f);
        }

        interface IPaymentStrategy
        {
            void ProcessPayment(float amount);
        }

        class CreditCardPaymentStrategy : IPaymentStrategy
        {
            private readonly string _creditCardNumber;
            private readonly string _expirationDate;
            private readonly string _cvv;

            public CreditCardPaymentStrategy(string creditCardNumber, string expirationDate, string cvv)
            {
                _creditCardNumber = creditCardNumber;
                _expirationDate = expirationDate;
                _cvv = cvv;
            }

            public void ProcessPayment(float amount) => Console.WriteLine($"Processing credit card payment of {amount:C} using {_creditCardNumber}");
        }

        class PayPalPaymentStrategy : IPaymentStrategy
        {
            private readonly string _email;

            public PayPalPaymentStrategy(string email) => _email = email;

            public void ProcessPayment(float amount) => Console.WriteLine($"Processing PayPal payment of {amount:C} using {_email}");
        }

        class BankTransferPaymentStrategy : IPaymentStrategy
        {
            private readonly string _accountNumber;

            public BankTransferPaymentStrategy(string accountNumber) => _accountNumber = accountNumber;

            public void ProcessPayment(float amount) => Console.WriteLine($"Processing bank transfer payment of {amount:C} to {_accountNumber}");
        }

        class PaymentProcessor
        {
            private IPaymentStrategy _paymentStrategy;

            public void SetPaymentStrategy(IPaymentStrategy paymentStrategy) => _paymentStrategy = paymentStrategy;

            public void ProcessPayment(float amount)
            {
                if (_paymentStrategy == null)
                {
                    Console.WriteLine("Please set a payment strategy first.");
                    return;
                }

                _paymentStrategy.ProcessPayment(amount);
            }
        }
    }
}
