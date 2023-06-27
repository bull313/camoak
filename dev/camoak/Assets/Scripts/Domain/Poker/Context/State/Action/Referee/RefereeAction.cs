namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public abstract class RefereeAction
    {
        public PokerGameState GameState { get; set; }

        public abstract void Execute();

        public override int GetHashCode() => $"{GetType().Name}".GetHashCode();
    }
}
