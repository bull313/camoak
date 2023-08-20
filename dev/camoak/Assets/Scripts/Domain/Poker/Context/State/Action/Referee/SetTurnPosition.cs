using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class SetTurnPosition : RefereeAction
    {
        private ITargetPosition TurnPosition { get; set; }
        private int TurnPositionIndex { get; set; }

        public SetTurnPosition(ITargetPosition t) : base() => TurnPosition = t;

        public SetTurnPosition(int p) : base() => TurnPositionIndex = p;

        public override void Execute() =>
            GameState.TurnPosition = TurnPosition?.GetPosition(GameState)
            ?? TurnPositionIndex;
    }
}
