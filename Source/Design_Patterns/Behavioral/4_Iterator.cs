using System.Collections;

namespace Design_Patterns.Behavioral
{
    public class IteratorDemo : IDemonstratePattern
    {
        public string Name => "Behavioral.Iterator";

        public string ShortSummary => @"Sequentially access the elements of a collection";

        public void Run()
        {
            MyCollection collection = new MyCollection();
            foreach (string item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class MyCollection : IEnumerable<string>
    {
        private readonly string[] data = ["a", "abc", "d", "e", "f"];
        public IEnumerator<string> GetEnumerator() => new MyCollectionEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => new MyCollectionEnumerator(this);

        public class MyCollectionEnumerator : IEnumerator<string>
        {
            private readonly MyCollection collection;
            private int index = -1;

            public MyCollectionEnumerator(MyCollection collection) => this.collection = collection;

            public string Current => index == -1 ? null : collection.data[index];

            object IEnumerator.Current => Current;

            public void Dispose() { }
            public bool MoveNext()
            {
                if (index + 1 < collection.data.Count())
                {
                    index++;
                    return true;
                }

                return false;
            }
            public void Reset() => index = -1;
        }
    }
}
