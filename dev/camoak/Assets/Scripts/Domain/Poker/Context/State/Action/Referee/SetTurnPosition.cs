using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class SetTurnPosition : RefereeAction
    {
        private ITargetPosition NextTurnPosition { get; set; }

        public SetTurnPosition(ITargetPosition nextTurnPosition) : base() =>
            NextTurnPosition = nextTurnPosition;

        public override void Execute() =>
            GameState.TurnPosition = NextTurnPosition.GetPosition(GameState);
    }
}
