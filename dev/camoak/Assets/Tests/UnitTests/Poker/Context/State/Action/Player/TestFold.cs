using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Player
{
    public class TestFold
    {
        private Fold foldAction;
        private PokerGameState gameState, gameStateCopy;
        private int turnPlayer;
        private List<int> expectedPlayersInAction;

        [SetUp]
        public void SetUp()
        {
            turnPlayer = 1;
            expectedPlayersInAction = new() { 1 };

            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 1, 0 })
                .SetPlayersInAction(new() { 1, 0 })
                .SetTurnPlayer(turnPlayer)
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            foldAction = new();
            foldAction.GameState = gameState;
            foldAction.Execute();
        }

        [Test]
        public void TestFoldingPlayerIsRemovedFromPlayersInAction() =>
            Assert.IsTrue(
                expectedPlayersInAction.SequenceEqual(gameState.PlayersInAction)
            );

        [Test]
        public void TestFoldOnlyAffectsPlayersInAction()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayersInAction(gameStateCopy.PlayersInAction)
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(), gameState.GetHashCode()
            );
        }
    }
}
