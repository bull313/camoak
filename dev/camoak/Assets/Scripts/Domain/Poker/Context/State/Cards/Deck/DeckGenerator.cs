using System.Collections.Generic;
using Camoak.Domain.Poker.Context.State.Cards.Filter;

namespace Camoak.Domain.Poker.Context.State.Cards.Deck
{
    public abstract class DeckGenerator
    {
        public CardFilter Filter { get; private set; }

        public DeckGenerator(CardFilter filter) => Filter = filter;

        public abstract List<Card> Generate();
    }
}
