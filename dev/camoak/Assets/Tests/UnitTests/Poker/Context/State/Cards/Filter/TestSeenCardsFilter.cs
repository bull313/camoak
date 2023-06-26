using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Cards;
using Camoak.Domain.Poker.Context.State.Cards.Filter;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Cards.Filter
{
    public class TestSeenCardsFilter
    {
        private SeenCardsFilter filter;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetHoleCards(new()
                    {
                        Card.KING_OF_DIAMONDS, Card.NINE_OF_DIAMONDS
                    })
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[1])
                    .SetHoleCards(new()
                    {
                        Card.JACK_OF_SPADES, Card.EIGHT_OF_SPADES
                    })
                    .Build())
                .Build();

            filter = new();
            filter.GameState = gameState;
        }

        [Test]
        public void TestPlayerHoleCardsTriggerFilterFlag()
        {
            Assert.IsFalse(filter.AllowThrough(Card.KING_OF_DIAMONDS));
            Assert.IsFalse(filter.AllowThrough(Card.EIGHT_OF_SPADES));
        }

        [Test]
        public void TestCardsThatAreNotInPlayerHoleCardsDoNotTriggerFilterFlag()
            => Assert.IsTrue(filter.AllowThrough(Card.JACK_OF_HEARTS));
    }
}
