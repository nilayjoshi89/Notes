
using Demo.Common.Library;

namespace Design_Patterns.StructuralPatterns
{
    public class BridgeDemo : DemoBase
    {
        public override string Name => "Structural.Bridge";

        public override string ShortSummary => @"lets you split a large class or a set of closely related classes into two separate hierarchies—abstraction and implementation—which can be developed independently of each other.
- decouples an abstraction from its implementation so that the two can vary independently.
	- Shape; Rectangle; Square; Rounded Rectangle; Rounded Square; Pointed Rectangle; Pointed Square;
	- Shape; Rectangle, Square; Style; Rounded; Pointed
		- Style Part of Shape";

        public override Task Run()
        {
            Console.WriteLine("Sending Short message over Email");
            Message shortMessageOverEmail = new ShortMessage(new EmailSender());
            shortMessageOverEmail.Send();

            Console.WriteLine("Sending Long email over SMS");
            Message longEmailOverSms = new LongEmail(new SMSSender());
            longEmailOverSms.Send();

            return Task.CompletedTask;
        }
    }

    interface IMessageSender
    {
        void SendMessage(string message);
    }

    class EmailSender : IMessageSender
    {
        public void SendMessage(string message) => Console.WriteLine($"Sending email: {message}");
    }

    class SMSSender : IMessageSender
    {
        public void SendMessage(string message) => Console.WriteLine($"Sending SMS: {message}");
    }

    abstract class Message
    {
        protected IMessageSender messageSender;

        protected Message(IMessageSender messageSender) => this.messageSender = messageSender;

        public abstract void Send();
    }

    class ShortMessage : Message
    {
        public ShortMessage(IMessageSender messageSender) : base(messageSender)
        {
        }

        public override void Send()
        {
            Console.WriteLine("Sending a short message:");
            messageSender.SendMessage("This is a short message.");
        }
    }

    class LongEmail : Message
    {
        public LongEmail(IMessageSender messageSender) : base(messageSender)
        {
        }

        public override void Send()
        {
            Console.WriteLine("Sending a long email:");
            messageSender.SendMessage("This is a long email with lots of details.");
        }
    }
}
