using System;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy
{
    public class SmallBlindTurn : ITurnPlayerStrategy
    {
        public int GetTurnPlayer(PokerGameState gameState) =>
            Math.Abs(Math.Min(gameState.PlayerPositions.Count - 3, 0));
    }
}
