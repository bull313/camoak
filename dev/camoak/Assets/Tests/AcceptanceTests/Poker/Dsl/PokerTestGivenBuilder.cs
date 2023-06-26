using Camoak.Domain.Poker.Actor.Player;
using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;
using Camoak.Domain.Poker.Context.State.Cards.Selection;

namespace Camoak.Tests.AcceptanceTests.Poker.Dsl
{
    public class PokerTestGivenBuilder
    {
        private PokerScenario Scenario { get; set; }

        private PokerTestGivenBuilder(PokerScenario s) => Scenario = s;

        public static PokerTestGivenBuilder Create(PokerScenario s) => new(s);

        public PokerTestGivenBuilder GameWithState(PokerGameState gameState)
        {
            Scenario.Game.GameContext.GameState = gameState;
            return this;
        }

        public PokerTestGivenBuilder WithPlayerActor(PokerPlayerActor actor)
        {
            Scenario.Game.GameContext.ActorContext.Players.Add(actor);
            return this;
        }

        public PokerTestGivenBuilder WithRefereeActor(PokerRefereeActor actor)
        {
            Scenario.Game.GameContext.ActorContext.Referee = actor;
            return this;
        }

        public PokerTestGivenBuilder WithCardSelector(ICardSelector selector)
        {
            RefereeActionSequence.Dealer.CardSelector = selector;
            return this;
        }

        public PokerTestWhenBuilder When()
        {
            Scenario.SaveGameState();
            return PokerTestWhenBuilder.Create(Scenario);
        }
    }
}
