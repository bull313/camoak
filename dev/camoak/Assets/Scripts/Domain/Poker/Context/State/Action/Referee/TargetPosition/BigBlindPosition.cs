using System;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy
{
    public class BigBlindPosition : ITargetPosition
    {
        public int GetPosition(PokerGameState gameState) =>
            Math.Min(1, gameState.PlayerPositions.Count - 2);
    }
}
