using System.Linq;

namespace Camoak.Domain.Poker.Context.State.Action.Referee
{
    public class RotatePlayerPositions : RefereeAction
    {
        private void RotatePosition(int position)
        {
            GameState.PlayerPositions[position] += 1;
            GameState.PlayerPositions[position] %=
                GameState.PlayerPositions.Count;
        }

        public override void Execute() =>
            Enumerable.Range(0, GameState.PlayerPositions.Count)
                .ToList()
                .ForEach(RotatePosition);
    }
}
