using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using TMPro;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public abstract class PlayerMoneySubComponent : TableSubcomponent
    {
        public const string MONETARY_FORMAT = "c";

        private List<TextMeshProUGUI> Visuals { get; set; }

        protected abstract float GetMoney(PokerPlayer player);

        public void Start() => Visuals =
            new(GetComponentsInChildren<TextMeshProUGUI>());

        private string ConvertMoneyToString(float money) =>
            money.ToString(MONETARY_FORMAT);

        private List<string> GetPlayerMoney() =>
            GameState.Players.Select(GetMoney)
                .ToList()
                .Select(ConvertMoneyToString)
                .ToList();

        private KeyValuePair<TextMeshProUGUI, string> PairMoneyToVisual(
            TextMeshProUGUI visual, string money
        ) => new(visual, money);

        private void RenderMoney(KeyValuePair<TextMeshProUGUI, string> pair) =>
            pair.Key.text = pair.Value;

        public override void Notify() =>
            Visuals.Zip(GetPlayerMoney(), PairMoneyToVisual)
                .ToList()
                .ForEach(RenderMoney);
    }
}
