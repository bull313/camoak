using System.Collections.Generic;
using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy.TurnPlayerPosition;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.Sequence
{
    public class MoveToNextPlayerSequence : RefereeActionSequence
    {
        protected override List<RefereeAction> InitSequence() => new()
        {
            new SetTurnPosition(new NextTurnPosition())
        };
    }
}
