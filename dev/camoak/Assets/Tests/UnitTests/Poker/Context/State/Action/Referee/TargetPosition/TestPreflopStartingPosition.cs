using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.TurnPlayerStrategy
{
    public class TestPreflopStartingPosition
    {
        private PreflopStartingPosition turnStrategy;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => turnStrategy = new();

        [Test]
        public void TestSelectPlayerLeftOfBigBlindInThreePlusPlayerGame()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 2, 3, 4, 0, 1 })
                .SetTurnPosition(0)
                .Build();

            Assert.AreEqual(2, turnStrategy.GetPosition(gameState));
        }

        [Test]
        public void TestSelectButtonPlayerInHeadsUpGame()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 0, 1 })
                .SetTurnPosition(0)
                .Build();

            Assert.AreEqual(1, turnStrategy.GetPosition(gameState));
        }
    }
}
