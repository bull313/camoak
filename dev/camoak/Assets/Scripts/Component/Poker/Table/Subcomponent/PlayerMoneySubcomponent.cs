using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using TMPro;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public abstract class PlayerMoneySubcomponent : TableSubcomponent
    {
        public const string MONETARY_FORMAT = "c";

        private List<TextMeshProUGUI> Visuals { get; set; }

        public void Start() => Visuals =
            new(GetComponentsInChildren<TextMeshProUGUI>());

        private float ScaleToBigBlindSize(float money) =>
            money * GameState.BigBlindSize;

        private string ConvertMoneyToString(float money) =>
            money.ToString(MONETARY_FORMAT);

        private List<string> GetPlayerMoney() =>
            GameState.Players.Select(GetMoney)
                .ToList()
                .Select(ScaleToBigBlindSize)
                .ToList()
                .Select(ConvertMoneyToString)
                .ToList();

        private List<KeyValuePair<TextMeshProUGUI, string>> GetVizMoneyPairs()
            => Visuals.Zip(GetPlayerMoney(), PairMoneyToVisual).ToList();

        private KeyValuePair<TextMeshProUGUI, string> PairMoneyToVisual(
            TextMeshProUGUI visual, string money
        ) => new(visual, money);

        private void RenderMoney(KeyValuePair<TextMeshProUGUI, string> pair) =>
            pair.Key.text = pair.Value;

        protected abstract float GetMoney(PokerPlayer player);

        public override void Notify() =>
            GetVizMoneyPairs().ForEach(RenderMoney);
    }
}
