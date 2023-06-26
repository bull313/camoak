namespace Camoak.Domain.Poker.Context.State.Filter
{
    public abstract class FilteredPokerGameState : PokerGameState
    {
        public int Player { get; set; }

        public abstract void Update(PokerGameState gameState);
    }
}
