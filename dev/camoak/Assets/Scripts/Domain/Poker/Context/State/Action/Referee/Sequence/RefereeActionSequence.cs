using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards.Dealer;
using Camoak.Domain.Poker.Context.State.Cards.Deck;
using Camoak.Domain.Poker.Context.State.Cards.Filter;
using Camoak.Domain.Poker.Context.State.Cards.Selection;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.Sequence
{
    public abstract class RefereeActionSequence
    {
        public static CardDealer Dealer { get; set; }

        private PokerGameState GameState { get; set; }
        public List<RefereeAction> Sequence { get; set; }

        static RefereeActionSequence() =>
            Dealer = new(
                new BasicDeckGenerator(new SeenCardsFilter()),
                new RandomCardSelector()
            );

        public RefereeActionSequence() => Sequence = InitSequence();

        private void UpdateGameState(RefereeAction action) =>
            action.GameState = GameState;

        private void ExecuteAction(RefereeAction action) =>
            action.Execute();

        public void SetGameState(PokerGameState gameState)
        {
            GameState = gameState;
            Sequence.ForEach(UpdateGameState);
        }

        public void ExecuteAll() => Sequence.ForEach(ExecuteAction);

        private int GetActionHashCode(RefereeAction action) =>
            action.GetHashCode();

        protected abstract List<RefereeAction> InitSequence();

        public override int GetHashCode() =>
            ($"{GetType().Name}"
            + $"{string.Join("", Sequence.Select(GetActionHashCode).ToArray())}"
            ).GetHashCode();
    }
}
