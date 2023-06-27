namespace Camoak.Domain.Poker.Context.State.Action.Player
{
    public abstract class PlayerAction
    {
        public PokerGameState GameState { get; set; }

        public abstract void Execute();
    }
}
