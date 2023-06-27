namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class ClearHoleCards : RefereeAction
    {
        private void ClearPlayerHoleCards(PokerPlayer player) =>
            player.HoleCards.Clear();

        public override void Execute() =>
            GameState.Players.ForEach(ClearPlayerHoleCards);
    }
}
