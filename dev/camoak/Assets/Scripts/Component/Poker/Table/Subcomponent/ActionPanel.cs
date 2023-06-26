using Camoak.Domain.Poker.Context.State.Action.Player;
using UnityEngine.UI;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class ActionPanel : TableSubcomponent
    {
        public Button FoldButton { get; private set; }

        public void Start() => FoldButton = GetComponentInChildren<Button>();

        private void CompleteTaskWithFold() =>
            SelectionTask.Result = new Fold();

        private bool IsPlayerTurn() =>
            GameState.PlayerPositions[GameState.TurnPlayer] == GameState.Player;

        public override void Notify()
        {
            FoldButton.gameObject.SetActive(IsPlayerTurn());
            FoldButton.onClick.RemoveAllListeners();
            FoldButton.onClick.AddListener(CompleteTaskWithFold);
        }
    }
}
