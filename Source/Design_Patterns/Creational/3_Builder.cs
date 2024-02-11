namespace Design_Patterns.CreationalPatterns
{
    public class BuilderDemo : IDemonstratePattern
    {
        public string Name => "Cretional.Builder";

        public string ShortSummary => "Lets you construct complex objects step by step. The pattern allows you to produce different types and representations of an object using the same construction code.";

        public void Run()
        {
            Console.WriteLine("Resume Builder:");
            DocumentDirector director = new DocumentDirector(new ResumeBuilder());
            Console.WriteLine("Minumal:");
            Document doc = director.MinumalDoc();
            doc.Print();
            Console.WriteLine("Full:");
            doc = director.FullDoc();
            doc.Print();
            Console.WriteLine();

            Console.WriteLine("Report Builder:");
            director = new DocumentDirector(new ReportBuilder());
            Console.WriteLine("Minumal:");
            doc = director.MinumalDoc();
            doc.Print();
            Console.WriteLine("Full:");
            doc = director.FullDoc();
            doc.Print();
            Console.WriteLine();
        }
    }
    internal class DocumentDirector
    {
        private readonly DocumentBuilder builder;

        public DocumentDirector(DocumentBuilder builder) => this.builder = builder;

        public Document MinumalDoc()
        {
            Document doc = new Document(builder.DocType);
            builder.AddContentPages(doc);
            return doc;
        }
        public Document FullDoc()
        {
            Document doc = new Document(builder.DocType);
            builder.AddStartPages(doc);
            builder.AddContentPages(doc);
            builder.AddEndPages(doc);
            return doc;
        }
    }
    internal abstract class DocumentBuilder
    {
        public abstract string DocType { get; }

        public abstract void AddEndPages(Document doc);
        public abstract void AddContentPages(Document doc);
        public abstract void AddStartPages(Document doc);
    }
    internal class ResumeBuilder : DocumentBuilder
    {
        public override string DocType => "Resume";

        public override void AddStartPages(Document doc) => doc.Pages.Add(new SkillsPage());
        public override void AddContentPages(Document doc) => doc.Pages.Add(new EducationPage());
        public override void AddEndPages(Document doc) => doc.Pages.Add(new ExperiencePage());
    }
    internal class ReportBuilder : DocumentBuilder
    {
        public override string DocType => "Report";
        public override void AddStartPages(Document doc) => doc.Pages.Add(new IntroductionPage());
        public override void AddContentPages(Document doc)
        {
            doc.Pages.Add(new ResultsPage());
            doc.Pages.Add(new ConclusionPage());
        }
        public override void AddEndPages(Document doc)
        {
            doc.Pages.Add(new SummaryPage());
            doc.Pages.Add(new BibliographyPage());
        }
    }
    internal class Document
    {
        private readonly string type;

        public Document(string type) => this.type = type;
        public List<Page> Pages { get; } = [];

        public void Print()
        {
            Console.WriteLine($"Document ({type}):");
            foreach (Page p in Pages)
            {
                Console.WriteLine(p.Content);
            }
        }
    }
    internal abstract class Page
    {
        public abstract string Content { get; }
    }
    internal class SkillsPage : Page
    {
        public override string Content => "Skill Page";
    }
    internal class EducationPage : Page
    {
        public override string Content => "Education Page";
    }
    internal class ExperiencePage : Page
    {
        public override string Content => "Experience Page";
    }
    internal class IntroductionPage : Page
    {
        public override string Content => "Introduction Page";
    }
    internal class ResultsPage : Page
    {
        public override string Content => "Result Page";
    }
    internal class ConclusionPage : Page
    {
        public override string Content => "Conclusion Page";
    }
    internal class SummaryPage : Page
    {
        public override string Content => "Summary Page";
    }
    internal class BibliographyPage : Page
    {
        public override string Content => "Bibliography Page";
    }
}
