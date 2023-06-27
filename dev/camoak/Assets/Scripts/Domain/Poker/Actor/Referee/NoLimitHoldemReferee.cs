using System.Collections.Generic;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker.Actor.Referee
{
    public class NoLimitHoldemReferee : PokerRefereeActor
    {
        protected override List<IGameStateCheck> InitGameChecks() => new()
        {
            new SinglePlayerInActionCheck()
        };

        protected override
            List<KeyValuePair<List<bool?>, RefereeActionSequence>>
                GetLogicMap() => new()
                {
                    new(new() { true }, new EndHandActionSequence()),
                    new(new() { false }, new MoveToNextPlayerSequence())
                };
    }
}
