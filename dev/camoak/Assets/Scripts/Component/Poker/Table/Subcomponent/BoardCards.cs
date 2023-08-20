using System.Collections.Generic;
using UnityEngine.UI;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class BoardCards : TableSubcomponent
    {
        private CardRenderer Renderer { get; set; }
        private List<Image> BoardCardImages { get; set; }

        public void Start()
        {
            Renderer = new();
            BoardCardImages = new(GetComponentsInChildren<Image>());
        }

        public override void Notify()
        {
            Renderer.ResetImages(BoardCardImages);
            Renderer.RenderSprites(BoardCardImages, GameState.BoardCards);
        }
    }
}
