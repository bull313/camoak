using System.Threading;
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
                            new PokerThinkingBot(new BasicPokerBot())
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

        /* Temporary method to restart game */
        private void ResetGame()
        {
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
                            new PokerThinkingBot(new BasicPokerBot())
                        },

                        Referee = new NoLimitHoldemReferee()
                    }
                }
            };

            Game.PlayReferee();
        }

        /* Temporary method to determine reset condition */
        private bool IsResetConditionMet() =>
            Game.GameContext.GameState.BoardCards.Count > 3;

        private async Task PlayATurn()
        {
            GameIdle = false;

            UpdatePlayers();
            UpdateTable();

            /* Temporarily resets game until more features come out */
            if (IsResetConditionMet())
            {
                await Task.Run(() => Thread.Sleep(500));
                ResetGame();
            }
            else
            {
                await Game.GetPlayerAction();

                UpdateGameState();
            }

            GameIdle = true;
        }

        public override void Play()
        {
            if (GameIdle) _ = PlayATurn();
        }

        private class BasicPokerBot : PokerPlayerActor
        {
            public BasicPokerBot() : base(new BasicFilteredPokerGameState())
            { }

            public override Task<PlayerAction> SelectAction() =>
                GameState.TurnPosition == 0 ?
                Task.FromResult<PlayerAction>(new Check())
                :
                Task.FromResult<PlayerAction>(new Call());
        }
    }
}
