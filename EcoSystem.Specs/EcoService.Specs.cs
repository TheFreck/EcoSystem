using Machine.Specifications;

namespace EcoSystem.Specs
{
    public class When_Introducing_Two_Creatures
    {
        Establish context = () =>
        {
            trials = 100;
            inputHawkA = new Creature(Species.Hawk,"Name");
            inputHawkB = new Creature(Species.Hawk,"Name");
            inputDoveA = new Creature(Species.Dove,"Name");
            inputDoveB = new Creature(Species.Dove,"Name");
            expect_Hawk_Hawk = new Creature(Species.Hawk,"Name");
            expect_Hawk_Dove = new Creature(Species.Hawk,"Name");
            expect_Dove_Dove = new Creature(Species.Dove,"Name");
            answer_Hawk_Hawk = new (Creature, Creature)[trials];
            answer_Hawk_Dove = new (Creature, Creature)[trials];
            answer_Dove_Dove = new (Creature, Creature)[trials];
        };

        Because of = () =>
        {
            (initialWinner, initialLoser) = EcoService.Trial(inputHawkA, inputHawkB);
            initialWinnerLostLast = initialWinner.LostLast;
            initialLoserLostLast = initialLoser.LostLast;
            for (var i = 0; i < trials; i++)
            {
                answer_Hawk_Hawk[i] = EcoService.Trial(inputHawkA, inputHawkB);
                answer_Hawk_Dove[i] = EcoService.Trial(inputHawkA, inputDoveA);
                answer_Dove_Dove[i] = EcoService.Trial(inputDoveA, inputDoveB);
            }
            outputHawkACount = answer_Hawk_Hawk.Count(h => h.Item1.Id.Equals(inputHawkA.Id));
            outputHawkBCount = answer_Hawk_Hawk.Count(h => h.Item1.Id.Equals(inputHawkB.Id));
            outputDoveACount = answer_Dove_Dove.Count(h => h.Item1.Id.Equals(inputDoveA.Id));
            outputDoveBCount = answer_Dove_Dove.Count(h => h.Item1.Id.Equals(inputDoveB.Id));
        };

        It Should_Return_A_Hawk_When_A_Hawk_And_A_Hawk_Meet = () =>
        {
            for (var i = 0; i < trials; i++)
            {
                answer_Hawk_Hawk[i].Item1.Species.ShouldEqual(Species.Hawk);
                answer_Hawk_Hawk[i].Item2.Species.ShouldEqual(Species.Hawk);
            }
        };
        It Should_Return_A_Hawk_When_A_Hawk_And_A_Dove_Meet = () =>
        {
            for (var i = 0; i < trials; i++)
            {
                answer_Hawk_Dove[i].Item1.Species.ShouldEqual(Species.Hawk);
                answer_Hawk_Dove[i].Item2.Species.ShouldEqual(Species.Dove);
            }
        };
        It Should_Return_A_Dove_When_A_Dove_And_A_Dove_Meet = () =>
        {
            for (var i = 0; i < trials; i++)
            {
                answer_Dove_Dove[i].Item1.Species.ShouldEqual(Species.Dove);
                answer_Dove_Dove[i].Item2.Species.ShouldEqual(Species.Dove);
            }
        };

        It Should_Return_HawkA_Between_30_And_70_Percent_Of_The_Time = () =>
        {
            outputHawkACount.ShouldBeGreaterThan(trials * .3);
            outputHawkACount.ShouldBeLessThan(trials * .7);
        };
        It Should_Return_HawkB_Between_30_And_70_Percent_Of_The_Time = () =>
        {
            outputHawkBCount.ShouldBeGreaterThan(trials * .3);
            outputHawkBCount.ShouldBeLessThan(trials * .7);
        };

        It Should_Update_HawkA_Wins_After_Trials = () => inputHawkA.Wins.ShouldBeGreaterThan(0);
        It Should_Update_HawkB_Wins_After_Trials = () => inputHawkB.Wins.ShouldBeGreaterThan(0);

        It Should_Update_HawkA_Losses_After_Trials = () => inputHawkA.Losses.ShouldBeGreaterThan(0);
        It Should_Update_HawkB_Losses_After_Trials = () => inputHawkB.Losses.ShouldBeGreaterThan(0);

        It Should_Calculate_Win_Loss_Ratio_For_HawkA = () => inputHawkA.WLRatio.ShouldBeGreaterThan(0);
        It Should_Calculate_Win_Loss_Ratio_For_HawkB = () => inputHawkB.WLRatio.ShouldBeGreaterThan(0);

        It Should_Update_LostLast_Status_Of_Losing_Hawk_To_True = () => initialWinnerLostLast.ShouldEqual(false);
        It Should_Update_LostLast_Status_Of_Winning_Hawk_To_False = () => initialLoserLostLast.ShouldEqual(true);

