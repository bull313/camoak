using Camoak.Domain.Poker.Context.State;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class PlayerActions : PlayerMoneySubComponent
    {
        protected override float GetMoney(PokerPlayer player) => player.Action;
    }
}
