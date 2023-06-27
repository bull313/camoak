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
    public class AT_PreflopBeginningButtonFold
    {
        [Test]
        public void PlayerRemovesSelfFromTheAction()
        {
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.PreflopBeginningState)
                        .Build())
                    .WithPlayerActor(
                        new TestPreflopButtonBeginningFoldPlayerActor()
                    )
                    .WithRefereeActor(new NoLimitHoldemReferee())
                .When()
                    .TurnPlayerPlays()
                .Then()
                    .AssertPlayersInAction(new() { 1 })
                    .AssertAllElseUnchanged();
        }

        [Test]
        public void RefereeResetsToANewHand()
        {
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.PreflopBeginningState)
                        .SetPlayersInAction(new() { 1 })
                        .Build())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                    .WithCardSelector(new ForceHoleCardsSelector())
                .When()
                    .RefereePlays()
                .Then()
                    .AssertStackEqual(98.5f, 0)
                    .AssertActionEqual(1f, 0)
                    .AssertHoleCard(Card.TEN_OF_SPADES, 0, 0)
                    .AssertHoleCard(Card.FOUR_OF_DIAMONDS, 0, 1)

                    .AssertStackEqual(100f, 1)
                    .AssertActionEqual(0.5f, 1)
                    .AssertHoleCard(Card.FOUR_OF_CLUBS, 1, 0)
                    .AssertHoleCard(Card.FIVE_OF_SPADES, 1, 1)

                    .AssertPlayerPositions(new() { 0, 1 })
                    .AssertPlayersInAction(new() { 0, 1 })
                    .AssertTurnPosition(1)

                    .AssertAllElseUnchanged();
        }
    }

    internal class ForceHoleCardsSelector : ICardSelector
    {
        private int counter;
        private readonly Card[] dealCards;

        public ForceHoleCardsSelector()
        {
            counter = 0;
            dealCards = new Card[]
            {
                Card.TEN_OF_SPADES,
                Card.FOUR_OF_DIAMONDS,
                Card.FOUR_OF_CLUBS,
                Card.FIVE_OF_SPADES
            };
        }

        public int SelectCard(List<Card> deck) =>
            deck.IndexOf(
                dealCards[counter++ % dealCards.Length]
            );
    }

    internal class TestPreflopButtonBeginningFoldPlayerActor : PokerPlayerActor
    {
        public TestPreflopButtonBeginningFoldPlayerActor()
            : base(new BasicFilteredPokerGameState())
        { }

        public override Task<PlayerAction> SelectAction() =>
            Task.FromResult<PlayerAction>(new Fold());
    }
}
