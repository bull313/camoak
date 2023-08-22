using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class TestIsButtonPlayerTurnCheck
    {
        private IsButtonPlayerTurnCheck check;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => check = new();

        [Test]
        public void TestLastIndexTurnSatisfiesCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .SetPlayersInAction(new() { 3, 0, 1 })
                .SetTurnPosition(2)
                .Build();

            Assert.IsTrue(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestNotLastIndexTurnDoesNotSatisfyCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .SetPlayersInAction(new() { 3, 0, 1 })
                .SetTurnPosition(0)
                .Build();

            Assert.IsFalse(check.IsSatisfied(gameState));
        }
    }
}
