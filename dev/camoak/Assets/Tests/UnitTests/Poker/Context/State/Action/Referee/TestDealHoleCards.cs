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
    public class TestDealHoleCards
    {
        private DealHoleCards dealAction;
        private PokerGameState gameState, gameStateCopy;
        private List<Card> expectedPlayer1Cards, expectedPlayer2Cards;

        [SetUp]
        public void SetUp()
        {
            expectedPlayer1Cards = new()
            {
                Card.ACE_OF_DIAMONDS,
                Card.TWO_OF_DIAMONDS
            };

            expectedPlayer2Cards = new()
            {
                Card.FIVE_OF_HEARTS,
                Card.SIX_OF_DIAMONDS
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
                2,
                new CardDealer(
                    new BasicDeckGenerator(new SeenCardsFilter()),
                    new TestFourCardSelector()
                )
            );

            dealAction.GameState = gameState;
            dealAction.Execute();
        }

        [Test]
        public void TestFourPlayerHoleCardsAreDealtToPlayers()
        {
            Assert.IsTrue(
                expectedPlayer1Cards.SequenceEqual(
                    gameState.Players[0].HoleCards
                )
            );

            Assert.IsTrue(
                expectedPlayer2Cards.SequenceEqual(
                    gameState.Players[1].HoleCards
                )
            );
        }

        [Test]
        public void TestDealActionOnlyAffectsPlayerHoleCards()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(gameState.Players[0])
                    .SetHoleCards(gameStateCopy.Players[0].HoleCards)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(gameState.Players[1])
                    .SetHoleCards(gameStateCopy.Players[1].HoleCards)
                    .Build())
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }
    }

    internal class TestFourCardSelector : ICardSelector
    {
        private int counter;
        private readonly Card[] dealCards;

        public TestFourCardSelector()
        {
            counter = 0;
            dealCards = new Card[]
            {
                Card.ACE_OF_DIAMONDS,
                Card.TWO_OF_DIAMONDS,
                Card.FIVE_OF_HEARTS,
                Card.SIX_OF_DIAMONDS
            };
        }

        public int SelectCard(List<Card> deck) =>
            deck.IndexOf(
                dealCards[counter++ % dealCards.Length]
            );
    }
}
