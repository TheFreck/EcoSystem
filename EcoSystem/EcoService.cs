using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcoSystem
{
    public class EcoService
    {
        public List<Creature> creatures;
        public EcoService(List<Creature> creatures)
        {
            this.creatures = creatures;
        }
        public Generation NextGeneration(List<(Guid,Guid)> pairs)
        {
            foreach(var pair in pairs)
            {
                Trial(creatures.Where(c => c.Id == pair.Item1).FirstOrDefault(), creatures.Where(c => c.Id == pair.Item2).FirstOrDefault());
            }

            return new Generation
            {
                Creatures = creatures,
            };
        }

        public static (Creature,Creature) Trial(Creature creatureA, Creature creatureB)
        {
            var randy = new Random();

            switch ((creatureA.Species, creatureB.Species))
            {
                case (Species.Hawk, Species.Hawk):
                    if (randy.Next(0, 2) == 1)
                    {
                        creatureA.Wins++;
                        creatureB.Losses++;
                        creatureA.LostLast = false;
                        creatureB.LostLast = true;
                        return (creatureA, creatureB);
                    }
                    else
                    {
                        creatureB.Wins++;
                        creatureA.Losses++;
                        creatureB.LostLast = false;
                        creatureA.LostLast = true;
                        return (creatureB, creatureA);
                    }
                case (Species.Hawk, Species.Dove):
                    if (randy.Next(10) <= 8)
                    {
                        creatureA.Wins++;
                        creatureB.Losses++;
                        creatureA.LostLast = false;
                        creatureB.LostLast = true;
                        return (creatureA, creatureB);
                    }
                    break;
                case (Species.Dove, Species.Dove):
                    if (randy.Next(0, 2) == 1)
                    {
                        creatureA.Wins++;
                        creatureB.Losses++;
                        creatureA.LostLast = false;
                        creatureB.LostLast = true;
                        return (creatureA, creatureB);
                    }
                    else
                    {
                        creatureB.Wins++;
                        creatureA.Losses++;
                        creatureB.LostLast = false;
                        creatureA.LostLast = true;
                        return (creatureB, creatureA);
                    }
                case (Species.Dove, Species.Hawk):
                    if (randy.Next(10) <= 8)
                    {
                        creatureA.Wins++;
                        creatureB.Losses++;
                        creatureA.LostLast = false;
                        creatureB.LostLast = true;
                        return (creatureA, creatureB);
                    }
                    break;
            }
            return (new Creature(Species.Hawk, "Missed"), new Creature(Species.Dove, "Got Away"));
        }

        public Generation Reproduce()
        {
            var randy = new Random();
            var newGeneration = NextGeneration(PairCreatures(creatures,randy));

            var newCreatures = new List<Creature>();
            var births = new List<Creature>();
            var deaths = new List<Creature>();
            Console.WriteLine("=====================");
            for (var i=0; i< newGeneration.Creatures.Count; i++)
            {
                if (newGeneration.Creatures[i].WLRatio >= .5)
                {
                    var birth = new Creature(newGeneration.Creatures[i].Species, newGeneration.Creatures[i].Name + "." + randy.Next(100));
                    newCreatures.Add(birth);
                    births.Add(birth);
                }
                if (newGeneration.Creatures[i].WLRatio >= .2)
                {
                    newCreatures.Add(newGeneration.Creatures[i]);
                }
                else
                {
                    deaths.Add(creatures[i]);
                }
            }
            creatures = newCreatures;
            return new Generation
            {
                Creatures = newCreatures,
                Births = births,
                Deaths = deaths,
            };
        }

        public List<(Guid, Guid)> PairCreatures(List<Creature> creatures, Random randy)
        {
            var pairs = new List<(Guid,Guid)>();
            var yetToBeTried = creatures.Where(c => !c.LostLast).ToList();
            creatures.Where(c => c.LostLast).ToList().ForEach(c => {
                c.LostLast = false;
                c.Passes++;
            });
            for (var i = 0; i < creatures.Count / 2; i++)
            {
                if (yetToBeTried.Count == 0) break;
                if (yetToBeTried.Count == 1)
                {
                    yetToBeTried[0].Passes++;
                    yetToBeTried[0].LostLast = false;
                    break;
                }
                var creatureIndex = randy.Next(yetToBeTried.Count);
                var creatureA = yetToBeTried[creatureIndex];
                yetToBeTried.RemoveAt(creatureIndex);
                creatureIndex = randy.Next(yetToBeTried.Count);
                var creatureB = yetToBeTried[creatureIndex];
                yetToBeTried.RemoveAt(creatureIndex);
                pairs.Add((creatureA.Id, creatureB.Id));
            }

            return pairs;
        }

        public List<(Guid,Guid)> PairCreatures(List<Creature> creatures)
        {
            var proximities = new Dictionary<Creature, Creature[]>();
            var selectedArrays = new Dictionary<string, string[]>();
            var differences = new Dictionary<string, (string, string)[]>();
            var pairs = new List<(Guid,Guid)>();
            foreach(var creature in creatures)
            {
                var creatureArray = creatures.OrderBy(c => ((creature.Location.Item2 - c.Location.Item2) * (creature.Location.Item2 - c.Location.Item2)) + ((creature.Location.Item1 - c.Location.Item1)* (creature.Location.Item1 - c.Location.Item1))).ToArray();
                var selected = creatureArray.Select(c => ((creature.Location.Item2 - c.Location.Item2) * (creature.Location.Item2 - c.Location.Item2)) + ((creature.Location.Item1 - c.Location.Item1) * (creature.Location.Item1 - c.Location.Item1))).ToArray();
                
                proximities.Add(creature, creatures.OrderBy(c => ((creature.Location.Item2 - c.Location.Item2) * (creature.Location.Item2 - c.Location.Item2)) + ((creature.Location.Item1 - c.Location.Item1) * (creature.Location.Item1 - c.Location.Item1))).ToArray());
                selectedArrays.Add(creature.Name, creatureArray.Select(s => s.Name).ToArray());
            }
            // proximities include a creature and the remaining creatures in order of proximity
            // still need to assign pairs based on this
            return new List<(Guid, Guid)>();
        }
    }
}
