using Camoak.Domain.Poker.Context.State;
using Camoak.Domain.Poker.Context.State.Cards;

namespace Camoak.Tests.Common.Poker
{
    public class PokerCommonGameStates
    {
        public static readonly PokerGameState PreflopBeginningState =
            PokerGameStateBuilder.Create()
                .SetPlayers(new()
                {
                    PokerPlayerBuilder.Create()
                        .SetStack(99.5f)
                        .SetAction(0.5f)
                        .SetHoleCards(new()
                        {
                            Card.JACK_OF_DIAMONDS,
                            Card.SEVEN_OF_CLUBS
                        })
                        .Build(),

                    PokerPlayerBuilder.Create()
                        .SetStack(99f)
                        .SetAction(1f)
                        .SetHoleCards(new()
                        {
                            Card.FIVE_OF_SPADES,
                            Card.THREE_OF_SPADES
                        })
                        .Build()
                })
                .SetBigBlindSize(10)
                .SetPlayerPositions(new() { 1, 0 })
                .SetPlayersInAction(new() { 1, 0 })
                .SetTurnPlayer(1)
                .SetCenterPot(0f)
                .Build();
    }
}
