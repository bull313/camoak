using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Camoak.Component.Poker.Table
{
    using ImageSpritePair = List<KeyValuePair<Image, Sprite>>;

    public class CardRenderer
    {
        public const string CARD_PATH_TEMPLATE = "Sprites/Poker/Cards/Card{0}";

        public Sprite GetSprite(Card c) =>
            Resources.Load<Sprite>(string.Format(CARD_PATH_TEMPLATE, (int)c));

        private void RenderSpriteToImage(KeyValuePair<Image, Sprite> pair) =>
            pair.Key.sprite = pair.Value;

        private KeyValuePair<Image, Sprite> PairImageToSprite(Image i, Sprite s)
            => new(i, s);

        private List<Sprite> GetCardSprites(List<Card> cards) =>
            cards.Select(GetSprite).ToList();

        private ImageSpritePair GetSpriteImagePairs(List<Image> i, List<Card> c)
            => i.Zip(GetCardSprites(c), PairImageToSprite).ToList();

        private Sprite GetFaceDownCardSprite() =>
            Resources.Load<Sprite>(string.Format(CARD_PATH_TEMPLATE, ""));

        private void ResetImage(Image image) =>
            image.sprite = GetFaceDownCardSprite();

        public void ResetImages(List<Image> images) =>
            images.ForEach(ResetImage);

        public void RenderSprites(List<Image> images, List<Card> cards) =>
            GetSpriteImagePairs(images, cards).ForEach(RenderSpriteToImage);
    }
}
