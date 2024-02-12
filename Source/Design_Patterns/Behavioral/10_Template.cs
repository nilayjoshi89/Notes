using Demo.Common.Library;

namespace Design_Patterns.Behavioral
{
    public class TemplateDemo : DemoBase
    {
        public override string Name => "Behavioral.Template";

        public override string ShortSummary => "defines the skeleton of an algorithm in the superclass but lets subclasses override specific steps of the algorithm without changing its structure.";

        public override Task Run()
        {
            Console.WriteLine("Processing PDF document:");
            DocumentProcessor pdfProcessor = new PdfDocumentProcessor();
            pdfProcessor.ProcessDocument();

            Console.WriteLine("\nProcessing Word document:");
            DocumentProcessor wordProcessor = new WordDocumentProcessor();
            wordProcessor.ProcessDocument();

            return Task.CompletedTask;
        }

        abstract class DocumentProcessor
        {
            public void ProcessDocument()
            {
                OpenDocument();
                ExtractText();
                AnalyzeText();
                CloseDocument();
            }

            protected abstract void OpenDocument();
            protected abstract void ExtractText();
            protected abstract void AnalyzeText();
            protected abstract void CloseDocument();
        }

        class PdfDocumentProcessor : DocumentProcessor
        {
            protected override void OpenDocument()
            {
                Console.WriteLine("Opening PDF document");
            }

            protected override void ExtractText()
            {
                Console.WriteLine("Extracting text from PDF document");
            }

            protected override void AnalyzeText()
            {
                Console.WriteLine("Analyzing text from PDF document");
            }

            protected override void CloseDocument()
            {
                Console.WriteLine("Closing PDF document");
            }
        }

        class WordDocumentProcessor : DocumentProcessor
        {
            protected override void OpenDocument()
            {
                Console.WriteLine("Opening Word document");
            }

            protected override void ExtractText()
            {
                Console.WriteLine("Extracting text from Word document");
            }

            protected override void AnalyzeText()
            {
                Console.WriteLine("Analyzing text from Word document");
            }

            protected override void CloseDocument()
            {
                Console.WriteLine("Closing Word document");
            }
        }
    }
}
