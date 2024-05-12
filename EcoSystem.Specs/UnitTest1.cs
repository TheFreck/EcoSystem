using Machine.Specifications;

namespace EcoSystem.Specs
{
    public class When_Introducing_Two_Creatures
    {
        Establish context = () =>
        {
            trials = 100;
            inputHawkA = new Creature(Species.Hawk);
            inputHawkB = new Creature(Species.Hawk);
            inputDoveA = new Creature(Species.Dove);
            inputDoveB = new Creature(Species.Dove);
            expect_Hawk_Hawk = new Creature(Species.Hawk);
            expect_Hawk_Dove = new Creature(Species.Hawk);
            expect_Dove_Dove = new Creature(Species.Dove);
            answer_Hawk_Hawk = new (Creature, Creature)[trials];
            answer_Hawk_Dove = new (Creature, Creature)[trials];
            answer_Dove_Dove = new (Creature, Creature)[trials];
        };

        Because of = () =>
        {
            (initialWinner, initialLoser) = EcoSerivice.Trial(inputHawkA, inputHawkB);
            initialWinnerLostLast = initialWinner.LostLast;
            initialLoserLostLast = initialLoser.LostLast;
            for (var i = 0; i < trials; i++)
            {
                answer_Hawk_Hawk[i] = EcoSerivice.Trial(inputHawkA, inputHawkB);
                answer_Hawk_Dove[i] = EcoSerivice.Trial(inputHawkA, inputDoveA);
                answer_Dove_Dove[i] = EcoSerivice.Trial(inputDoveA, inputDoveB);
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

    public class When_Running_A_Round_Of_Trials_With_Randomized_Interactions
    {
        Establish context = () =>
        {
            trials = 100;
            inputHawks = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
            inputDoves = new int[] { 90, 80, 70, 60, 50, 40, 30, 20, 10 };
            creatures = new Creature[inputHawks.Length][];
            answers = new Creature[inputDoves.Length][];
        };

        Because of = () =>
        {
            for (var i = 0; i < inputHawks.Length; i++)
            {
                creatures[i] = new Creature[trials];
                var h = 0;
                for (; h < inputHawks[i]; h++)
                {
                    creatures[i][h] = new Creature(Species.Hawk);
                }
                for (var d = h; d < inputDoves[i] + h; d++)
                {
                    creatures[i][d] = new Creature(Species.Dove);
                }
                creatures[i].OrderBy(c => c.Id);
                answers[i] = EcoSerivice.Generation(creatures[i]);
            }
        };

        It Should_Return_Creatures_Array_With_Trial_Results = () =>
        {
            for (var i = 0; i < answers.Length; i++)
            {
                for (var j = 0; j < answers[i].Length; j++)
                {
                    (answers[i][j].Wins + answers[i][j].Losses + answers[i][j].Passes)
                    .ShouldEqual(1);
                }
            }
        };

        private static int trials;
        private static int[] inputHawks;
        private static int[] inputDoves;
        private static Creature[][] answers;
        private static Creature[][] creatures;
    }
}