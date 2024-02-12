using Demo.Common.Library;

namespace Design_Patterns.CreationalPatterns
{
    public class PrototypeDemo : DemoBase
    {
        public override string Name => "Cretional.Prototype";

        public override string ShortSummary => "Lets you copy existing objects without making your code dependent on their classes.";

        public override Task Run()
        {
            Person person1 = new Person() { FirstName = "F1", LastName = "L1" };
            Person person2 = new Person() { FirstName = "F2", LastName = "L2", Reference = person1 };
            Person person3 = (Person)person2.Clone();
            person1.FirstName = "F11";
            Person person4 = (Person)person2.Clone();
            Person[] persons = new Person[] { person1, person2, person3, person4 };

            foreach (Person person in persons)
            {
                Console.WriteLine("#");
                person.Print();
                Console.WriteLine("#");
            }

            return Task.CompletedTask;
        }
    }

    internal class Person : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Person Reference { get; set; }

        public object Clone()
        {
            Person? clonePerson = MemberwiseClone() as Person;
            if (Reference != null)
            {
                clonePerson!.Reference = (Person)Reference.Clone();
            }
            return clonePerson!;
        }

        public void Print()
        {
            Console.WriteLine($"FirstName: {FirstName}, LastName: {LastName}");
            if (Reference == null)
            {
                return;
            }

            Console.WriteLine($"Reference:");
            Reference.Print();
        }
    }
}
