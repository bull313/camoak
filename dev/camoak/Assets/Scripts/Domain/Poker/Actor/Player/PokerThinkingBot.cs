using System;
using System.Threading;
using System.Threading.Tasks;
using Camoak.Domain.Poker.Context.State.Action.Player;
using Camoak.Domain.Poker.Context.State.Filter;

namespace Camoak.Domain.Poker.Actor.Player
{
    public class PokerThinkingBot : PokerPlayerActor
    {
        public const int MIN_THINKING_MILLIS = 250;
        public const int MAX_THINKING_MILLIS = 1750;

        private PokerPlayerActor PokerBot { get; set; }
        private Random Randomizer { get; set; }

        public override FilteredPokerGameState GameState
        { get => PokerBot.GameState; }

        public PokerThinkingBot(PokerPlayerActor bot) : base(null)
        {
            PokerBot = bot;
            Randomizer = new();
        }

        private void Think() =>
            Thread.Sleep(
                Randomizer.Next(MIN_THINKING_MILLIS, MAX_THINKING_MILLIS)
            );

        public async override Task<PlayerAction> SelectAction()
        {
            await Task.Run(Think);
            return await PokerBot.SelectAction();
        }
    }
}
