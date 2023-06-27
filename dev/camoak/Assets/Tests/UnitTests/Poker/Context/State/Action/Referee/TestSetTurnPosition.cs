using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestSetTurnPosition
    {
        private SetTurnPosition setTurnAction;
        private PokerGameState gameState, gameStateCopy;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 1, 2, 3, 4, 0 })
                .SetTurnPosition(0)
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            setTurnAction = new(new TestNextTurnPosition());
            setTurnAction.GameState = gameState;
            setTurnAction.Execute();
        }

        [Test]
        public void TestNewTurnPlayerIsPlayerAtIndexThree() =>
            Assert.AreEqual(3, gameState.TurnPosition);

        [Test]
        public void TestSetTurnPlayerActionOnlyAffectsTurnPlayer()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetTurnPosition(gameStateCopy.TurnPosition)
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }
    }

    internal class TestNextTurnPosition : ITargetPosition
    {
        public int GetPosition(PokerGameState gameState) => 3;
    }
}
