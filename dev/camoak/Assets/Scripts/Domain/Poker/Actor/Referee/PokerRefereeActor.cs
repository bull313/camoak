using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker.Actor.Referee
{
    public abstract class PokerRefereeActor
    {
        public PokerGameState GameState { get; set; }

        public abstract RefereeActionSequence SelectActionSequence();
    }
}
