using System.Collections.Generic;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy.TurnPlayerPosition
{
    public class NextTurnPosition : ITargetPosition
    {
        private bool PlayerInAction(PokerGameState gameState) =>
            new HashSet<int>(gameState.PlayersInAction).Contains(
                gameState.PlayerPositions[gameState.TurnPosition]
            );

        private int GetIncrementedTurnPlayer(PokerGameState gameState) =>
            gameState.TurnPosition + (PlayerInAction(gameState) ? 1 : 0);

        public int GetPosition(PokerGameState gameState) =>
            GetIncrementedTurnPlayer(gameState)
                % gameState.PlayersInAction.Count;
            
    }
}
