using System.Threading.Tasks;
using Camoak.Domain.Poker.Context.State.Action.Player;

namespace Camoak.Domain.Poker.Actor.Player.SelectTask
{
    public class ActionSelectionTask
    {
        private TaskCompletionSource<PlayerAction> CompletionSource
        { get; set; }

        public Task Task { get => CompletionSource?.Task; }

        public PlayerAction Result
        {
            get => CompletionSource?.Task.Result;
            set => CompletionSource?.SetResult(value);
        }

        public ActionSelectionTask() => Reset();

        public void Reset() => CompletionSource = new();
    }
}
