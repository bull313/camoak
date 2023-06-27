namespace Camoak.Tests.AcceptanceTests.Poker.Dsl
{
    public class PokerTestWhenBuilder
    {
        private PokerScenario Scenario { get; set; }

        private PokerTestWhenBuilder(PokerScenario s) => Scenario = s;

        public static PokerTestWhenBuilder Create(PokerScenario s) => new(s);

        public PokerTestWhenBuilder TurnPlayerPlays()
        {
            Scenario.Game.PlayTurnPlayer().Wait();
            return this;
        }

        public PokerTestWhenBuilder RefereePlays()
        {
            Scenario.Game.PlayReferee();
            return this;
        }

        public PokerTestThenBuilder Then() => PokerTestThenBuilder.Create(Scenario);
    }
}
