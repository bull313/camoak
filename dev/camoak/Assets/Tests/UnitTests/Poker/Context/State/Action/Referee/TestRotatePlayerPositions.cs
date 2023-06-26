using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestRotatePlayerPositions
    {
        private RotatePlayerPositions rotatePlayers;
        private PokerGameState gameState, gameStateCopy;
        private List<int> expectedPositions;

        [SetUp]
        public void SetUp()
        {
            expectedPositions = new() { 4, 0, 1, 2, 3 };

            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 3, 4, 0, 1, 2 })
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            rotatePlayers = new();
            rotatePlayers.GameState = gameState;
            rotatePlayers.Execute();
        }

        [Test]
        public void TestPlayerPositionsHaveBeenRotated() =>
            Assert.IsTrue(
                expectedPositions.SequenceEqual(gameState.PlayerPositions)
            );

        [Test]
        public void TestRotatePlayersOnlyAffectsPlayerPositions()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayerPositions(gameStateCopy.PlayerPositions)
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }
    }
}
