﻿using System.Collections.Generic;
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
    public class AT_TurnFacingCheckButtonCheck
    {
        [Test]
        public void PlayerCheckActionDoesNothing() =>
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.TurnBeginningState)
                        .SetTurnPosition(1)
                        .Build())
                    .WithPlayerActor(new CheckActor())
                    .WithPlayerActor(new DoNothingActor())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                .When()
                    .TurnPlayerPlays()
                .Then()
                    .AssertAllElseUnchanged();

        [Test]
        public void RefereeFlipsCardAndSetsTurnToBigBlind() =>
            PokerScenario.Create()
                .Given()
                    .GameWithState(PokerGameStateBuilder.Create()
                        .Copy(PokerCommonGameStates.TurnBeginningState)
                        .SetTurnPosition(1)
                        .Build())
                    .WithPlayerActor(new DoNothingActor())
                    .WithPlayerActor(new CheckActor())
                    .WithRefereeActor(new NoLimitHoldemReferee())
                    .WithCardSelector(new TestCardSelector())
                .When()
                    .RefereePlays()
                .Then()
                    .AssertBoardCards(
                        5,
                        Card.JACK_OF_CLUBS,
                        Card.SEVEN_OF_SPADES,
                        Card.JACK_OF_HEARTS,
                        Card.EIGHT_OF_HEARTS,
                        Card.ACE_OF_CLUBS
                    )
                    .AssertTurnPosition(0)
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

        private class TestCardSelector : ICardSelector
        {
            private int counter;
            private readonly Card[] dealCards;

            public TestCardSelector()
            {
                counter = 0;
                dealCards = new Card[]
                {
                    Card.ACE_OF_CLUBS
                };
            }

            public int SelectCard(List<Card> deck) =>
                deck.IndexOf(
                    dealCards[counter++ % dealCards.Length]
                );
        }
    }
}
