using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestSetTurnPlayer
    {
        private SetTurnPlayer setTurnPlayerAction;
        private PokerGameState gameState, gameStateCopy;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 1, 2, 3, 4, 0 })
                .SetTurnPlayer(0)
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            setTurnPlayerAction = new(new TestNextTurnStrategy());
            setTurnPlayerAction.GameState = gameState;
            setTurnPlayerAction.Execute();
        }

        [Test]
        public void TestNewTurnPlayerIsPlayerAtIndexThree() =>
            Assert.AreEqual(3, gameState.TurnPlayer);

        [Test]
        public void TestSetTurnPlayerActionOnlyAffectsTurnPlayer()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetTurnPlayer(gameStateCopy.TurnPlayer)
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }
    }

    internal class TestNextTurnStrategy : ITurnPlayerStrategy
    {
        public int GetTurnPlayer(PokerGameState gameState) => 3;
    }
}
