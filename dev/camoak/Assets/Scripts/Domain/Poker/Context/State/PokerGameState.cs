using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards;

namespace Camoak.Domain.Poker.Context.State
{
    public class PokerGameState
    {
        public List<PokerPlayer> Players { get; internal set; }
        public int BigBlindSize { get; internal set; }
        public List<int> PlayerPositions { get; internal set; }
        public List<int> PlayersInAction { get; internal set; }
        public int TurnPosition { get; internal set; }
        public float CenterPot { get; internal set; }
        public List<Card> BoardCards { get; internal set; }

        public PokerGameState()
        {
            Players = new();
            PlayerPositions = new();
            PlayersInAction = new();
            BoardCards = new();
        }

        private int GetPlayerHashCode(PokerPlayer player) => 
            player.GetHashCode();

        public override int GetHashCode() => (
                $"{string.Join("", Players.Select(GetPlayerHashCode).ToArray())}"
                + $"{BigBlindSize}"
                + $"{string.Join("", PlayerPositions.ToArray())}"
                + $"{string.Join("", PlayersInAction.ToArray())}"
                + $"{TurnPosition}"
                + $"{CenterPot}"
                + $"{string.Join("", BoardCards.ToArray())}"
                ).GetHashCode();
    }
}
