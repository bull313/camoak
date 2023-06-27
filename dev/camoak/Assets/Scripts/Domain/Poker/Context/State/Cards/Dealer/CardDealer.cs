using System.Collections.Generic;
using Camoak.Domain.Poker.Context.State.Cards.Deck;
using Camoak.Domain.Poker.Context.State.Cards.Selection;

namespace Camoak.Domain.Poker.Context.State.Cards.Dealer
{
    public class CardDealer
    {
        public DeckGenerator DeckGenerator { get; set; }
        public ICardSelector CardSelector { get; set; }

        public CardDealer(DeckGenerator genrator, ICardSelector selector)
        {
            DeckGenerator = genrator;
            CardSelector = selector;
        }

        public Card DrawCard()
        {
            List<Card> deck = DeckGenerator.Generate();
            return deck[CardSelector.SelectCard(deck)];
        }
    }
}
