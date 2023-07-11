using System.Collections.Generic;
using Camoak.Domain.Poker.Actor.Referee.Schema;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;
using NUnit.Framework;

namespace Camoak.Tests.UnitTests.Poker.Actor.Referee.Schema
{
    public class TestLogicSchema
    {
        private LogicSchema schema;
        private List<IGameStateCheck> checks;

        [SetUp]
        public void SetUp() => schema = new();

        [Test]
        public void TestTrueActionSequenceIsReturnedWhenCheckIsSatisfied()
        {
            checks = new() { new TestTrueGameCheck() };
            schema = new(
                new(new() { true }, new TestSequence1()),
                new(new() { false }, new TestSequence2())
            );

            Assert.AreEqual(
                "TestSequence1",
                schema.Evaluate(null, checks).GetType().Name
            );
        }

        [Test]
        public void TestFalseActionSequenceIsReturnedWhenCheckIsSatisfied()
        {
            checks = new() { new TestFalseGameCheck() };
            schema = new(
                new(new() { true }, new TestSequence1()),
                new(new() { false }, new TestSequence2())
            );

            Assert.AreEqual(
                "TestSequence2",
                schema.Evaluate(null, checks).GetType().Name
            );
        }

        [Test]
        public void TestTrueActionSequenceIsReturnedWhenWeDontCareAbout()
        {
            checks = new() { new TestTrueGameCheck() };
            schema = new(
                new(new() { null }, new TestSequence1()),
                new(new() { false }, new TestSequence2())
            );

            Assert.AreEqual(
                "TestSequence1",
                schema.Evaluate(null, checks).GetType().Name
            );
        }

        [Test]
        public void TestFalseActionSequenceIsReturnedWhenWeDontCare()
        {
            checks = new() { new TestFalseGameCheck() };
            schema = new(
                new(new() { true }, new TestSequence1()),
                new(new() { null }, new TestSequence2())
            );

            Assert.AreEqual(
                "TestSequence2",
                schema.Evaluate(null, checks).GetType().Name
            );
        }
    }

    internal class TestTrueGameCheck : IGameStateCheck
    {
        public bool IsSatisfied(PokerGameState gameState) => true;
    }

    internal class TestFalseGameCheck : IGameStateCheck
    {
        public bool IsSatisfied(PokerGameState gameState) => false;
    }

    internal class TestSequence1 : RefereeActionSequence
    {
        protected override List<RefereeAction> InitSequence() => null;
    }

    internal class TestSequence2 : RefereeActionSequence
    {
        protected override List<RefereeAction> InitSequence() => null;
    }
}
