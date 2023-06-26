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

        private List<Image> HoleCardImages { get; set; }

        public void Start() =>
            HoleCardImages = new(GetComponentsInChildren<Image>());

        private List<Card> GetHoleCards(PokerPlayer player) => player.HoleCards;

        private Sprite GetSprite(Card c) =>
            Resources.Load<Sprite>(string.Format(CARD_PATH_TEMPLATE, (int)c));

        private List<Sprite> ConvertCardsToSprites(List<Card> holeCards) =>
            holeCards.Select(GetSprite).ToList();

        private List<Sprite> IncludeAll(List<Sprite> l) => l;

        private KeyValuePair<Image, Sprite> PairImageToSprite(Image i, Sprite s)
            => new(i, s);

        private void RenderSpriteToImage(KeyValuePair<Image, Sprite> pair) =>
            pair.Key.sprite = pair.Value;

        private List<Sprite> GetCardSprites() =>
            GameState.Players.Select(GetHoleCards)
                    .ToList()
                    .Select(ConvertCardsToSprites)
                    .ToList()
                    .SelectMany(IncludeAll)
                    .ToList();

        private List<KeyValuePair<Image, Sprite>> GetSpriteImagePairs() =>
            HoleCardImages.Zip(GetCardSprites(), PairImageToSprite).ToList();

        public override void Notify() =>
            GetSpriteImagePairs().ForEach(RenderSpriteToImage);
    }
}