        It Should_Return_DoveA_Between_30_And_70_Percent_Of_The_Time = () =>
        {
            outputDoveACount.ShouldBeGreaterThan(trials * .3);
            outputDoveACount.ShouldBeLessThan(trials * .7);
        };
        It Should_Return_DoveB_Between_30_And_70_Percent_Of_The_Time = () =>
        {
            outputDoveBCount.ShouldBeGreaterThan(trials * .3);
            outputDoveBCount.ShouldBeLessThan(trials * .7);
        };

        It Should_Update_DoveA_Wins_After_Trials = () => inputDoveA.Wins.ShouldBeGreaterThan(0);
        It Should_Update_DoveB_Wins_After_Trials = () => inputDoveB.Wins.ShouldBeGreaterThan(0);

        It Should_Update_DoveA_Losses_After_Trials = () => inputDoveA.Losses.ShouldBeGreaterThan(0);
        It Should_Update_DoveB_Losses_After_Trials = () => inputDoveB.Losses.ShouldBeGreaterThan(0);

        It Should_Calculate_Win_Loss_Ratio_For_DoveA = () => inputDoveA.WLRatio.ShouldBeGreaterThan(0);
        It Should_Calculate_Win_Loss_Ratio_For_DoveB = () => inputDoveB.WLRatio.ShouldBeGreaterThan(0);

        private static int trials;
        private static Creature inputHawkA;
        private static Creature inputHawkB;
        private static Creature inputDoveA;
        private static Creature inputDoveB;
        private static Creature expect_Hawk_Hawk;
        private static Creature expect_Hawk_Dove;
        private static Creature expect_Dove_Dove;
        private static (Creature, Creature)[] answer_Hawk_Hawk;
        private static (Creature, Creature)[] answer_Hawk_Dove;
        private static (Creature, Creature)[] answer_Dove_Dove;
        private static int outputHawkACount;
        private static int outputHawkBCount;
        private static int outputDoveACount;
        private static int outputDoveBCount;
        private static Creature initialWinner;
        private static Creature initialLoser;
        private static bool initialWinnerLostLast;
        private static bool initialLoserLostLast;
    }

    public class When_Deciding_Interactions_Of_New_Creatures_Based_On_Randomness
    {
        Establish context = () =>
        {
            creatureCount = 10;
            creatures = new List<Creature>();
            answers = new List<(Guid, Guid)>();
            rando = new Random();

            for(var i=0; i<creatureCount; i++)
            {
                creatures.Add(new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, rando.NextDouble().ToString()));
            }
            ecoService = new EcoService(creatures);
        };

        Because of = () => answers = ecoService.PairCreatures(creatures,rando);

        It Should_Return_A_List_Half_As_Long_As_The_Creature_Count = () => answers.Count.ShouldEqual(creatureCount/2);
        It Should_Pair_Up_Creatures_Into_A_List_Of_Tuples = () =>
        {
            expectedHash = new HashSet<Guid>();
            foreach(var answer in answers)
            {
                expectedHash.Add(answer.Item1);
                expectedHash.Add(answer.Item2);
            }
            expectedHash.Count.ShouldEqual(creatureCount);
        };

