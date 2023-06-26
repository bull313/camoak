using Camoak.Domain.Poker.Context.State;

namespace Camoak.Domain.Poker.Context
{
    public class PokerGameContext
    {
        public PokerGameState GameState { get; set; }
        public PokerActorContext ActorContext { get; set; }

        public PokerGameContext()
        {
            GameState = new();
            ActorContext = new();
        }
    }
}
