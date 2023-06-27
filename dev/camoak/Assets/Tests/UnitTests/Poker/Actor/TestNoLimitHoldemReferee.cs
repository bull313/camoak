﻿using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Actor
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
        public void TestMultiplePlayersInTheHandSelectsMoveNextTurnSequence()
        {
            gameState = PokerGameStateBuilder.Create()
                .Copy(PokerCommonGameStates.PreflopBeginningState)
                .SetPlayersInAction(new() { 1, 0 })
                .Build();

            referee = new();
            referee.GameState = gameState;
            actualSequence = referee.SelectActionSequence();

            Assert.AreEqual(
                new MoveToNextPlayerSequence().GetHashCode(),
                actualSequence.GetHashCode()
            );
        }
    }
}
