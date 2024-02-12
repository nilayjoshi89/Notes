using Spectre.Console;

namespace Demo.Common.Library
{
    public interface IConsoleWriter
    {
        ConsoleKeyInfo ReadKey();
        void WriteLine(string text);
        void WriteHeaderLine(string text);
        void WriteHeader2Line(string text);
        void Write(string text);
    }

    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string text) => AnsiConsole.MarkupLine(text);
        public ConsoleKeyInfo ReadKey() => Console.ReadKey();
        public void Write(string text) => AnsiConsole.Markup(text);
        public void WriteHeaderLine(string text) => AnsiConsole.MarkupLine($"[bold underline green]{text}[/]");
        public void WriteHeader2Line(string text) => AnsiConsole.MarkupLine($"[bold teal]{text}[/]");
    }
}
