using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestMoveActionToCenterPot
    {
        private MoveActionToCenterPot moveAction;
        private PokerGameState gameState, gameStateCopy;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetCenterPot(6f)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetAction(1f)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[1])
                    .SetAction(3f)
                    .Build())
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            moveAction = new();
            moveAction.GameState = gameState;
            moveAction.Execute();
        }

        [Test]
        public void TestAllPlayersHaveEmptyAction()
        {
            Assert.AreEqual(0f, gameState.Players[0].Action);
            Assert.AreEqual(0f, gameState.Players[1].Action);
        }

        [Test]
        public void TestCenterPotNowIncludesAllPlayerActions() =>
            Assert.AreEqual(10f, gameState.CenterPot);

        [Test]
        public void TestMoveActionToCenterPotOnlyAffectsActionsAndPot()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetCenterPot(gameStateCopy.CenterPot)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetAction(gameStateCopy.Players[0].Action)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[1])
                    .SetAction(gameStateCopy.Players[1].Action)
                    .Build())
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }
    }
}
