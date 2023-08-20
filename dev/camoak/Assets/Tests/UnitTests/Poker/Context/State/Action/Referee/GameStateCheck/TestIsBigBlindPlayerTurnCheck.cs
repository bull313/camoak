using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class TestIsBigBlindPlayerTurnCheck
    {
        private IsBigBlindPlayerTurnCheck check;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => check = new();

        [Test]
        public void TestBigBlindPlayerTurnHeadsUpSatisfiesCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 1, 0 })
                .SetPlayersInAction(new() { 1, 0 })
                .SetTurnPosition(0)
                .Build();

            Assert.IsTrue(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestNotBigBlindPlayerTurnHeadsUpDoesNotSatisfyCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 1, 0 })
                .SetPlayersInAction(new() { 1, 0 })
                .SetTurnPosition(1)
                .Build();

            Assert.IsFalse(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestBigBlindPlayerTurnThreePlusPlayersSatisfiesCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .SetPlayersInAction(new() { 3, 4, 0, 1, 2 })
                .SetTurnPosition(1)
                .Build();

            Assert.IsTrue(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestNotBigBlindPlayerTurnThreePlusPlayersFailsCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .SetPlayersInAction(new() { 3, 4, 0, 1, 2 })
                .SetTurnPosition(0)
                .Build();

            Assert.IsFalse(check.IsSatisfied(gameState));
        }
    }
}
