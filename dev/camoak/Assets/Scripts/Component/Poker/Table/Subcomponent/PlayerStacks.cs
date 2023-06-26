using Camoak.Domain.Poker.Context.State;

namespace Camoak.Component.Poker.Table.Subcomponent
{
    public class PlayerStacks : PlayerMoneySubComponent
    {
        protected override float GetMoney(PokerPlayer player) => player.Stack;
    }
}
