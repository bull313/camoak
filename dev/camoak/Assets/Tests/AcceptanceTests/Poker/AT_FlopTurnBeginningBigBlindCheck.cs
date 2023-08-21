using System.Threading.Tasks;
using Camoak.Domain.Poker.Actor.Player;
using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Domain.Poker.Context.State.Filter;
using Camoak.Tests.AcceptanceTests.Poker.Dsl;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.AcceptanceTests.Poker
{
    public class AT_FlopTurnBeginningBigBlindCheck
    {
        [Test]
        public void PlayerCheckActionDoesNothing() =>
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.FlopBeginningState)
                        .Build())
                    .WithPlayerActor(new DoNothingActor())
                    .WithPlayerActor(new CheckActor())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                .When()
                    .TurnPlayerPlays()
                .Then()
                    .AssertAllElseUnchanged();

        [Test]
        public void RefereeMakesItTheButtonPlayersTurn() =>
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.FlopBeginningState)
                        .Build())
                    .WithPlayerActor(new DoNothingActor())
                    .WithPlayerActor(new CheckActor())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                .When()
                    .RefereePlays()
                .Then()
                    .AssertTurnPosition(1)
                    .AssertAllElseUnchanged();

        private class CheckActor : PokerPlayerActor
        {
            public CheckActor()
                : base(new BasicFilteredPokerGameState())
            { }

            public override Task<PlayerAction> SelectAction() =>
                Task.FromResult<PlayerAction>(new Check());
        }

        private class DoNothingActor : PokerPlayerActor
        {
            public DoNothingActor()
                : base(new BasicFilteredPokerGameState())
            { }

            public override Task<PlayerAction> SelectAction() => null;
        }
    }
}
