namespace Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy
{
    public interface ITargetPosition
    {
        public int GetPosition(PokerGameState gameState);
    }
}
