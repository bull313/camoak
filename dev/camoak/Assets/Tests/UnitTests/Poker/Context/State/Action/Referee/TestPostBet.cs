using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestPostBet
    {
        private PostBet postBetAction;
        private PokerGameState gameState, gameStateCopy;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetStack(100f)
                    .SetAction(0f)
                    .Build())
                .SetPlayerPositions(new() { 1, 0 })
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            postBetAction = new(new TestPosition(), 10f);
            postBetAction.GameState = gameState;
            postBetAction.Execute();
        }

        [Test]
        public void TestTargetPlayerStackSizeIsReducedByBetSize() =>
            Assert.AreEqual(90f, gameState.Players[0].Stack);

        [Test]
        public void TestTargetPlayerActionSizeIsIncreasedByBetSize() =>
            Assert.AreEqual(10f, gameState.Players[0].Action);

        [Test]
        public void TestPostBetActionOnlyAffectsTargetPlayerStackAndAction()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(gameState.Players[0])
                    .SetStack(gameStateCopy.Players[0].Stack)
                    .SetAction(gameStateCopy.Players[0].Action)
                    .Build())
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }

        private class TestPosition : ITargetPosition
        {
            public int GetPosition(PokerGameState gameState) => 1;
        }
    }
}
