using System.Collections.Generic;
using Camoak.Domain.Poker.Actor.Referee.Schema;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker.Actor.Referee
{
    public abstract class PokerRefereeActor
    {
        protected abstract List<IGameStateCheck> GameChecks { get; }
        protected abstract LogicSchema GameLogicSchema { get; }
        public PokerGameState GameState { get; set; }

        public RefereeActionSequence SelectActionSequence() =>
            GameLogicSchema.Evaluate(GameState, GameChecks);
    }
}
