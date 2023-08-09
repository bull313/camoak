using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards.Dealer;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class DealBoardCards : RefereeAction
    {
        private int NumBoardCards { get; set; }
        private CardDealer Dealer { get; set; }

        public DealBoardCards(int numBoardCards, CardDealer dealer)
        {
            NumBoardCards = numBoardCards;
            Dealer = dealer;
        }

        private void DealBoardCard(int _) =>
            GameState.BoardCards.Add(Dealer.DrawCard());

        public override void Execute()
        {
            Dealer.DeckGenerator.Filter.GameState = GameState;
            Enumerable.Range(0, NumBoardCards).ToList().ForEach(DealBoardCard);
        }
    }
}
