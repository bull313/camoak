using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Actor
{
    public class TestNoLimitHoldemReferee
    {
        private NoLimitHoldemReferee referee;
        private PokerGameState foldedGameState;
        private RefereeActionSequence actualSequence;
        private EndHandActionSequence expectedSequence;

        [SetUp]
        public void SetUp()
        {
            expectedSequence = new();

            foldedGameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayersInAction(new() { 1 })
                .Build();

            referee = new();
            referee.GameState = foldedGameState;
            actualSequence = referee.SelectActionSequence();
        }

        [Test]
        public void TestOnlyOnePlayerInTheHandSelectsHandEndingSequence() =>
            Assert.AreEqual(
                expectedSequence.GetHashCode(), actualSequence.GetHashCode()
            );
    }
}
