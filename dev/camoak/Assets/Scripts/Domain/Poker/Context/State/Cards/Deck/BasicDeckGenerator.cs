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

        protected override List<Card> CreateCards() =>
            Enum.GetValues(typeof(Card)).Cast<Card>().ToList();
    }
}
