using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards.Filter;

namespace Camoak.Domain.Poker.Context.State.Cards.Deck
{
    public abstract class DeckGenerator
    {
        public CardFilter Filter { get; private set; }

        public DeckGenerator(CardFilter filter) => Filter = filter;

        protected abstract List<Card> CreateCards();

        public List<Card> Generate() => CreateCards()
            .Where(Filter.AllowThrough)
            .ToList();
    }
}
