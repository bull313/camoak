﻿using System.Collections.Generic;
using Camoak.Domain.Poker.Actor.Referee.Schema;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker.Actor.Referee
{
    public class NoLimitHoldemReferee : PokerRefereeActor
    {
        public const int EMPTY_BOARD_SIZE = 0;

        protected override List<IGameStateCheck> GameChecks => new()
        {
            new SinglePlayerInActionCheck(),
            new AllPlayerActionsAreEqualCheck(),
            new IsBigBlindPlayerTurnCheck(),
            new NumCardsOnBoardCheck(EMPTY_BOARD_SIZE)
        };

        protected override LogicSchema GameLogicSchema
            => new(
                new(new() { true, null, null, null }, new EndHandActionSequence()),
                new(new() { false, true, false, true }, new MoveToNextPlayerSequence()),
                new(new() { false, true, true, true }, new MoveToFlopSequence()),
                new(new() { false, true, true, false }, new MoveToNextPlayerSequence())
            );
    }
}
