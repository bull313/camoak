using System.Collections.Generic;

namespace Camoak.Domain.Poker.Context.State.Cards.Selection
{
    public interface ICardSelector
    {
        public int SelectCard(List<Card> deck);
    }
}
