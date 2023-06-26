namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class AddAllPlayersToAction : RefereeAction
    {
        public override void Execute() =>
            GameState.PlayersInAction = new(GameState.PlayerPositions);
    }
}
