using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.TurnPlayerStrategy.TurnPlayerPosition
{
    public class TestBigBlindTurn
    {
        private BigBlindTurn turnStrategy;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => turnStrategy = new();

        [Test]
        public void TestBigBlindIsPlayerAtOneIndexInGameWithThreePlusPlayers()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 2, 3, 4, 0, 1 })
                .Build();

            Assert.AreEqual(1, turnStrategy.GetTurnPlayer(gameState));
        }

        [Test]
        public void TestBigBlindIsPlayerAtZeroIndexInHeadsUpGame()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 1, 0 })
                .Build();

            Assert.AreEqual(0, turnStrategy.GetTurnPlayer(gameState));
        }
    }
}
