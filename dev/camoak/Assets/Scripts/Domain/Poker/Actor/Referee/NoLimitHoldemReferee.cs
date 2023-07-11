using System.Collections.Generic;
using Camoak.Domain.Poker.Actor.Referee.Schema;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker.Actor.Referee
{
    public class NoLimitHoldemReferee : PokerRefereeActor
    {
        protected override List<IGameStateCheck> GameChecks => new()
        {
            new SinglePlayerInActionCheck()
        };

        protected override LogicSchema GameLogicSchema
            => new(
                new(new() { true }, new EndHandActionSequence()),
                new(new() { false }, new MoveToNextPlayerSequence())
            );
    }
}
