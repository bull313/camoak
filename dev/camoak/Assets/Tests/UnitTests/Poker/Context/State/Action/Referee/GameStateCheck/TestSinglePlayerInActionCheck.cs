using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class TestSinglePlayerInActionCheck
    {
        private SinglePlayerInActionCheck check;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => check = new();

        [Test]
        public void TestSinlgePlayerInActionSatisfiesCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayersInAction(new() { 1 })
                .Build();

            Assert.IsTrue(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestMultiplePlayersInActionDoesNotSatsifyCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayersInAction(new() { 1, 0 })
                .Build();

            Assert.IsFalse(check.IsSatisfied(gameState));
        }
    }
}
