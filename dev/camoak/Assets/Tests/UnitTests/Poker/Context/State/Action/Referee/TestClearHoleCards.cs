using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Domain.Poker.Context.State.Cards;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Context.State.Action.Referee
{
    public class TestClearHoleCards
    {
        private ClearHoleCards clearAction;
        private PokerGameState gameState, gameStateCopy;

        [SetUp]
        public void SetUp()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetHoleCards(new() { Card.KING_OF_CLUBS, Card.SEVEN_OF_HEARTS })
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[1])
                    .SetHoleCards(new() { Card.QUEEN_OF_CLUBS, Card.TWO_OF_CLUBS })
                    .Build())
                .SetPlayerPositions(new() { 1, 0 })
                .Build();

            gameStateCopy = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .Build();

            clearAction = new();
            clearAction.GameState = gameState;
            clearAction.Execute();
        }

        [Test]
        public void TestAllPlayerHoleCardsAreGone()
        {
            Assert.AreEqual(0, gameState.Players[0].HoleCards.Count);
            Assert.AreEqual(0, gameState.Players[1].HoleCards.Count);
        }

        [Test]
        public void TestPostBetActionOnlyAffectsTargetPlayerStackAndAction()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(gameState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(gameState.Players[0])
                    .SetHoleCards(gameStateCopy.Players[0].HoleCards)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(gameState.Players[1])
                    .SetHoleCards(gameStateCopy.Players[1].HoleCards)
                    .Build())
                .Build();

            Assert.AreEqual(
                gameStateCopy.GetHashCode(),
                gameState.GetHashCode()
            );
        }
    }
}
