using System.Collections.Generic;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.Sequence
{
    public class MoveToNextStreetSequence : RefereeActionSequence
    {
        public const int LEFT_OF_DEALER_POSITION = 0;
        public const int NUM_STREET_CARDS = 1;

        protected override List<RefereeAction> InitSequence() => new()
        {
            new MoveActionToCenterPot(),
            new SetTurnPosition(LEFT_OF_DEALER_POSITION),
            new DealBoardCards(NUM_STREET_CARDS, Dealer)
        };
    }
}
