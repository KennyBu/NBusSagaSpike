using System;

namespace ServerSaga.TestEntity
{
    public class Ninja : IWarrior
    {
        public Ninja()
        {
            Name = "I am a Ninja Warrior!";
        }

        public string Name { get; set; }
    }
}