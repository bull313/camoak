using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Cards;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class TestNumCardsOnBoardCheck
    {
        private NumCardsOnBoardCheck check;
        private PokerGameState gameState;

        [Test]
        public void TestBoardHasNoCardsSatisfiesZeroCheck()
        {
            check = new(0);
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetBoardCards(new())
                .Build();

            Assert.IsTrue(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestBoardHasAtLeastOneCardDoesNotSatisfyZeroCheck()
        {
            check = new(0);
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

        [Test]
        public void TestBoardHasNoCardsDoesNotSatisfyThreeCheck()
        {
            check = new(3);
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetBoardCards(new())
                .Build();

            Assert.IsFalse(check.IsSatisfied(gameState));
        }

        [Test]
        public void TestBoardHasThreeCardsSatisfiesThreeCheck()
        {
            check = new(3);
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetBoardCards(new()
                {
                    Card.QUEEN_OF_HEARTS,
                    Card.THREE_OF_DIAMONDS,
                    Card.QUEEN_OF_SPADES
                })
                .Build();

            Assert.IsTrue(check.IsSatisfied(gameState));
        }
    }
}
