
using Demo.Common.Library;

namespace Design_Patterns.StructuralPatterns
{
    public class FlyweightDemo : DemoBase
    {
        public override string Name => "Structural.Flyweight";

        public override string ShortSummary => @"allows programs to support vast quantities of objects by keeping their memory consumption low.
    - reduce memory usage by sharing data across similar objects.
	- Factory creating objects using same supporting class references rather than creating new earch and every time (like logger, printer etc.)";

        public override Task Run()
        {
            TextEditor textEditor = new TextEditor();

            // Adding characters with formatting
            textEditor.AddCharacter('H', ConsoleColor.Red);
            textEditor.AddCharacter('e', ConsoleColor.Green);
            textEditor.AddCharacter('l', ConsoleColor.Blue);
            textEditor.AddCharacter('l', ConsoleColor.Cyan);
            textEditor.AddCharacter('o', ConsoleColor.Magenta);

            // Displaying the formatted text
            textEditor.DisplayText();

            return Task.CompletedTask;
        }

        interface ICharacterFormat
        {
            void ApplyFormat();
        }

        class CharacterFormat : ICharacterFormat
        {
            private readonly char symbol;
            private readonly ConsoleColor color;

            public CharacterFormat(char symbol, ConsoleColor color)
            {
                this.symbol = symbol;
                this.color = color;
            }

            public void ApplyFormat()
            {
                Console.ForegroundColor = color;
                Console.Write(symbol);
                Console.ResetColor();
            }
        }

        class CharacterFormatFactory
        {
            private readonly Dictionary<char, ICharacterFormat> characterFormats = [];

            public ICharacterFormat GetCharacterFormat(char symbol, ConsoleColor color)
            {
                if (!characterFormats.ContainsKey(symbol))
                {
                    characterFormats[symbol] = new CharacterFormat(symbol, color);
                }
                return characterFormats[symbol];
            }
        }

        class TextEditor
        {
            private readonly List<Tuple<char, ConsoleColor>> characters = [];
            private readonly CharacterFormatFactory formatFactory = new CharacterFormatFactory();

            public void AddCharacter(char symbol, ConsoleColor color)
            {
                _ = formatFactory.GetCharacterFormat(symbol, color);
                characters.Add(new Tuple<char, ConsoleColor>(symbol, color));
            }

            public void DisplayText()
            {
                foreach (Tuple<char, ConsoleColor> tuple in characters)
                {
                    formatFactory.GetCharacterFormat(tuple.Item1, tuple.Item2).ApplyFormat();
                }
            }
        }

    }
}
