using Camoak.Domain.Poker.Context.State;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class PlayerActions : PlayerMoneySubcomponent
    {
        protected override float GetMoney(PokerPlayer player) => player.Action;
    }
}
