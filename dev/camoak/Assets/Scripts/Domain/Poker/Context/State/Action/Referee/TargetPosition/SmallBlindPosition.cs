using System;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy
{
    public class SmallBlindPosition : ITargetPosition
    {
        public int GetPosition(PokerGameState gameState) =>
            Math.Abs(Math.Min(gameState.PlayerPositions.Count - 3, 0));
    }
}
