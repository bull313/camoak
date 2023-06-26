using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker.Actor.Referee
{
    public class NoLimitHoldemReferee : PokerRefereeActor
    {
        public override RefereeActionSequence SelectActionSequence() =>
            new EndHandActionSequence();
    }
}
