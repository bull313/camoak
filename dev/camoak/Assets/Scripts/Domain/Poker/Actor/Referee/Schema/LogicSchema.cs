using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Action.Referee.GameStateCheck;
using Camoak.Domain.Poker.Context.State.Action.Referee.Sequence;

namespace Camoak.Domain.Poker.Actor.Referee.Schema
{
    using Rule = KeyValuePair<List<bool?>, RefereeActionSequence>;

    public class LogicSchema
    {
        private List<bool> GameChecks { get; set; }
        private PokerGameState GameState { get; set; }
        private List<Rule> Ruleset { get; set; }

        public LogicSchema(params Rule[] rules) => Ruleset = new(rules);

        private bool RunGameCheck(IGameStateCheck check) =>
            check.IsSatisfied(GameState);

        private bool CheckMatchesRule(bool? rule, bool check) =>
            (rule ?? check) == check;

        private bool IsTrue(bool b) => b == true;

        private bool AreChecksMet(Rule rule) =>
            rule.Key.Zip(GameChecks, CheckMatchesRule).ToList().All(IsTrue);

        public RefereeActionSequence Evaluate(
            PokerGameState gameState, List<IGameStateCheck> checks
        )
        {
            GameState = gameState;
            GameChecks = checks.Select(RunGameCheck).ToList();
            return Ruleset.Where(AreChecksMet).First().Value;
        }
    }
}
