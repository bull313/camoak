namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class PayoutPot : RefereeAction
    {
        public const float EMPTY_POT = 0f;

        private float PlayerEarn { get; set; }

        private void PayWinner(int winnerIdx) =>
            GameState.Players[winnerIdx].Stack += PlayerEarn;

        public override void Execute()
        {
            PlayerEarn = GameState.CenterPot / GameState.PlayersInAction.Count;
            GameState.PlayersInAction.ForEach(PayWinner);
            GameState.CenterPot = EMPTY_POT;
        }
    }
}
