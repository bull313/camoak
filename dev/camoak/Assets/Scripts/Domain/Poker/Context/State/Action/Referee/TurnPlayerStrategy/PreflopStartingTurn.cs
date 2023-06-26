using System;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy
{
    public class PreflopStartingTurn : ITurnPlayerStrategy
    {
        private const int LEFT_OF_BIG_BLIND = 2;

        public int GetTurnPlayer(PokerGameState gameState) =>
            Math.Min(LEFT_OF_BIG_BLIND, gameState.PlayerPositions.Count - 1);
    }
}
