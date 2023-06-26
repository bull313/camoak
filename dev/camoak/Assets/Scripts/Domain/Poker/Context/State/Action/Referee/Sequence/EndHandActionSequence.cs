using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.Sequence
{
    public class EndHandActionSequence : RefereeActionSequence
    {
        public const float SMALL_BLIND_BET = 0.5f;
        public const float BIG_BLIND_BET = 1f;
        public const int NUM_HOLE_CARDS_PER_PLAYER = 2;

        public EndHandActionSequence() => Sequence = new()
        {
            new MoveActionToCenterPot(),
            new PayoutPot(),
            new RotatePlayerPositions(),
            new AddAllPlayersToAction(),
            new SetTurnPlayer(new PreflopStartingTurn()),
            new PostBet(new SmallBlindTurn(), SMALL_BLIND_BET),
            new PostBet(new BigBlindTurn(), BIG_BLIND_BET),
            new ClearHoleCards(),
            new DealHoleCards(NUM_HOLE_CARDS_PER_PLAYER, Dealer)
        };
    }
}
