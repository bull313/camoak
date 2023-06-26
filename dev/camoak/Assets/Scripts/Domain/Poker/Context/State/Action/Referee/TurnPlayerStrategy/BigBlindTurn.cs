using System;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy
{
    public class BigBlindTurn : ITurnPlayerStrategy
    {
        public int GetTurnPlayer(PokerGameState gameState) =>
            Math.Min(1, gameState.PlayerPositions.Count - 2);
    }
}
