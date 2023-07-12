namespace Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class SinglePlayerInActionCheck : IGameStateCheck
    {
        public bool IsSatisfied(PokerGameState gameState) =>
            gameState.PlayersInAction.Count == 1;
    }
}
