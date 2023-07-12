using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Player
{
    public class TestCall
    {
        private Call callAction;
        private PokerGameState gameState, allInGameState, gameStateCopy;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetStack(99.5f)
                    .SetAction(0.5f)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetStack(99f)
                    .SetAction(1f)
                    .Build())
                .Build();

            allInGameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(gameState.Players[0])
                    .SetAction(0f)
                    .SetStack(0.2f)
                    .Build())
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            callAction = new();
            callAction.GameState = gameState;
            callAction.Execute();

            callAction.GameState = allInGameState;
            callAction.Execute();
        }

        [Test]
        public void TestCallingPlayerBetsToMatchMaximumActionOnTable() =>
            Assert.AreEqual(1f, gameState.Players[0].Action);

        [Test]
        public void TestCallingPlayerBetsFromStack() =>
            Assert.AreEqual(99f, gameState.Players[0].Stack);

        [Test]
        public void TestCallingPlayerJustBetsStackIfUnableToMatchMaxAction() =>
            Assert.AreEqual(0.2f, allInGameState.Players[0].Action);

        [Test]
        public void TestCallingPlayerCannotMatchMaxActionLosesEntireStack() =>
            Assert.AreEqual(0f, allInGameState.Players[0].Stack);

        [Test]
        public void TestCallOnlyAffectsStackAndAction()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(gameState.Players[0])
                    .SetStack(gameStateCopy.Players[0].Stack)
                    .SetAction(gameStateCopy.Players[0].Action)
                    .Build())
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(), gameState.GetHashCode()
            );
        }
    }
}
