using System.Collections.Generic;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy.TurnPlayerPosition
{
    public class NextTurnPosition : ITargetPosition
    {
        private const int DO_NOT_SHIFT = 0;
        private const int SHIFT = 1;

        private bool IsPlayerInAction(PokerGameState gameState) =>
            new HashSet<int>(gameState.PlayersInAction).Contains(
                gameState.PlayerPositions[gameState.TurnPosition]
            );

        private int GetNewTurnPosition(PokerGameState gameState) =>
            gameState.TurnPosition +
                (IsPlayerInAction(gameState) ? SHIFT : DO_NOT_SHIFT);

        public int GetPosition(PokerGameState gameState) =>
            GetNewTurnPosition(gameState)
                % gameState.PlayersInAction.Count;
            
    }
}
