using System.Collections.Generic;

namespace Camoak.Domain.Poker.Context.State.Cards.Filter
{
    public class SeenCardsFilter : CardFilter
    {
        private HashSet<Card> SeenCards { get; set; }

        public SeenCardsFilter() => SeenCards = new();

        private void AddSeenCard(Card card) => SeenCards.Add(card);

        private void SeePlayerHoleCards(PokerPlayer player) =>
            player.HoleCards.ForEach(AddSeenCard);

        private void CollectSeenCards()
        {
            SeenCards.Clear();
            GameState.Players.ForEach(SeePlayerHoleCards);
        }

        public override bool AllowThrough(Card card)
        {
            CollectSeenCards();
            return SeenCards.Contains(card) == false;
        }
    }
}
