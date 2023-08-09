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
    public class AT_PreflopBeginningButtonCall
    {
        [Test]
        public void PlayerMovesHalfABigBlindFromStackToAction() =>
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.PreflopBeginningState)
                        .Build())
                    .WithPlayerActor(new TestPreflopButtonBeginningCallActor())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                .When()
                    .TurnPlayerPlays()
                .Then()
                    .AssertStackEqual(99f, 0)
                    .AssertActionEqual(1f, 0)
                    .AssertAllElseUnchanged();

        [Test]
        public void RefereeMakesItTheBigBlindPlayersTurn() =>
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.PreflopBeginningState)
                        .SetPlayer(0, PokerPlayerBuilder.Create()
                            .Copy(PokerCommonGameStates.PreflopBeginningState.Players[0])
                            .SetStack(99f)
                            .SetAction(1f)
                            .Build())
                        .Build())
                    .WithPlayerActor(new TestPreflopButtonBeginningCallActor())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                .When()
                    .RefereePlays()
                .Then()
                    .AssertTurnPosition(0)
                    .AssertAllElseUnchanged();
    }

    internal class TestPreflopButtonBeginningCallActor : PokerPlayerActor
    {
        public TestPreflopButtonBeginningCallActor()
            : base(new BasicFilteredPokerGameState())
        { }

        public override Task<PlayerAction> SelectAction() =>
            Task.FromResult<PlayerAction>(new Call());
    }
}
