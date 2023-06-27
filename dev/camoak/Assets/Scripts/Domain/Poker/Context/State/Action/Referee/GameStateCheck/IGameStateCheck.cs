namespace Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck
{
    public interface IGameStateCheck
    {
        public abstract bool IsSatisfied(PokerGameState gameState);
    }
}
