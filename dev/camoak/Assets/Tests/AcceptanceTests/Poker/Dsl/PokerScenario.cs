using Camoak.Domain.Poker;
using Camoak.Domain.Poker.Context.State;

namespace Camoak.Tests.AcceptanceTests.Poker.Dsl
{
    public class PokerScenario
    {
        public PokerGame Game { get; set; }
        public PokerGameState GameStateBefore { get; set; }

        private PokerScenario() => Game = new();

        public static PokerScenario Create() => new();

        public void SaveGameState() =>
            GameStateBefore = PokerGameStateBuilder.Create()
                .Copy(Game.GameContext.GameState)
                .Build();

        public PokerTestGivenBuilder Given() => 
            PokerTestGivenBuilder.Create(this);
    }
}
