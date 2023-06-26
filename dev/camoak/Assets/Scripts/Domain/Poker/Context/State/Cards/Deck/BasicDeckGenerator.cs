using System;
using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards.Filter;

namespace Camoak.Domain.Poker.Context.State.Cards.Deck
{
    public class BasicDeckGenerator : DeckGenerator
    {
        public BasicDeckGenerator(CardFilter filter) : base(filter)
        { }

        private List<Card> GenerateFullDeck() => Enum.GetValues(typeof(Card))
            .Cast<Card>()
            .ToList();

        public override List<Card> Generate() => GenerateFullDeck()
            .Where(Filter.AllowThrough)
            .ToList();
    }
}
