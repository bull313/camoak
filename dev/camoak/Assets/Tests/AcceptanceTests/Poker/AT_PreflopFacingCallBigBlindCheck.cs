using System.Collections.Generic;
using System.Threading.Tasks;
using Camoak.Domain.Poker.Actor.Player;
using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Domain.Poker.Context.State.Cards;
using Camoak.Domain.Poker.Context.State.Cards.Selection;
using Camoak.Domain.Poker.Context.State.Filter;
using Camoak.Tests.AcceptanceTests.Poker.Dsl;
using Camoak.Tests.Common.Poker;
using NUnit.Framework;

namespace Camoak.Tests.AcceptanceTests.Poker
{
    public class AT_PreflopFacingCallBigBlindCheck
    {
        [Test]
        public void PlayerCheckActionDoesNothing() =>
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.PreflopBeginningState)
                        .SetPlayer(0, PokerPlayerBuilder.Create()
                            .SetStack(99f)
                            .SetAction(1f)
                            .Build())
                        .SetTurnPosition(0)
                        .Build())
                    .WithPlayerActor(new TestDoNothingActor())
                    .WithPlayerActor(new TestPreflopFacingCallBigBlindCheckActor())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                .When()
                    .TurnPlayerPlays()
                .Then()
                    .AssertAllElseUnchanged();

        [Test]
        public void RefereeMovesCentersActionFlipsCardsAndSetsTurnToBigBlind()
        {
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.PreflopBeginningState)
                        .SetPlayer(0, PokerPlayerBuilder.Create()
                            .SetStack(99f)
                            .SetAction(1f)
                            .Build())
                        .SetTurnPosition(0)
                        .Build())
                    .WithPlayerActor(new TestDoNothingActor())
                    .WithPlayerActor(new TestPreflopFacingCallBigBlindCheckActor())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                    .WithCardSelector(new PreflopFacingCallBigBlindCheckSelector())
                .When()
                    .RefereePlays()
                .Then()
                    .AssertActionEqual(0f, 0)
                    .AssertActionEqual(0f, 1)
                    .AssertCenterPot(2f)
                    .AssertTurnPosition(0)
                    .AssertBoardCards(
                        3,
                        Card.TWO_OF_SPADES,
                        Card.TWO_OF_HEARTS,
                        Card.KING_OF_HEARTS
                    )
                    .AssertAllElseUnchanged();
        }

        internal class TestPreflopFacingCallBigBlindCheckActor : PokerPlayerActor
        {
            public TestPreflopFacingCallBigBlindCheckActor()
                : base(new BasicFilteredPokerGameState())
            { }

            public override Task<PlayerAction> SelectAction() =>
                Task.FromResult<PlayerAction>(new Check());
        }

        internal class TestDoNothingActor : PokerPlayerActor
        {
            public TestDoNothingActor()
                : base(new BasicFilteredPokerGameState())
            { }

            public override Task<PlayerAction> SelectAction() => null;
        }

        internal class PreflopFacingCallBigBlindCheckSelector : ICardSelector
        {
            private int counter;
            private readonly Card[] dealCards;

            public PreflopFacingCallBigBlindCheckSelector()
            {
                counter = 0;
                dealCards = new Card[]
                {
                    Card.TWO_OF_SPADES,
                    Card.TWO_OF_HEARTS,
                    Card.KING_OF_HEARTS,
                    Card.TEN_OF_DIAMONDS
                };
            }

            public int SelectCard(List<Card> deck) =>
                deck.IndexOf(
                    dealCards[counter++ % dealCards.Length]
                );
        }
    }
}
