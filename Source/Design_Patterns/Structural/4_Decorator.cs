
using Demo.Common.Library;

namespace Design_Patterns.StructuralPatterns
{
    public class DecoratorDemo : DemoBase
    {
        public override string Name => "Structural.Decorator";

        public override string ShortSummary => @"adding new behaviors to objects dynamically by placing them inside special wrapper objects, called decorators.
    - attaches additional responsibilities to an object dynamically.
	- New Abstract class from original; wrapping original abstract implementation with additional code";

        public override Task Run()
        {
            ITextFormatter plainTextFormatter = new PlainTextFormatter();
            Console.WriteLine(plainTextFormatter.Format("Hello, Decorator Pattern!"));

            ITextFormatter boldTextFormatter = new BoldTextDecorator(plainTextFormatter);
            Console.WriteLine(boldTextFormatter.Format("Hello, Decorator Pattern!"));

            ITextFormatter italicTextFormatter = new ItalicTextDecorator(boldTextFormatter);
            Console.WriteLine(italicTextFormatter.Format("Hello, Decorator Pattern!"));

            return Task.CompletedTask;
        }
    }
    interface ITextFormatter
    {
        string Format(string text);
    }

    class PlainTextFormatter : ITextFormatter
    {
        public string Format(string text) => $"Plain Text: {text}";
    }

    abstract class TextDecorator : ITextFormatter
    {
        protected ITextFormatter decoratedTextFormatter;

        public TextDecorator(ITextFormatter decoratedTextFormatter) => this.decoratedTextFormatter = decoratedTextFormatter;

        public virtual string Format(string text) => decoratedTextFormatter.Format(text);
    }

    class BoldTextDecorator : TextDecorator
    {
        public BoldTextDecorator(ITextFormatter decoratedTextFormatter) : base(decoratedTextFormatter)
        {
        }

        public override string Format(string text) => $"Bold: {base.Format(text)}";
    }

    class ItalicTextDecorator : TextDecorator
    {
        public ItalicTextDecorator(ITextFormatter decoratedTextFormatter) : base(decoratedTextFormatter)
        {
        }

        public override string Format(string text) => $"Italic: {base.Format(text)}";
    }
}
