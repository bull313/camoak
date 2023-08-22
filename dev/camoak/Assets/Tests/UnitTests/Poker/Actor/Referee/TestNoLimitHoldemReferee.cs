using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Actor.Referee
{
    public class TestNoLimitHoldemReferee
    {
        private NoLimitHoldemReferee referee;
        private PokerGameState gameState;
        private RefereeActionSequence actualSequence;

        [Test]
        public void TestOnlyOnePlayerInTheHandSelectsHandEndingSequence()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayersInAction(new() { 1 })
                .Build();

            referee = new();
            referee.GameState = gameState;
            actualSequence = referee.SelectActionSequence();

            Assert.AreEqual(
                new EndHandActionSequence().GetHashCode(),
                actualSequence.GetHashCode()
            );
        }

        [Test]
        public void TestPreflopAllActionEqualAndNotBBTurnMoveToNextPlayer()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                    .SetAction(1f)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .Copy(PokerCommonGameStates.PreflopBeginningState.Players[1])
                    .SetAction(1f)
                    .Build())
                .SetTurnPosition(1)
                .SetBoardCards(new())
                .Build();

            referee = new();
            referee.GameState = gameState;
            actualSequence = referee.SelectActionSequence();

            Assert.AreEqual(
                new MoveToNextPlayerSequence().GetHashCode(),
                actualSequence.GetHashCode()
            );
        }

        [Test]
        public void TestPreflopAllActionEqualAndBBTurnMoveToFlop()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayer(0, PokerPlayerBuilder.Create()
                    .SetAction(1f)
                    .Build())
                .SetPlayer(1, PokerPlayerBuilder.Create()
                    .SetAction(1f)
                    .Build())
                .SetTurnPosition(0)
                .SetBoardCards(new())
                .Build();

            referee = new();
            referee.GameState = gameState;
            actualSequence = referee.SelectActionSequence();

            Assert.AreEqual(
                new MoveToFlopSequence().GetHashCode(),
                actualSequence.GetHashCode()
            );
        }

        [Test]
        public void TestFlopAllActionEqualAndBBTurnMoveToNextPlayer()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.FlopBeginningState)
                .Build();

            referee = new();
            referee.GameState = gameState;
            actualSequence = referee.SelectActionSequence();

            Assert.AreEqual(
                new MoveToNextPlayerSequence().GetHashCode(),
                actualSequence.GetHashCode()
            );
        }

        [Test]
        public void TestFlopAllActionEqualAndButtonTurnMoveToNextPlayer()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.FlopBeginningState)
                .SetTurnPosition(1)
                .Build();

            referee = new();
            referee.GameState = gameState;
            actualSequence = referee.SelectActionSequence();

            Assert.AreEqual(
                new MoveToNextStreetSequence().GetHashCode(),
                actualSequence.GetHashCode()
            );
        }
    }
}
