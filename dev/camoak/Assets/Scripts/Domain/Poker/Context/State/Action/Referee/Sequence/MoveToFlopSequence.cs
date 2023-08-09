using System.Collections.Generic;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.Sequence
{
    public class MoveToFlopSequence : RefereeActionSequence
    {
        public const int LEFT_OF_DEALER_POSITION = 0;
        public const int NUM_FLOP_CARDS = 3;

        protected override List<RefereeAction> InitSequence() => new()
        {
            new MoveActionToCenterPot(),
            new SetTurnPosition(LEFT_OF_DEALER_POSITION),
            new DealBoardCards(NUM_FLOP_CARDS, Dealer)
        };
    }
}
