using System.Linq;
using System.Threading.Tasks;
using Camoak.Domain.Poker.Actor.Player;
using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Context;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker
{
    public class PokerGame
    {
        public PokerGameContext GameContext { get; set; }
        public PokerPlayerActor TurnPlayer { get; set; }
        public PokerRefereeActor Referee { get; set; }
        public PlayerAction SelectedPlayerAction { get; set; }
        public RefereeActionSequence SelectedRefereeAction { get; set; }

        public PokerGame() => GameContext = new();

        private int GetTurnIndex() =>
            GameContext.GameState.PlayersInAction[
                GameContext.GameState.TurnPosition
            ];

        private void CopyPlayerGameState(PokerPlayerActor player, int index)
        {
            player.GameState.Player = index;

            player.GameState.Update(
                PokerGameStateBuilder
                    .Create()
                    .Copy(GameContext.GameState)
                    .Build()
            );
        }

        private void CopyGameStateAtIndex(int p) =>
            CopyPlayerGameState(GameContext.ActorContext.Players[p], p);

        public PokerPlayerActor GetTurnPlayer() =>
            TurnPlayer = GameContext.ActorContext.Players[GetTurnIndex()];

        public async Task GetPlayerAction() =>
            SelectedPlayerAction = await TurnPlayer.SelectAction();

        public void GetReferee() => Referee = GameContext.ActorContext.Referee;

        public void GetRefereeActionSequence()
            => SelectedRefereeAction = Referee.SelectActionSequence();

        public void UpdatePlayerGameStates() =>
            Enumerable.Range(0, GameContext.ActorContext.Players.Count)
                .ToList()
                .ForEach(CopyGameStateAtIndex);

        public void UpdateRefereeGameState() => 
            Referee.GameState = PokerGameStateBuilder
                .Create()
                .Copy(GameContext.GameState)
                .Build();

        public void ExecutePlayerAction()
        {
            SelectedPlayerAction.GameState = GameContext.GameState;
            SelectedPlayerAction.Execute();
        }

        public void ExecuteRefereeActionSequence()
        {
            SelectedRefereeAction.SetGameState(GameContext.GameState);
            SelectedRefereeAction.ExecuteAll();
        }

        public async Task PlayTurnPlayer()
        {
            GetTurnPlayer();
            UpdatePlayerGameStates();
            await GetPlayerAction();
            ExecutePlayerAction();
        }

        public void PlayReferee()
        {
            GetReferee();
            UpdateRefereeGameState();
            GetRefereeActionSequence();
            ExecuteRefereeActionSequence();
        }
    }
}
