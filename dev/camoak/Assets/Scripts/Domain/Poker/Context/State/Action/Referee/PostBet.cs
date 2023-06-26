using Camoak.Domain.Poker.Context.State.Action.Referee.TurnPlayerStrategy;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class PostBet : RefereeAction
    {
        private ITurnPlayerStrategy TargetPosition { get; set; }
        private float BetSize { get; set; }

        public PostBet(ITurnPlayerStrategy targetPosition, float betSize)
        {
            TargetPosition = targetPosition;
            BetSize = betSize;
        }

        private PokerPlayer GetTargetPlayer() =>
            GameState.Players[
                GameState.PlayerPositions[
                    TargetPosition.GetTurnPlayer(GameState)
                ]
            ];

        public override void Execute()
        {
            GetTargetPlayer().Stack -= BetSize;
            GetTargetPlayer().Action += BetSize;
        }
    }
}
