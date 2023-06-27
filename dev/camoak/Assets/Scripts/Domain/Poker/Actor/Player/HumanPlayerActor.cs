using System.Threading.Tasks;
using Camoak.Domain.Poker.Actor.Player.SelectTask;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Domain.Poker.Context.State.Filter;

namespace Camoak.Domain.Poker.Actor.Player
{
    public class HumanPlayerActor : PokerPlayerActor
    {
        private ActionSelectionTask SelectionTask { get; set; }

        public HumanPlayerActor(FilteredPokerGameState g, ActionSelectionTask t)
            : base(g) => SelectionTask = t;

        public async override Task<PlayerAction> SelectAction()
        {
            await SelectionTask.Task;
            return SelectionTask.Result;
        }
    }
}