        private static int creatureCount;
        private static List<Creature> creatures;
        private static List<(Guid, Guid)> answers;
        private static Random rando;
        private static HashSet<Guid> expectedHash;
        private static EcoService ecoService;
    }

    public class When_Deciding_Interactions_Of_Creatures_By_Proximity
    {
        Establish context = () =>
        {
            rando = new Random();
            creatureCount = 10;
            A = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "A")
            {
                Location = (1, 5)
            };
            B = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "B")
            {
                Location = (10, 1)
            };
            C = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "C")
            {
                Location = (4, 5)
            };
            D = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "D")
            {
                Location = (2, 1)
            };
            E = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "E")
            {
                Location = (4, 2)
            };
            F = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "F")
            {
                Location = (3, 5)
            };
            G = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "G")
            {
                Location = (1, 4)
            };
            H = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "H")
            {
                Location = (1, 2)
            };
            I = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "I")
            {
                Location = (6, 2)
            };
            J = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, "J")
            {
                Location = (9, 3)
            };
            creatures = new List<Creature>
            {
                A,
                B,
                C,
                D,
                E,
                F,
                G,
                H,
                I,
                J
            };
            expectedPairs = new List<(Guid, Guid)>
            {
                (A.Id,G.Id),
                (H.Id,D.Id),
                (F.Id,C.Id),
                (E.Id,I.Id),
                (J.Id,B.Id)
            };
            answers = new List<(Guid, Guid)>();

            ecoService = new EcoService(creatures);
        };

        Because of = () => answers = ecoService.PairCreatures(creatures);

        It Should_Return_Pairs_Based_On_Proximity_Of_Creatures_To_Each_Other = () =>
        {
            foreach(var pair in answers)
            {
                if(expectedPairs.Where(p => p.Item1 == pair.Item1).Any())
                {
                    expectedPairs.Where(p => p.Item1 == pair.Item1).FirstOrDefault().Item2.ShouldEqual(pair.Item2);
                }
                else if(expectedPairs.Where(p => p.Item1 == pair.Item2).Any())
                {
                    expectedPairs.Where(p => p.Item1 == pair.Item2).FirstOrDefault().Item2.ShouldEqual(pair.Item1);
                }
                else
                {
                    "This answer pair is strange".ShouldEqual("since it should only be case A or case B");
                }
            }
        };

        private static int creatureCount;

        public static Creature A;
        public static Creature B;
        public static Creature C;
        public static Creature D;
        public static Creature E;
        public static Creature F;
        public static Creature G;
        public static Creature H;
        public static Creature I;
        public static Creature J;
        public static List<Creature> creatures;
        private static List<(Guid, Guid)> expectedPairs;
        public static List<(Guid, Guid)> answers;
        public static Random rando;
        public static EcoService ecoService;
    }

    public class When_Deciding_Interactions_Of_Creatures_Based_On_Randomness
    {
        Establish context = () =>
        {
            creatureCount = 12;
            creatures = new List<Creature>();
            losers = new List<Creature>();
            winners = new List<Creature>();
            answers = new List<(Guid, Guid)>();
            rando = new Random();

            for (var i = 0; i < creatureCount; i++)
            {
                var creature = new Creature(rando.Next(5) == 1 ? Species.Hawk : Species.Dove, rando.NextDouble().ToString())
                {
                    LostLast = i % 2 == 0 ? true : false
                };
                if(i % 2 == 0) losers.Add(creature);
                else winners.Add(creature);
                creatures.Add(creature);
            }
            ecoService = new EcoService(creatures);
        };

        Because of = () => answers = ecoService.PairCreatures(creatures, rando);

        It Should_Not_Pair_Those_Creatures_Who_Lost_Last = () => answers.Count.ShouldEqual(creatureCount/4);
        It Should_Pair_Up_Creatures_Into_A_List_Of_Tuples = () =>
        {
            expectedHash = new HashSet<Guid>();
            foreach (var answer in answers)
            {
                expectedHash.Add(answer.Item1);
                expectedHash.Add(answer.Item2);
            }
            expectedHash.Count.ShouldEqual(winners.Count);
        };

        private static int creatureCount;
        private static List<Creature> creatures;
        private static List<Creature> losers;
        private static List<Creature> winners;
        private static List<(Guid, Guid)> answers;
        private static Random rando;
        private static HashSet<Guid> expectedHash;
        private static EcoService ecoService;
    }

    public class When_Running_An_Initial_Round_Of_Trials_With_Randomized_Interactions
    {
        Establish context = () =>
        {
            randy = new Random();
            creatureCount = 10;
            creatures = new List<Creature>();
            interactions = new List<(Guid, Guid)>();
            for(var i=0; i<creatureCount/2; i++)
            {
                var creatureA = new Creature(randy.Next(4) % 4 == 0 ? Species.Hawk : Species.Dove, "Name");
                var creatureB = new Creature(randy.Next(4) % 4 == 0 ? Species.Hawk : Species.Dove, "Name");
                creatures.Add(creatureA);
                creatures.Add(creatureB);
                interactions.Add((creatureA.Id, creatureB.Id));

            }
            answers = new Generation();
            answers.Creatures = new List<Creature>();
            ecoService = new EcoService(creatures);
        };

        Because of = () => answers = ecoService.NextGeneration(ecoService.PairCreatures(creatures,randy));

        It Should_Return_Creatures_Array_With_Trial_Results = () =>
        {
            for (var i = 0; i < answers.Creatures.Count; i++)
            {
                if (answers.Creatures[i].LostLast) answers.Creatures[i].Losses.ShouldEqual(1);
                else answers.Creatures[i].Losses.ShouldEqual(0);
            }
        };

        It Should_Return_The_Same_Creatures_But_Modified = () =>
        {
            for (var i = 0; i < creatureCount; i++)
            {
                answers.Creatures[i].Id.ShouldEqual(creatures[i].Id);
            }
        };
        private static Random randy;
        public static int creatureCount;
        public static int[] inputHawks;
        public static int[] inputDoves;
        public static Generation answers;
        public static EcoService ecoService;
        public static List<Creature> creatures;
        public static List<(Guid, Guid)> interactions;
    }

    public class When_Running_A_Second_Round_Of_Trials_With_Randomized_Interactions
    {
        Establish context = () =>
        {
            randy = new Random();
            doveA = new Creature(Species.Dove, "LostLast")
            {
                Wins = 0,
                Losses = 0,
                Passes = 0,
                LostLast = true
            };
            expectDoveA = new Creature(Species.Dove, "expectDoveA")
            {
                Wins = 0,
                Losses = 0,
                Passes = 0,
                LostLast = false
            };
            doveB = new Creature(Species.Dove,"DoveB")
            {
                Wins = 0,
                Losses = 0,
                Passes = 0,
                LostLast = false
            };
            expectDoveB = new Creature(Species.Dove, "LostLast")
            {
                Wins = 0,
                Losses = 0,
                Passes = 0,
                LostLast = true
            };
            hawk = new Creature(Species.Hawk,"Hawk")
            {
                Wins = 0,
                Losses = 0,
                Passes = 0,
                LostLast = false
            };
            expectHawk = new Creature(Species.Hawk,"expectHawk")
            {
                Wins = 0,
                Losses = 0,
                Passes = 0,
                LostLast = false
            };
            input = new List<Creature>
            {
                doveA,
                doveB,
                hawk
            };
            ecoService = new EcoService(input);
            answer = new Generation
            {
                Creatures = input
            };
        };

        Because of = () => answer = ecoService.NextGeneration(ecoService.PairCreatures(input,randy));

        It Should_Return_The_Same_Creatures_But_Modified = () =>
        {
            for (var i = 0; i < input.Count; i++)
            {
                answer.Creatures[i].Id.ShouldEqual(input[i].Id);
            }
        };

        It Should_Skip_Creature_That_Lost_Last_Round = () =>
        {
            for(var i=0; i<answer.Creatures.Count; i++)
            {
                if (answer.Creatures[i].Name.Equals("LostLast"))
                {
                    (answer.Creatures[i].Wins + answer.Creatures[i].Losses).ShouldEqual(0);
                }
                else
                {
                    (answer.Creatures[i].Wins + answer.Creatures[i].Losses).ShouldEqual(1);
                }
            }
        };
        private static Random randy;
        private static Creature doveA;
        private static Creature hawk;
        private static Creature doveB;
        private static List<Creature> input;
        private static Creature expectDoveA;
        private static Creature expectDoveB;
        private static Creature expectHawk;
        private static EcoService ecoService;
        private static Generation answer;
    }

    public class When_Reproducing_Creatures
    {
        Establish context = () =>
        {
            input = new List<Creature>
            {
                new Creature(Species.Hawk, "HawkA")
                {
                    Wins = 75,
                    Losses = 25,
                    Passes = 1
                },
                new Creature(Species.Hawk, "HawkB")
                {
                    Wins = 50,
                    Losses = 50,
                    Passes = 50
                },
                new Creature(Species.Dove,"DoveA")
                {
                    Wins = 25,
                    Losses = 75,
                    Passes = 3
                },
                new Creature(Species.Dove, "DoveB")
                {
                    Wins = 15,
                    Losses = 85,
                    Passes = 2
                },
            };
            expect = new Generation
            {
                Creatures = new List<Creature>
                {
                    new Creature(Species.Hawk, "HawkA")
                    {
                        Wins = 75,
                        Losses = 25,
                        Passes = 1
                    },
                    new Creature(Species.Hawk,"HawkA.1"),
                    new Creature(Species.Hawk, "HawkB")
                    {
                        Wins = 50,
                        Losses = 50,
                        Passes = 50
                    },
                    new Creature(Species.Hawk,"HawkB.1"),
                    new Creature(Species.Dove,"DoveA")
                    {
                        Wins = 25,
                        Losses = 75,
                        Passes = 3
                    },
                },
                Births = new List<Creature>
                {
                    new Creature(Species.Hawk,"HawkA.1"),
                    new Creature(Species.Hawk,"HawkB.1"),
                },
                Deaths = new List<Creature>
                {
                    new Creature(Species.Dove, "DoveB")
                    {
                        Wins = 15,
                        Losses = 85,
                        Passes = 2
                    },
                }
            };
            ecoService = new EcoService(input);
        };

        Because of = () => answer = ecoService.Reproduce();

        It Should_Reproduce_The_Fit = () => answer.Births.ForEach(b => answer.Creatures.Select(e => e.Id).ShouldContain(b.Id));
        It Should_Remove_The_Unfit = () => answer.Deaths.ForEach(d => answer.Creatures.Select(e => e.Id).ShouldNotContain(d.Id));
        It Should_Only_Remove_The_Unfit = () => answer.Creatures.ForEach(d => answer.Deaths.Select(a => a.Id).ShouldNotContain(d.Id));

        private static List<Creature> input;
        private static Generation expect;
        private static EcoService ecoService;
        private static Generation answer;
    }
}