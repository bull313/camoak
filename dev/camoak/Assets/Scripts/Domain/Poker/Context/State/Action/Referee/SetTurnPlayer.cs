using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class SetTurnPlayer : RefereeAction
    {
        private ITurnPlayerStrategy NextTurnStrategy { get; set; }

        public SetTurnPlayer(ITurnPlayerStrategy nextTurnStrategy) : base() =>
            NextTurnStrategy = nextTurnStrategy;

        public override void Execute() =>
            GameState.TurnPlayer = NextTurnStrategy.GetTurnPlayer(GameState);
    }
}
