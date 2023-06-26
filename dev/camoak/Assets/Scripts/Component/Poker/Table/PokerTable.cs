using System.Collections.Generic;
using Camoak.Component.Poker.Table.Subcomponent;
using Camoak.Domain.Poker.Actor.Player.SelectTask;
using Camoak.Domain.Poker.Context.State.Filter;
using UnityEngine;

namespace Camoak.Component.Poker.Table
{
    public class PokerTable : MonoBehaviour
    {
        public List<TableSubcomponent> Subcomponents { get; private set; }
        public FilteredPokerGameState GameState { get; set; }
        public ActionSelectionTask SelectionTask { get; set; }

        public void Start()
        {
            Subcomponents = new(GetComponentsInChildren<TableSubcomponent>());
            Subcomponents.ForEach(InitSubcomponent);
        }

        protected void InitSubcomponent(TableSubcomponent subcomponent)
        {
            subcomponent.GameState = GameState;
            subcomponent.SelectionTask = SelectionTask;
        }

        private void Notify(TableSubcomponent component) => component.Notify();

        public void UpdateView() => Subcomponents.ForEach(Notify);
    }
}
