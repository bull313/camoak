using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Filter;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Filter
{
    public class TestBasicFilteredPokerGameState
    {
        private BasicFilteredPokerGameState filteredGameState;
        private PokerGameState gameState;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .Build();

            filteredGameState = new();
            filteredGameState.Player = 0;
            filteredGameState.Update(PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build()
            );
        }

        [Test]
        public void TestGameStateOpponentHoleCardsAreCleared() =>
            Assert.IsEmpty(filteredGameState.Players[1].HoleCards);

        [Test]
        public void TestAllGameExceptHoleCardsStateIsPerfectlyCopied()
        {
            PokerGameState filteredStateIgnoreCards =
                PokerGameStateBuilder.Create()
                    .Copy(filteredGameState)
                    .SetPlayer(0, PokerPlayerBuilder.Create()
                        .Copy(filteredGameState.Players[0])
                        .SetHoleCards(gameState.Players[0].HoleCards)
                        .Build())
                    .SetPlayer(1, PokerPlayerBuilder.Create()
                        .Copy(filteredGameState.Players[1])
                        .SetHoleCards(gameState.Players[1].HoleCards)
                        .Build())
                    .Build();

            Assert.AreEqual(
                gameState.GetHashCode(),
                filteredStateIgnoreCards.GetHashCode()
            );
        }
    }
}
