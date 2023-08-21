namespace Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck
{
    public class NumCardsOnBoardCheck : IGameStateCheck
    {
        private int ExpectedNumCards { get; set; }

        public NumCardsOnBoardCheck(int expectedNumCards) =>
            ExpectedNumCards = expectedNumCards;

        public bool IsSatisfied(PokerGameState gameState)
            => gameState.BoardCards.Count == ExpectedNumCards;
    }
}
