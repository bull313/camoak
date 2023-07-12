using System.Collections.Generic;
using System.Threading.Tasks;
using Camoak.Domain.Poker;
using Camoak.Domain.Poker.Actor.Player;
using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Actor.Referee.Schema;
using Camoak.Domain.Poker.Context;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;
using Camoak.Domain.Poker.Context.State.Filter;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker
{
    public class TestPokerGame
    {
        private PokerGame gameManager;
        private PokerGameContext gameContext;
        private PlayerAction expectedPlayerAction;
        private RefereeActionSequence expectedRefSequence;
        private PokerPlayerActor expectedTurnPlayer;
        private PokerRefereeActor expectedReferee;

        [SetUp]
        public void SetUp()
        {
            expectedPlayerAction = new Fold();
            expectedRefSequence =
                new EndHandActionSequence();
            expectedTurnPlayer = new TestPokerPlayerActor(expectedPlayerAction);
            expectedReferee = new TestReferee(expectedRefSequence);

            gameContext = new()
            {
                GameState = PokerGameStateBuilder.Create()
                    .SetPlayersInAction(new() { 1, 0 })
                    .SetTurnPosition(1)
                    .Build(),

                ActorContext = new()
                {
                    Players = new()
                    {
                        expectedTurnPlayer,
                        new TestPokerPlayerActor(null)
                    },

                    Referee = expectedReferee
                }
            };

            gameManager = new();
            gameManager.GameContext = gameContext;
        }

        [Test]
        public void TestManagerGetsPlayerAtPositionSpecifiedByTurnPlayer()
        {
            gameManager.GetTurnPlayer();

            Assert.AreEqual(expectedTurnPlayer, gameManager.TurnPlayer);
        }

        [Test]
        public void TestManagerSetsPlayerIndexOfPlayerFilteredGameState()
        {
            gameManager.TurnPlayer = expectedTurnPlayer;
            gameManager.UpdatePlayerGameStates();

            Assert.AreEqual(0, expectedTurnPlayer.GameState.Player);
        }

        [Test]
        public void TestManagerSendsCopyOfGameStateToAllPlayers()
        {
            gameManager.TurnPlayer = expectedTurnPlayer;
            gameManager.UpdatePlayerGameStates();

            Assert.AreEqual(
                gameContext.GameState.GetHashCode(),
                expectedTurnPlayer.GameState.GetHashCode()
            );

            Assert.AreEqual(
                gameContext.GameState.GetHashCode(),
                gameContext.ActorContext.Players[1].GameState.GetHashCode()
            );
        }

        [Test]
        public void TestManagerGetsSelectedActionFromTurnPlayer()
        {
            gameManager.TurnPlayer = expectedTurnPlayer;
            gameManager.GetPlayerAction().Wait();

            Assert.AreEqual(
                expectedPlayerAction,
                gameManager.SelectedPlayerAction
            );
        }

        [Test]
        public void TestManagerGetsGameReferee()
        {
            gameManager.GetReferee();
            Assert.AreEqual(expectedReferee, gameManager.Referee);
        }

        [Test]
        public void TestManagerSendsCopyOfGameStateToReferee()
        {
            gameManager.Referee = expectedReferee;

            gameManager.UpdateRefereeGameState();

            Assert.AreEqual(
                gameContext.GameState.GetHashCode(),
                expectedReferee.GameState.GetHashCode()
            );
        }

        [Test]
        public void TestManagerGetsSelectedActionSequenceFromGameReferee()
        {
            gameManager.Referee = expectedReferee;
            gameManager.GetRefereeActionSequence();
            Assert.AreEqual(expectedRefSequence, gameManager.SelectedRefereeAction);
        }
    }

    internal class TestPokerPlayerActor : PokerPlayerActor
    {
        private readonly PlayerAction action;

        public TestPokerPlayerActor(PlayerAction a) : base(
            new BasicFilteredPokerGameState()
        ) => action = a;

        public override Task<PlayerAction> SelectAction() =>
            Task.FromResult(action);
    }

    internal class TestReferee : PokerRefereeActor
    {
        private readonly RefereeActionSequence sequence;

        protected override List<IGameStateCheck> GameChecks => new();

        protected override LogicSchema GameLogicSchema
            => new(
                new KeyValuePair<List<bool?>, RefereeActionSequence>(
                    new() { null }, sequence
                )
            );

        public TestReferee(RefereeActionSequence seq) => sequence = seq;
    }
}
