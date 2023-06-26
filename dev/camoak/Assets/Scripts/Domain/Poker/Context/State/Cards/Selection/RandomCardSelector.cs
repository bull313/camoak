using System;
using System.Collections.Generic;

namespace Camoak.Domain.Poker.Context.State.Cards.Selection
{
    public class RandomCardSelector : ICardSelector
    {
        private readonly Random randomizer;

        public RandomCardSelector() => randomizer = new();

        public int SelectCard(List<Card> deck) => randomizer.Next(deck.Count);
    }
}
