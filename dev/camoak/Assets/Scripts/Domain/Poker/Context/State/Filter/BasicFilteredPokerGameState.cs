using System.Linq;

namespace Camoak.Domain.Poker.Context.State.Filter
{
    public class BasicFilteredPokerGameState : FilteredPokerGameState
    {
        private void GetGameStateData(PokerGameState gameState)
        {
            Players = gameState.Players;
            BigBlindSize = gameState.BigBlindSize;
            PlayerPositions = gameState.PlayerPositions;
            PlayersInAction = gameState.PlayersInAction;
            TurnPlayer = gameState.TurnPlayer;
            CenterPot = gameState.CenterPot;
        }

        private void ClearHoleCards(PokerPlayer p) => p.HoleCards.Clear();

        private bool IsOpponent(PokerPlayer _, int p) => p != Player;

        private void HideOpponentHoleCards(PokerGameState gameState) =>
            gameState.Players.Where(IsOpponent)
                .ToList()
                .ForEach(ClearHoleCards);

        public override void Update(PokerGameState gameState)
        {
            GetGameStateData(gameState);
            HideOpponentHoleCards(gameState);
        }
    }
}
