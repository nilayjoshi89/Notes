namespace Design_Patterns.Behavioral
{
    public class MediatorDemo : IDemonstratePattern
    {
        public string Name => "Behavioral.Mediator";

        public string ShortSummary => @"Mediator is a behavioral design pattern that reduces coupling between components of a program by making them communicate indirectly, through a special mediator object.";

        public void Run()
        {
            Chatroom chatroom = new Chatroom();
            Participant George = new Beatle("George");
            Participant Paul = new Beatle("Paul");
            Participant Ringo = new Beatle("Ringo");
            Participant John = new Beatle("John");
            Participant Yoko = new NonBeatle("Yoko");

            chatroom.Register(George);
            chatroom.Register(Paul);
            chatroom.Register(Ringo);
            chatroom.Register(John);
            chatroom.Register(Yoko);

            Yoko.Send("John", "Hi John!");
            Paul.Send("Ringo", "All you need is love");
            Ringo.Send("George", "My sweet Lord");
            Paul.Send("John", "Can't buy me love");
            John.Send("Yoko", "My sweet love");
        }

        public abstract class AbstractChatroom
        {
            public abstract void Register(Participant participant);
            public abstract void Send(
                string from, string to, string message);
        }
        public class Chatroom : AbstractChatroom
        {
            private readonly Dictionary<string, Participant> participants = [];
            public override void Register(Participant participant)
            {
                if (!participants.ContainsValue(participant))
                {
                    participants[participant.Name] = participant;
                }
                participant.Chatroom = this;
            }
            public override void Send(string from, string to, string message)
            {
                Participant participant = participants[to];
                participant?.Receive(from, message);
            }
        }
        public class Participant
        {
            public Participant(string name) => this.Name = name;
            public string Name { get; }
            public Chatroom Chatroom { set; get; }
            public void Send(string to, string message) => Chatroom.Send(Name, to, message);
            public virtual void Receive(
                string from, string message)
            {
                Console.WriteLine("{0} to {1}: '{2}'",
                    from, Name, message);
            }
        }
        public class Beatle : Participant
        {
            public Beatle(string name)
                : base(name)
            {
            }
            public override void Receive(string from, string message)
            {
                Console.Write("To a Beatle: ");
                base.Receive(from, message);
            }
        }
        /// <summary>
        /// A 'ConcreteColleague' class
        /// </summary>
        public class NonBeatle : Participant
        {
            public NonBeatle(string name)
                : base(name)
            {
            }
            public override void Receive(string from, string message)
            {
                Console.Write("To a non-Beatle: ");
                base.Receive(from, message);
            }
        }
    }
}
