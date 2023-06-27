using System.Threading.Tasks;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Domain.Poker.Context.State.Filter;

namespace Camoak.Domain.Poker.Actor.Player
{
    public abstract class PokerPlayerActor
    {
        public virtual FilteredPokerGameState GameState { get; }

        public PokerPlayerActor(FilteredPokerGameState gameState) =>
            GameState = gameState;

        public abstract Task<PlayerAction> SelectAction();
    }
}
