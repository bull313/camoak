using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State.Action.Player;
using TMPro;
using UnityEngine.UI;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class ActionPanel : TableSubcomponent
    {
        private List<PlayerAction> Actions { get; set; }
        public List<Button> Buttons { get; private set; }

        public void Start()
        {
            Buttons = new(GetComponentsInChildren<Button>());
            Actions = new() { new Call(), new Fold(), new Check() };
        }

        private bool IsPlayerTurn() =>
            GameState.PlayerPositions[GameState.TurnPosition]
            ==
            GameState.Player;

        private void ToggleActive(Button button)
        {
            button.gameObject.SetActive(
                IsPlayerTurn()

                /* Temporary Check Button Disable */
                && (
                    button.GetComponentInChildren<TextMeshProUGUI>().text != "CHECK"
                    ||
                    GameState.PlayerPositions[0] == 0
                )
            );
        }
            

        private void RemoveButtonListeners(Button button) =>
            button.onClick.RemoveAllListeners();

        private void SetButtonListener(int buttonIdx) =>
            Buttons[buttonIdx].onClick.AddListener(
                () => SelectionTask.Result = Actions[buttonIdx]
            );

        public override void Notify()
        {
            Buttons.ForEach(ToggleActive);
            Buttons.ForEach(RemoveButtonListeners);

            Enumerable.Range(0, Buttons.Count)
                .ToList()
                .ForEach(SetButtonListener);
        }
    }
}
