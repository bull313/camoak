using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.TurnPlayerStrategy.TurnPlayerPosition
{
    public class TestSmallBlindTurn
    {
        private SmallBlindTurn turnStrategy;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => turnStrategy = new();

        [Test]
        public void TestSmallBlindIsPlayerAtZeroIndexInGameOfThreePlusPlayers()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 2, 3, 4, 0, 1 })
                .Build();

            Assert.AreEqual(0, turnStrategy.GetTurnPlayer(gameState));
        }

        [Test]
        public void TestSmallBlindIsLastPlayerInHeadsUpGame()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 1, 0 })
                .Build();

            Assert.AreEqual(1, turnStrategy.GetTurnPlayer(gameState));
        }
    }
}
