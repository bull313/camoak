using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards;
using Camoak.Domain.Poker.Context.State.Cards.Deck;
using Camoak.Domain.Poker.Context.State.Cards.Filter;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Cards.Deck
{
    public class TestBasicDeckGenerator
    {
        private BasicDeckGenerator deckGenerator;
        private CardFilter cardFilter;
        private List<Card> deck, expectedDeck;

        [SetUp]
        public void SetUp()
        {
            expectedDeck = new() { Card.JACK_OF_HEARTS, Card.ACE_OF_HEARTS };
            cardFilter = new TestAceJackOfHeartsOnlyFilter();
            deckGenerator = new(cardFilter);
            deck = deckGenerator.Generate();
        }

        [Test]
        public void TestAceJackOfHeartsFilterGeneratesOnlyAceJackOfHearts() =>
            Assert.IsTrue(expectedDeck.SequenceEqual(deck));

        private class TestAceJackOfHeartsOnlyFilter : CardFilter
        {
            public override bool AllowThrough(Card card) =>
                card == Card.ACE_OF_HEARTS || card == Card.JACK_OF_HEARTS;
        }
    }
}
