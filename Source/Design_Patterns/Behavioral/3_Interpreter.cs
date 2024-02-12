using Demo.Common.Library;

namespace Design_Patterns.Behavioral
{
    public class InterpreterDemo : DemoBase
    {
        public override string Name => "Behavioral.Interpreter";

        public override string ShortSummary => @"A way to include language elements in a program.";

        public override Task Run()
        {
            AbstractExpression expression = new MultiplyExpression(new AddExpression(new NumberExpression(3), new NumberExpression(2)), new NumberExpression(6));

            int result = expression.Interpret();

            Console.WriteLine("Result: " + result);
            return Task.CompletedTask;
        }
    }

    abstract class AbstractExpression
    {
        public abstract int Interpret();
    }

    class NumberExpression : AbstractExpression
    {
        private readonly int _number;

        public NumberExpression(int number) => _number = number;

        public override int Interpret() => _number;
    }

    class AddExpression : AbstractExpression
    {
        private readonly AbstractExpression _left;
        private readonly AbstractExpression _right;

        public AddExpression(AbstractExpression left, AbstractExpression right)
        {
            _left = left;
            _right = right;
        }

        public override int Interpret() => _left.Interpret() + _right.Interpret();
    }

    class SubtractExpression : AbstractExpression
    {
        private readonly AbstractExpression _left;
        private readonly AbstractExpression _right;

        public SubtractExpression(AbstractExpression left, AbstractExpression right)
        {
            _left = left;
            _right = right;
        }

        public override int Interpret() => _left.Interpret() - _right.Interpret();
    }

    class MultiplyExpression : AbstractExpression
    {
        private readonly AbstractExpression _left;
        private readonly AbstractExpression _right;

        public MultiplyExpression(AbstractExpression left, AbstractExpression right)
        {
            _left = left;
            _right = right;
        }

        public override int Interpret() => _left.Interpret() * _right.Interpret();
    }

    class DivideExpression : AbstractExpression
    {
        private readonly AbstractExpression _left;
        private readonly AbstractExpression _right;

        public DivideExpression(AbstractExpression left, AbstractExpression right)
        {
            _left = left;
            _right = right;
        }

        public override int Interpret()
        {
            int rightValue = _right.Interpret();
            return rightValue != 0 ? _left.Interpret() / rightValue : throw new DivideByZeroException("Cannot divide by zero.");
        }
    }
}
