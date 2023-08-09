using System.Collections.Generic;
using System.Linq;
using Camoak.Component.Poker.Table.Subcomponent;
using Camoak.Domain.Poker.Context.State.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class BoardCards : TableSubcomponent
    {
        public const string CARD_PATH_TEMPLATE = "Sprites/Poker/Cards/Card{0}";

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
