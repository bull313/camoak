namespace Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class IsButtonPlayerTurnCheck : IGameStateCheck
    {
        public bool IsSatisfied(PokerGameState gameState)
            => gameState.TurnPosition == gameState.PlayersInAction.Count - 1;
    }
}
