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
                .SetTurnPosition(1)
                .SetCenterPot(0f)
                .Build();

        public static readonly PokerGameState FlopBeginningState =
            PokerGameStateBuilder.Create()
                .SetPlayers(new()
                {
                    PokerPlayerBuilder.Create()
                        .SetStack(99f)
                        .SetAction(0f)
                        .SetHoleCards(new()
                        {
                            Card.TEN_OF_DIAMONDS,
                            Card.FOUR_OF_HEARTS
                        })
                        .Build(),

                    PokerPlayerBuilder.Create()
                        .SetStack(99f)
                        .SetAction(0f)
                        .SetHoleCards(new()
                        {
                            Card.EIGHT_OF_CLUBS,
                            Card.JACK_OF_CLUBS
                        })
                        .Build()
                })
                .SetBigBlindSize(10)
                .SetPlayerPositions(new() { 1, 0 })
                .SetPlayersInAction(new() { 1, 0 })
                .SetTurnPosition(0)
                .SetCenterPot(2f)
                .SetBoardCards(new()
                {
                    Card.FIVE_OF_HEARTS,
                    Card.KING_OF_CLUBS,
                    Card.TWO_OF_CLUBS
                })
                .Build();

        public static readonly PokerGameState TurnBeginningState =
            PokerGameStateBuilder.Create()
                .SetPlayers(new()
                {
                    PokerPlayerBuilder.Create()
                        .SetStack(99f)
                        .SetAction(0f)
                        .SetHoleCards(new()
                        {
                            Card.SIX_OF_SPADES,
                            Card.TEN_OF_HEARTS
                        })
                        .Build(),

                    PokerPlayerBuilder.Create()
                        .SetStack(99f)
                        .SetAction(0f)
                        .SetHoleCards(new()
                        {
                            Card.TEN_OF_SPADES,
                            Card.SEVEN_OF_DIAMONDS
                        })
                        .Build()
                })
                .SetBigBlindSize(10)
                .SetPlayerPositions(new() { 1, 0 })
                .SetPlayersInAction(new() { 1, 0 })
                .SetTurnPosition(0)
                .SetCenterPot(2f)
                .SetBoardCards(new()
                {
                    Card.JACK_OF_CLUBS,
                    Card.SEVEN_OF_SPADES,
                    Card.JACK_OF_HEARTS,
                    Card.EIGHT_OF_HEARTS
                })
                .Build();

        public static readonly PokerGameState RiverBeginningState =
            PokerGameStateBuilder.Create()
                .SetPlayers(new()
                {
                    PokerPlayerBuilder.Create()
                        .SetStack(99f)
                        .SetAction(0f)
                        .SetHoleCards(new()
                        {
                            Card.TWO_OF_DIAMONDS,
                            Card.EIGHT_OF_CLUBS
                        })
                        .Build(),

                    PokerPlayerBuilder.Create()
                        .SetStack(99f)
                        .SetAction(0f)
                        .SetHoleCards(new()
                        {
                            Card.SEVEN_OF_HEARTS,
                            Card.TWO_OF_CLUBS
                        })
                        .Build()
                })
                .SetBigBlindSize(10)
                .SetPlayerPositions(new() { 1, 0 })
                .SetPlayersInAction(new() { 1, 0 })
                .SetTurnPosition(0)
                .SetCenterPot(2f)
                .SetBoardCards(new()
                {
                    Card.SEVEN_OF_CLUBS,
                    Card.TEN_OF_DIAMONDS,
                    Card.FOUR_OF_SPADES,
                    Card.EIGHT_OF_HEARTS,
                    Card.TEN_OF_HEARTS
                })
                .Build();
    }
}
