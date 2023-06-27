using System.Collections.Generic;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.Sequence
{
    public class EndHandActionSequence : RefereeActionSequence
    {
        public const float SMALL_BLIND_BET = 0.5f;
        public const float BIG_BLIND_BET = 1f;
        public const int NUM_HOLE_CARDS_PER_PLAYER = 2;

        protected override List<RefereeAction> InitSequence() => new()
        {
            new MoveActionToCenterPot(),
            new PayoutPot(),
            new RotatePlayerPositions(),
            new AddAllPlayersToAction(),
            new SetTurnPosition(new PreflopStartingPosition()),
            new PostBet(new SmallBlindPosition(), SMALL_BLIND_BET),
            new PostBet(new BigBlindPosition(), BIG_BLIND_BET),
            new ClearHoleCards(),
            new DealHoleCards(NUM_HOLE_CARDS_PER_PLAYER, Dealer)
        };
    }
}
