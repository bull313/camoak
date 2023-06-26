using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class PlayerPositions : TableSubcomponent
    {
        private List<Image> PositionImages { get; set; }

        public void Start() =>
            PositionImages = new(GetComponentsInChildren<Image>());

        private bool IsButtonPlayer(int playerPosition) =>
            GameState.PlayerPositions[^1] == playerPosition;

        private void SetImageActive(Image image, bool isActive) =>
            image.gameObject.SetActive(isActive);

        private void UpdateActiveState(int player) =>
            SetImageActive(PositionImages[player], IsButtonPlayer(player));

        public override void Notify() =>
            Enumerable.Range(0, PositionImages.Count)
                .ToList()
                .ForEach(UpdateActiveState);
    }
}
