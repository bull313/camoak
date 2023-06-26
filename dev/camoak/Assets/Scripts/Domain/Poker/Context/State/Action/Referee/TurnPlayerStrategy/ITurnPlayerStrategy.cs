namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy
{
    public interface ITurnPlayerStrategy
    {
        public int GetTurnPlayer(PokerGameState gameState);
    }
}
