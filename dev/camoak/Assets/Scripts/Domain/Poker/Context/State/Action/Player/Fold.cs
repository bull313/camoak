namespace Camoak.Domain.Poker.Context.State.Action.Player
{
    public class Fold : PlayerAction
    {
        public override void Execute() =>
            GameState.PlayersInAction.Remove(GameState.PlayersInAction[
                GameState.TurnPosition
            ]);
    }
}
