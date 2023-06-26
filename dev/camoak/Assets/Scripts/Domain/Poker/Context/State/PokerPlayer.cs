using System.Collections.Generic;
using Camoak.Domain.Poker.Context.State.Cards;

namespace Camoak.Domain.Poker.Context.State
{
    public class PokerPlayer
    {
        public float Stack { get; internal set; }
        public float Action { get; internal set; }
        public List<Card> HoleCards { get; internal set; }

        public PokerPlayer() => HoleCards = new();

        public override int GetHashCode() =>
            $"{Stack}{Action}{string.Join("", HoleCards.ToArray())}"
                .GetHashCode();
    }
}
