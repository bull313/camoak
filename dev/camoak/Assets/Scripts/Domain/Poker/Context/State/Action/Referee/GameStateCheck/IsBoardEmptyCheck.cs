namespace Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class IsBoardEmptyCheck : IGameStateCheck
    {
        public bool IsSatisfied(PokerGameState gameState)
            => gameState.BoardCards.Count == 0;
    }
}
