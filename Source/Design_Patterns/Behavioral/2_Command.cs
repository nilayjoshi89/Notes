namespace Design_Patterns.Behavioral
{
    public class CommandDemo : IDemonstratePattern
    {
        public string Name => "Behavioral.Command";

        public string ShortSummary => @"encapsulates a request as an object, thereby letting you parameterize clients with different requests, queue or log requests, and support undoable operations.";

        public void Run()
        {
            CalculatorSession calc = new CalculatorSession();
            calc.Compute('+', 100);
            calc.Compute('-', 50);
            calc.Compute('*', 10);
            calc.Compute('/', 2);

            calc.Undo(3);

            calc.Redo(3);

            calc.Undo(3);

            calc.Compute('+', 5);

            calc.Redo(2);
        }
    }

    public abstract class Command
    {
        public abstract void Do();
        public abstract void Undo();
    }
    public class CalculatorCommand : Command
    {
        readonly Calculator calculator;
        public CalculatorCommand(Calculator calculator,
            char @operator, int operand)
        {
            this.calculator = calculator;
            Operator = @operator;
            Operand = operand;
        }

        public char Operator { get; set; }
        public int Operand { get; set; }

        public override void Do() => calculator.Operation(Operator, Operand);
        public override void Undo() => calculator.Operation(GetUndoOperator(Operator), Operand);
        private char GetUndoOperator(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default:
                    throw new
             ArgumentException("@operator");
            }
        }
    }
    public class Calculator
    {
        int curr = 0;
        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': curr += operand; break;
                case '-': curr -= operand; break;
                case '*': curr *= operand; break;
                case '/': curr /= operand; break;
            }
            Console.WriteLine(
                "Current value = {0,3} (following {1} {2})",
                curr, @operator, operand);
        }
    }
    public class CalculatorSession
    {
        readonly Calculator calculator = new Calculator();
        readonly List<Command> commands = [];
        int current = 0;
        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels ", levels);
            for (int i = 0; i < levels; i++)
            {
                if (current < commands.Count - 1)
                {
                    Command command = commands[current++];
                    command.Do();
                }
            }
        }
        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels ", levels);

            for (int i = 0; i < levels; i++)
            {
                if (current > 0)
                {
                    Command command = commands[--current];
                    command.Undo();
                }
            }
        }
        public void Compute(char @operator, int operand)
        {
            Command command = new CalculatorCommand(calculator, @operator, operand);
            command.Do();

            if (commands.ElementAtOrDefault(current) == null)
            {
                commands.Add(command);

            }
            else
            {
                commands[current] = command;
                commands.RemoveRange(current + 1, commands.Count - current - 1);
            }
            current++;
        }
    }
}
