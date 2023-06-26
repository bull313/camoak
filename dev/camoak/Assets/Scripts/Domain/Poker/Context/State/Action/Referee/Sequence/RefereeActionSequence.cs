using System.Collections.Generic;
using System.Linq;
using Camoak.Domain.Poker.Context.State.Cards.Dealer;
using Camoak.Domain.Poker.Context.State.Cards.Deck;
using Camoak.Domain.Poker.Context.State.Cards.Filter;
using Camoak.Domain.Poker.Context.State.Cards.Selection;

namespace Camoak.Domain.Poker.Context.State.Action.Referee.Sequence
{
    public class RefereeActionSequence
    {
        public static CardDealer Dealer { get; set; }

        public List<RefereeAction> Sequence { get; set; }

        static RefereeActionSequence() =>
            Dealer = new(
                new BasicDeckGenerator(new SeenCardsFilter()),
                new RandomCardSelector()
            );

        public RefereeActionSequence() => Sequence = new();

        private void UpdateGameState(RefereeAction a, PokerGameState gs) =>
            a.GameState = gs;

        private void ExecuteAction(RefereeAction action) =>
            action.Execute();

        public void SetGameState(PokerGameState gameState) =>
            Sequence.ForEach(a => UpdateGameState(a, gameState));

        public void ExecuteAll() => Sequence.ForEach(ExecuteAction);

        private int GetActionHashCode(RefereeAction action) =>
            action.GetHashCode();

        public override int GetHashCode() =>
            ($"{GetType().Name}"
            + $"{string.Join("", Sequence.Select(GetActionHashCode).ToArray())}"
            ).GetHashCode();
    }
}
