using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoSystem
{
    public class EcoSerivice
    {
        public static Creature[] Generation(Creature[] creatures)
        {
            var yetToBeTried = creatures.ToList();
            var randy = new Random();
            for (var i = 0; i < creatures.Length / 2; i++)
            {
                var creatureIndex = randy.Next(yetToBeTried.Count);
                var creatureA = yetToBeTried[creatureIndex];
                yetToBeTried.RemoveAt(creatureIndex);
                creatureIndex = randy.Next(yetToBeTried.Count);
                var creatureB = yetToBeTried[creatureIndex];
                yetToBeTried.RemoveAt(creatureIndex);
                if (randy.Next(4) == 3)
                {
                    creatureA.Passes++;
                    creatureB.Passes++;
                }
                else
                {
                    Trial(creatureA, creatureB);
                }
            }

            return creatures;
        }

        public static (Creature, Creature) Trial(Creature creatureA, Creature creatureB)
        {
            var randy = new Random();

            switch ((creatureA.Species, creatureB.Species))
            {
                case (Species.Hawk, Species.Hawk):
                    return randy.Next(0, 2) == 1 ? UpdateCreatures(creatureA, creatureB) : UpdateCreatures(creatureB, creatureA);
                case (Species.Hawk, Species.Dove):
                case (Species.Dove, Species.Hawk):
                    return UpdateCreatures(creatureA, creatureB);
                case (Species.Dove, Species.Dove):
                    return randy.Next(0, 2) == 1 ? UpdateCreatures(creatureA, creatureB) : UpdateCreatures(creatureB, creatureA);
            }
            throw new Exception("This trial is for Hawks and Doves");
        }

        static (Creature, Creature) UpdateCreatures(Creature winner, Creature loser)
        {
            winner.Wins++;
            loser.Losses++;
            winner.LostLast = false;
            loser.LostLast = true;
            return (winner, loser);
        }
    }
}
