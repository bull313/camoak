using TMPro;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class CenterPot : TableSubcomponent
    {
        private TextMeshProUGUI PotVisual { get; set; }

        public void Start() => PotVisual = GetComponent<TextMeshProUGUI>();

        private string GetCenterPot() =>
            GameState.CenterPot
                .ToString(PlayerMoneySubcomponent.MONETARY_FORMAT);

        public override void Notify() =>
            PotVisual.text = GetCenterPot();
    }
}
