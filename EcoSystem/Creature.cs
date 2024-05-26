using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoSystem
{
    public class Creature
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly Species Species;
        public int Wins;
        public int Losses;
        public int Passes;
        public bool LostLast;
        public (double, double) Location;
        public double WLRatio { get { return (double)Wins / (double)(Wins + Losses); } }

        public Creature(Species species, string name)
        {
            Id = Guid.NewGuid();
            Species = species;
            Name = name;
        }
    }
}
