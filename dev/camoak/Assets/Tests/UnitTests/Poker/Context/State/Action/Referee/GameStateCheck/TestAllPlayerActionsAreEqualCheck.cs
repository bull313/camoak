using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class TestAllPlayerActionsAreEqualCheck
    {
        private AllPlayerActionsAreEqualCheck check;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => check = new();

        [Test]
        public void TestAllEqualActionSatisfiesCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .SetAction(1f)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .SetAction(1f)
                    .Build())
                .Build();

            Assert.IsTrue(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestUnequalActionsDoesNotSatisfyCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .SetAction(1f)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .SetAction(2f)
                    .Build())
                .Build();

            Assert.IsFalse(check.IsSatisfied(gameState));
        }
    }
}
