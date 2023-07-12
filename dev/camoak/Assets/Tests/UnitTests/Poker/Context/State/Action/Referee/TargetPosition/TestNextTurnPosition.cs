using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy.TurnPlayerPosition;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.TurnPlayerStrategy.TurnPlayerPosition
{
    public class TestNextTurnPosition
    {
        private NextTurnPosition nextTurnPosition;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => nextTurnPosition = new();

        [Test]
        public void TestTurnPlayerIncrementsWhenPlayerIsNotAtEndAndInAction()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .SetPlayersInAction(new() { 3, 4, 0, 1, 2 })
                .SetTurnPosition(0)
                .Build();

            Assert.AreEqual(1, nextTurnPosition.GetPosition(gameState));
        }

        [Test]
        public void TestTurnPlayerStaysTheSameWhenPlayerIsRemovedFromAction()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .SetPlayersInAction(new() { 4, 0, 1, 2 })
                .SetTurnPosition(0)
                .Build();

            Assert.AreEqual(0, nextTurnPosition.GetPosition(gameState));
        }

        [Test]
        public void TestTurnPlayerRollsOverWhenPreviousPositionIsButton()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .SetPlayersInAction(new() { 3, 4, 0, 1, 2 })
                .SetTurnPosition(4)
                .Build();

            Assert.AreEqual(0, nextTurnPosition.GetPosition(gameState));
        }

        [Test]
        public void TestTurnPlayerStillRollsOverWhenButtonFolds()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .SetPlayersInAction(new() { 3, 4, 0, 1 })
                .SetTurnPosition(4)
                .Build();

            Assert.AreEqual(0, nextTurnPosition.GetPosition(gameState));
        }
    }
}
