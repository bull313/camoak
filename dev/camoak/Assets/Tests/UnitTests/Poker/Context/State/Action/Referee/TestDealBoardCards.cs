using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Domain.Poker.Context.State.Cards;
using Camoak.Domain.Poker.Context.State.Cards.Dealer;
using Camoak.Domain.Poker.Context.State.Cards.Deck;
using Camoak.Domain.Poker.Context.State.Cards.Filter;
using Camoak.Domain.Poker.Context.State.Cards.Selection;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestDealBoardCards
    {
        private DealBoardCards dealAction;
        private PokerGameState gameState, gameStateCopy;
        private List<Card> expectedBoardCards;

        [SetUp]
        public void SetUp()
        {
            expectedBoardCards = new()
            {
                Card.TEN_OF_SPADES,
                Card.TWO_OF_CLUBS,
                Card.QUEEN_OF_SPADES
            };

            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetHoleCards(new())
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[1])
                    .SetHoleCards(new())
                    .Build())
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            dealAction = new(
                3,
                new CardDealer(
                    new BasicDeckGenerator(new SeenCardsFilter()),
                    new TestDealBoardCardsSelector()
                )
            );

            dealAction.GameState = gameState;
            dealAction.Execute();
        }

        [Test]
        public void TestBoardCardsAreDealt()
        {
            Assert.IsTrue(
                expectedBoardCards.SequenceEqual(
                    gameState.BoardCards
                )
            );
        }

        [Test]
        public void TestDealActionOnlyAffectsPlayerHoleCards()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetBoardCards(gameStateCopy.BoardCards)
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }
    }

    internal class TestDealBoardCardsSelector : ICardSelector
    {
        private int counter;
        private readonly Card[] dealCards;

        public TestDealBoardCardsSelector()
        {
            counter = 0;
            dealCards = new Card[]
            {
                Card.TEN_OF_SPADES,
                Card.TWO_OF_CLUBS,
                Card.QUEEN_OF_SPADES,
                Card.ACE_OF_HEARTS
            };
        }

        public int SelectCard(List<Card> deck) =>
            deck.IndexOf(
                dealCards[counter++ % dealCards.Length]
            );
    }
}
