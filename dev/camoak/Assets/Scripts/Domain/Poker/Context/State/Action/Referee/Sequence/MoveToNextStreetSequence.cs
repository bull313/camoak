using System.Collections.Generic;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.Sequence
{
    public class MoveToNextStreetSequence : RefereeActionSequence
    {
        public const int LEFT_OF_DEALER_POSITION = 0;

        private int NumDealCards { get; set; }

        public MoveToNextStreetSequence(int numDealCards)
        {
            NumDealCards = numDealCards;
            Sequence = InitSequence();
        }

        protected override List<RefereeAction> InitSequence() => new()
        {
            new MoveActionToCenterPot(),
            new SetTurnPosition(LEFT_OF_DEALER_POSITION),
            new DealBoardCards(NumDealCards, Dealer)
        };

        public override int GetHashCode() =>
            $"{base.GetHashCode()}{NumDealCards}".GetHashCode();
    }
}
