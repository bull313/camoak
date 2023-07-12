using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class SetTurnPosition : RefereeAction
    {
        private ITargetPosition TurnPosition { get; set; }

        public SetTurnPosition(ITargetPosition t) : base() => TurnPosition = t;

        public override void Execute() =>
            GameState.TurnPosition = TurnPosition.GetPosition(GameState);
    }
}
