namespace Camoak.Domain.Poker.Context.State.Cards.Filter
{
    public abstract class CardFilter
    {
        public PokerGameState GameState { get; set; }

        public abstract bool AllowThrough(Card card);
    }
}
