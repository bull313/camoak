using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Cards;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class TestIsBoardEmptyCheck
    {
        private IsBoardEmptyCheck check;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp() => check = new();

        [Test]
        public void TestBoardHasNoCardsSatisfiesCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetBoardCards(new())
                .Build();

            Assert.IsTrue(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestBoardHasAtLeastOneCardDoesNotSatisfyCheck()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetBoardCards(new()
                {
                    Card.QUEEN_OF_HEARTS,
                    Card.THREE_OF_DIAMONDS,
                    Card.QUEEN_OF_SPADES
                })
                .Build();

            Assert.IsFalse(check.IsSatisfied(gameState));
        }
    }
}
