namespace EcoSystem
{
    public class Generation
    {
        public List<Creature> Births;
        public List<Creature> Deaths;
        public List<Creature> Creatures;
        public int BirthCount { get { return Births.Count; } }
        public int DeathCount { get { return Deaths.Count; } }
        public int PopulationCount { get { return Creatures.Count; } }
    }
}