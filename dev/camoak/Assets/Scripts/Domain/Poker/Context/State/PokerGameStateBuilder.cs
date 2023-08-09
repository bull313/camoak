using System.Collections.Generic;
using Camoak.Domain.Poker.Context.State.Cards;

namespace Camoak.Domain.Poker.Context.State
{
    public class PokerGameStateBuilder
    {
        private PokerGameState GameState { get; set; }

        private PokerGameStateBuilder() => GameState = new();

        private PokerPlayer CopyPlayer(PokerPlayer player) =>
            PokerPlayerBuilder.Create()
                .Copy(player)
                .Build();

        private void AddPlayerCopy(PokerPlayer player) =>
            GameState.Players.Add(CopyPlayer(player));

        public static PokerGameStateBuilder Create() => new();

        public PokerGameStateBuilder Copy(PokerGameState gameState)
        {
            GameState = Create()
                .SetPlayers(gameState.Players)
                .SetBigBlindSize(gameState.BigBlindSize)
                .SetPlayerPositions(gameState.PlayerPositions)
                .SetPlayersInAction(gameState.PlayersInAction)
                .SetTurnPosition(gameState.TurnPosition)
                .SetCenterPot(gameState.CenterPot)
                .SetBoardCards(gameState.BoardCards)
                .Build();

            return this;
        }

        public PokerGameStateBuilder SetPlayers(List<PokerPlayer> players)
        {
            GameState.Players = new(players.Count);
            players.ForEach(AddPlayerCopy);
            return this;
        }

        public PokerGameStateBuilder SetPlayer(int slot, PokerPlayer player)
        {
            GameState.Players[slot] = CopyPlayer(player);
            return this;
        }

        public PokerGameStateBuilder SetBigBlindSize(int size)
        {
            GameState.BigBlindSize = size;
            return this;
        }

        public PokerGameStateBuilder SetPlayerPositions(List<int> positions)
        {
            GameState.PlayerPositions = new(positions);
            return this;
        }

        public PokerGameStateBuilder SetPlayersInAction(List<int> players)
        {
            GameState.PlayersInAction = new(players);
            return this;
        }

        public PokerGameStateBuilder SetTurnPosition(int player)
        {
            GameState.TurnPosition = player;
            return this;
        }

        public PokerGameStateBuilder SetCenterPot(float pot)
        {
            GameState.CenterPot = pot;
            return this;
        }

        public PokerGameStateBuilder SetBoardCards(List<Card> boardCards)
        {
            GameState.BoardCards = new(boardCards);
            return this;
        }

        public PokerGameState Build() => GameState;
    }
}
