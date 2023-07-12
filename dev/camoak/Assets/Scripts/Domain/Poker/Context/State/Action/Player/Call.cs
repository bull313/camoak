using System;
using System.Linq;

namespace Camoak.Domain.Poker.Context.State.Action.Player
{
    public class Call : PlayerAction
    {
        private PokerPlayer GetTurnPlayer() =>
            GameState.Players[
                GameState.PlayersInAction[GameState.TurnPosition]
            ];

        private float GetPlayerAction(PokerPlayer player) => player.Action;

        private float GetMaxAction() => GameState.Players.Max(GetPlayerAction);

        private float GetAmountToMatch() =>
            GetMaxAction() - GetTurnPlayer().Action;

        public override void Execute()
        {
            float betSize = Math.Min(GetAmountToMatch(), GetTurnPlayer().Stack);

            GetTurnPlayer().Stack -= betSize;
            GetTurnPlayer().Action += betSize;
        }
    }
}
