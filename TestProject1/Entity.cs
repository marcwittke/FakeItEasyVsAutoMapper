using System;

namespace TestProject1
{
    public class Entity
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime CreationDate { get; }


        public Entity()
        {
        }
        
        public Entity(int id, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
        }
    }
}