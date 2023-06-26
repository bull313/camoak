using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestAddAllPlayersToAction
    {
        private AddAllPlayersToAction addAction;
        private PokerGameState gameState, gameStateCopy;
        private List<int> expectedPlayersInAction;

        [SetUp]
        public void SetUp()
        {
            expectedPlayersInAction = new() { 0, 1, 2, 3, 4 };

            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayerPositions(new() { 0, 1, 2, 3, 4 })
                .SetPlayersInAction(new() { 0, 3, 4 })
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            addAction = new();
            addAction.GameState = gameState;
            addAction.Execute();
        }

        [Test]
        public void TestPlayersInActionResetToCopyOfPlayerPositions() =>
            Assert.IsTrue(
                expectedPlayersInAction.SequenceEqual(gameState.PlayersInAction)
            );

        [Test]
        public void TestAddAllPlayersToActionOnlyAffectsPlayersInAction()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayersInAction(gameStateCopy.PlayersInAction)
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }
    }
}
