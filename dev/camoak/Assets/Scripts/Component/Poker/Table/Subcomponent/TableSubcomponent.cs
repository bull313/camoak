using Camoak.Domain.Poker.Actor.Player.SelectTask;
using Camoak.Domain.Poker.Context.State.Filter;
using UnityEngine;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public abstract class TableSubcomponent : MonoBehaviour
    {
        public FilteredPokerGameState GameState { get; set; }
        public ActionSelectionTask SelectionTask { get; set; }

        public abstract void Notify();
    }
}
