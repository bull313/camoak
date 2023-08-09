using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class PlayerHoleCards : TableSubcomponent
    {
        public const string CARD_PATH_TEMPLATE = "Sprites/Poker/Cards/Card{0}";

        private CardRenderer Renderer { get; set; }
        private List<Image> HoleCardImages { get; set; }

        public void Start()
        {
            Renderer = new();
            HoleCardImages = new(GetComponentsInChildren<Image>());
        }

        private List<Card> GetHoleCards(PokerPlayer player) => player.HoleCards;

        private List<Card> IncludeAll(List<Card> l) => l;

        private List<Card> GetPlayerHoleCards() =>
            GameState.Players.Select(GetHoleCards)
                .ToList()
                .SelectMany(IncludeAll)
                .ToList();

        public override void Notify() =>
            Renderer.RenderSprites(HoleCardImages, GetPlayerHoleCards());
    }
}
