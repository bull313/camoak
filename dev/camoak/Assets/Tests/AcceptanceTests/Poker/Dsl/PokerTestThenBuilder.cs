using System;
using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Cards;
using NUnit.Framework;

namespace Camoak.Tests.AcceptanceTests.Poker.Dsl
{
    public class PokerTestThenBuilder
    {
        private PokerScenario Scenario { get; set; }
        private PokerGameState Before { get; set; }
        private PokerGameState After { get; set; }

        private PokerTestThenBuilder(PokerScenario scenario)
        {
            Scenario = scenario;
            After = Scenario.Game.GameContext.GameState;
            Before = scenario.GameStateBefore;
        }

        public static PokerTestThenBuilder Create(PokerScenario s) => new(s);

        public PokerTestThenBuilder AssertStackEqual(float expected, int player)
        {
            Assert.AreEqual(expected, After.Players[player].Stack);

            After = PokerGameStateBuilder.Create()
                .Copy(After)
                .SetPlayer(player, PokerPlayerBuilder.Create()
                    .Copy(After.Players[player])
                    .SetStack(Before.Players[player].Stack)
                    .Build())
                .Build();

            return this;
        }

        public PokerTestThenBuilder AssertActionEqual(float expected, int p)
        {
            Assert.AreEqual(expected, After.Players[p].Action);

            After = PokerGameStateBuilder.Create()
                .Copy(After)
                .SetPlayer(p, PokerPlayerBuilder.Create()
                    .Copy(After.Players[p])
                    .SetAction(Before.Players[p].Action)
                    .Build())
                .Build();

            return this;
        }

        public PokerTestThenBuilder AssertHoleCard(Card expected, int p, int i)
        {
            Assert.AreEqual(expected, After.Players[p].HoleCards[i]);

            After = PokerGameStateBuilder.Create()
                .Copy(After)
                .SetPlayer(p, PokerPlayerBuilder.Create()
                    .Copy(After.Players[p])
                    .SetHoleCard(Before.Players[p].HoleCards[i], i)
                    .Build())
                .Build();

            return this;
        }

        public PokerTestThenBuilder AssertPlayerPositions(List<int> expected)
        {
            Assert.IsTrue(expected.SequenceEqual(After.PlayerPositions));

            After = PokerGameStateBuilder.Create()
                .Copy(After)
                .SetPlayerPositions(Before.PlayerPositions)
                .Build();

            return this;
        }

        public PokerTestThenBuilder AssertPlayersInAction(List<int> expected)
        {
            Assert.IsTrue(expected.SequenceEqual(After.PlayersInAction));

            After = PokerGameStateBuilder.Create()
                .Copy(After)
                .SetPlayersInAction(Before.PlayersInAction)
                .Build();

            return this;
        }

        public PokerTestThenBuilder AssertTurnPlayer(int expected)
        {
            Assert.AreEqual(expected, After.TurnPlayer);

            After = PokerGameStateBuilder.Create()
                .Copy(After)
                .SetTurnPlayer(Before.TurnPlayer)
                .Build();

            return this;
        }

        public PokerTestThenBuilder AssertAllElseUnchanged()
        {
            Assert.AreEqual(Before.GetHashCode(), After.GetHashCode());
            return this;
        }
    }
}
