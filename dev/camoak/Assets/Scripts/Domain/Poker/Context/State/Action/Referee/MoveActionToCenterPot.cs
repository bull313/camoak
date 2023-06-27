using System.Linq;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class MoveActionToCenterPot : RefereeAction
    {
        public const float EMPTY_ACTION = 0f;

        private float AddActionToPot(float currentPot, PokerPlayer player) =>
            currentPot + player.Action;

        private void ClearAction(PokerPlayer player) =>
            player.Action = EMPTY_ACTION;

        public override void Execute()
        {
            GameState.CenterPot = GameState.Players.Aggregate(
                GameState.CenterPot, AddActionToPot
            );

            GameState.Players.ForEach(ClearAction);
        }
    }
}
