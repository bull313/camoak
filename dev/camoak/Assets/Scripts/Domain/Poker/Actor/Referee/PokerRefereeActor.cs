using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker.Actor.Referee
{
    public abstract class PokerRefereeActor
    {
        protected List<IGameStateCheck> GameChecks;
        public PokerGameState GameState { get; set; }

        public PokerRefereeActor() => GameChecks = InitGameChecks();

        private bool CheckGameState(IGameStateCheck check) =>
            check.IsSatisfied(GameState);

        private List<bool> EvaluateGameChecks() =>
            GameChecks.Select(CheckGameState).ToList();

        private bool MatchCheckValues(bool? schemaExpected, bool check) =>
            schemaExpected == null || check == schemaExpected;

        private bool IsTrue(bool value) => value;

        private bool AreChecksMet(
            KeyValuePair<List<bool?>, RefereeActionSequence> schema
        ) => schema.Key.Zip(EvaluateGameChecks(), MatchCheckValues)
                .ToList()
                .All(IsTrue);

        protected abstract List<IGameStateCheck> InitGameChecks();

        protected abstract
            List<KeyValuePair<List<bool?>, RefereeActionSequence>>
                 GetLogicMap();

        public RefereeActionSequence SelectActionSequence() =>
            GetLogicMap().Where(AreChecksMet).ToList()[0].Value;
    }
}
