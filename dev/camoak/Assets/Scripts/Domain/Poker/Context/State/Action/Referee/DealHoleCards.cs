using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards.Dealer;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class DealHoleCards : RefereeAction
    {
        public CardDealer Dealer { get; set; }
        public int CardsPerPlayer;

        public DealHoleCards(int cpp, CardDealer dealer)
        {
            CardsPerPlayer = cpp;
            Dealer = dealer;
        }

        private void DrawCard(PokerPlayer player) =>
            player.HoleCards.Add(Dealer.DrawCard());

        private void DealCardsToPlayer(PokerPlayer player) =>
            Enumerable.Range(0, CardsPerPlayer)
                .ToList()
                .ForEach(_ => DrawCard(player));

        public override void Execute()
        {
            Dealer.DeckGenerator.Filter.GameState = GameState;
            GameState.Players.ForEach(DealCardsToPlayer);
        }
    }
}
