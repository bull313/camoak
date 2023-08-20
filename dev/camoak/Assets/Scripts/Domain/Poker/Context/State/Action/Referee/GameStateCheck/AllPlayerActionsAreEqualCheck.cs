using System.Collections.Generic;
using System.Linq;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class AllPlayerActionsAreEqualCheck : IGameStateCheck
    {
        private float? UniversalAction { get; set; }

        private bool IsUniversalAction(float a) => UniversalAction == a;

        private float GetAction(PokerPlayer player) => player.Action;

        private List<float> GetPlayerActions(PokerGameState gameState) =>
            gameState.Players.Select(GetAction).ToList();

        public bool IsSatisfied(PokerGameState gameState)
        {
            UniversalAction = (gameState.Players.Count > 0)
                ? gameState.Players[0].Action : null;

            return GetPlayerActions(gameState).All(IsUniversalAction);
        }
    }
}
