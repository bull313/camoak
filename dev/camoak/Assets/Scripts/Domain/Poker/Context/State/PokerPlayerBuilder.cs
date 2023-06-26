using System.Collections.Generic;
using Camoak.Domain.Poker.Context.State.Cards;

namespace Camoak.Domain.Poker.Context.State
{
    public class PokerPlayerBuilder
    {
        private PokerPlayer Player { get; set; }

        private PokerPlayerBuilder() => Player = new();

        public static PokerPlayerBuilder Create() => new();

        public PokerPlayerBuilder Copy(PokerPlayer player)
        {
            Player = Create()
                .SetStack(player.Stack)
                .SetAction(player.Action)
                .SetHoleCards(player.HoleCards)
                .Build();

            return this;
        }

        public PokerPlayerBuilder SetStack(float stack)
        {
            Player.Stack = stack;
            return this;
        }

        public PokerPlayerBuilder SetAction(float action)
        {
            Player.Action = action;
            return this;
        }

        public PokerPlayerBuilder SetHoleCards(List<Card> holeCards)
        {
            Player.HoleCards = new(holeCards);
            return this;
        }

        public PokerPlayerBuilder SetHoleCard(Card card, int idx)
        {
            Player.HoleCards[idx] = card;
            return this;
        }

        public PokerPlayer Build() => Player;
    }
}
