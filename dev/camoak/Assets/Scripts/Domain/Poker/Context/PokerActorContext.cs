using System.Collections.Generic;
using Camoak.Domain.Poker.Actor.Player;
using Camoak.Domain.Poker.Actor.Referee;

namespace Camoak.Domain.Poker.Context
{
    public class PokerActorContext
    {
        public List<PokerPlayerActor> Players { get; set; }
        public PokerRefereeActor Referee { get; set; }

        public PokerActorContext() => Players = new();

        public override int GetHashCode() =>
            ($"{string.Join<PokerPlayerActor>("", Players.ToArray())}"
                + $"{Referee}").GetHashCode();
    }
}
