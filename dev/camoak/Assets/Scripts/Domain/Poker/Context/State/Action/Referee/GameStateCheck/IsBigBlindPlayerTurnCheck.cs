using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class IsBigBlindPlayerTurnCheck : IGameStateCheck
    {
        public bool IsSatisfied(PokerGameState gameState) =>
            gameState.TurnPosition
            ==
            new BigBlindPosition().GetPosition(gameState);
    }
}
