using System.Threading.Tasks;
using Camoak.Component.Poker.Table;
using Camoak.Domain.Poker;
using Camoak.Domain.Poker.Actor.Player;
using Camoak.Domain.Poker.Actor.Player.SelectTask;
using Camoak.Domain.Poker.Actor.Referee;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Domain.Poker.Context.State.Filter;
using UnityEngine;

namespace Camoak.Component.Poker
{
    public class PokerGameComponent : GameComponent
    {
        public const string POKER_TABLE_OBJECT = "Prefabs/HeadsUpNLHPokerTable";

        private bool GameIdle { get; set; }
        private FilteredPokerGameState HumanGameState;
        private ActionSelectionTask SelectionTask { get; set; }
        public PokerTable Table { get; private set; }
        public PokerGame Game { get; private set; }

        public override void Init()
        {
            GameIdle = true;
            HumanGameState = new BasicFilteredPokerGameState();
            SelectionTask = new();

            Table = Instantiate(
                Resources.Load<GameObject>(POKER_TABLE_OBJECT)
            ).GetComponent<PokerTable>();

            Table.GameState = HumanGameState;
            Table.SelectionTask = SelectionTask;

            Game = new()
            {
                GameContext = new()
                {
                    GameState = PokerGameStateBuilder.Create()
                        .SetPlayers(new()
                        {
                            PokerPlayerBuilder.Create()
                                .SetStack(100f)
                                .Build(),

                            PokerPlayerBuilder.Create()
                                .SetStack(100f)
                                .Build()
                        })
                        .SetBigBlindSize(10)
                        .SetPlayerPositions(new() { 0, 1 })
                        .SetPlayersInAction(new() { 1 })
                        .SetCenterPot(0f)
                        .Build(),

                    ActorContext = new()
                    {
                        Players = new()
                        {
                            new HumanPlayerActor(HumanGameState, SelectionTask),
                            new PokerThinkingBot(new PokerFoldBot())
                        },

                        Referee = new NoLimitHoldemReferee()
                    }
                }
            };

            Game.PlayReferee();
        }

        private void UpdatePlayers()
        {
            Game.GetTurnPlayer();
            Game.UpdatePlayerGameStates();
        }

        private void UpdateGameState()
        {
            Game.ExecutePlayerAction();
            Game.PlayReferee();
        }

        private void UpdateTable()
        {
            Table.SelectionTask.Reset();
            Table.UpdateView();
        }

        private async Task PlayATurn()
        {
            GameIdle = false;

            UpdatePlayers();
            UpdateTable();

            await Game.GetPlayerAction();

            UpdateGameState();

            GameIdle = true;
        }

        public override void Play()
        {
            if (GameIdle) _ = PlayATurn();
        }
    }

    internal class PokerFoldBot : PokerPlayerActor
    {
        public PokerFoldBot() : base(new BasicFilteredPokerGameState())
        { }

        public override Task<PlayerAction> SelectAction() =>
            Task.FromResult<PlayerAction>(new Fold());
    }
}
