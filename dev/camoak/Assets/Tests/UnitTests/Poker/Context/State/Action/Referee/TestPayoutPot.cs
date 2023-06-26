using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestPayoutPot
    {
        private PayoutPot payoutPot;
        private PokerGameState gameState, gameStateCopy;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetCenterPot(10f)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetStack(95f)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[1])
                    .SetStack(95f)
                    .Build())
                .SetPlayersInAction(new() { 1 })
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            payoutPot = new();
            payoutPot.GameState = gameState;
            payoutPot.Execute();
        }

        [Test]
        public void TestRemainingPlayerInActionIncreasesStackByPot() =>
            Assert.AreEqual(105f, gameState.Players[1].Stack);

        [Test]
        public void TestCenterPotIsEmptied() =>
            Assert.AreEqual(0f, gameState.CenterPot);

        [Test]
        public void TestPayoutPotActionOnlyAffectsStackSizeAndCenterPot()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(gameState.Players[1])
                    .SetStack(gameStateCopy.Players[1].Stack)
                    .Build())
                .SetCenterPot(gameStateCopy.CenterPot)
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }

        [Test]
        public void TestPlayersSplitCenterPotIfBothInAction()
        {
            PokerGameState splitState = PokerGameStateBuilder.Create()
                .Copy(gameStateCopy)
                .SetPlayersInAction(new() { 1, 0 })
                .Build();

            payoutPot.GameState = splitState;
            payoutPot.Execute();

            Assert.AreEqual(100f, splitState.Players[0].Stack);
            Assert.AreEqual(100f, splitState.Players[1].Stack);
        }
    }
}
