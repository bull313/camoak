# CAMOAK
*He called me on a King High!*

## Story Exploration
* Poker Simulator
    * Title Screen/Main Menu
        * Play Game
            * Poker Setup
                * Player Selection
                    * Human (implied)
                    * Bot only
                * Game Setup
                    * Blind schedule
                        * Increment time (minute)
                        * Blind increase chart
                            * Save blind chart
                        * Starting stack
                        * Starting big blind
                * Gameplay
                    * Pause Game
                    * Preflop
                        * Button
                            * Beginning
                                * Fold
                                * Call
                                * Raise
                            * Facing Raise
                                * Fold
                                * Call
                                * Re-Raise
                        * Big Blind
                            * Button Call
                                * Fold
                                * Call
                                * Raise
                            * Facing Raise
                                * Fold
                                * Call
                                * Raise
                    * Flop/Turn
                        * Button
                            * Facing Check
                                * Check
                                * Bet
                            * Facing Bet
                                * Fold
                                * Call
                                * Raise
                            * Facing Raise
                                * Fold
                                * Call
                                * Raise
                        * Big Blind
                            * Beginning
                                * Check
                                * Bet
                            * Facing Bet
                                * Fold
                                * Call
                                * Raise
                            * Facing Raise
                                * Fold
                                * Call
                                * Raise
                    * River
                        * Button
                            * Facing Check
                                * Check
                                * Bet
                            * Facing Bet
                                * Fold
                                * Call
                                * Raise
                            * Facing Raise
                                * Fold
                                * Call
                                * Raise
                        * Big Blind
                            * Beginning
                                * Check
                                * Bet
                            * Facing Bet
                                * Fold
                                * Call
                                * Raise
                            * Facing Raise
                                * Fold
                                * Call
                                * Raise
        * Simulate Games
            * Poker Setup
                * Player Selection
                    * Slot 1 - Bot
                    * Slot 2 - Bot
                * Simulator Setup
                    * Number of games to simulate
                * Game Setup
                    * Blind schedule
                        * Increment number of hands
                            * Average number of hands
                            * Std. deviation number of hands
                            * Normal distribution
                        * Blind increase chart
                        * Starting stack
                        * Starting big blind
                * Gameplay
                    * ...
        * Theater mode
            * Search for game to review (file names by date/matchup/sim number)
            * Set game speed
            * Pause
            * Next action
            * Previous action
        
* Poker Bot
    * Game Tree Builder
        * Specify Flop
        * Specify Pot
        * Specify Player Stacks
        * Specify Bing Blind Size
        * Specify Game Tree
            * Specify if raising is allowed
            * Specify all-in inclusion
            * Specify max raises
            * Specify raise size range
            * Specify bet size range
        * Solver Settings
            * Specify number of iterations OR max regret
            * Specify monte carlo
    * Solver
        * CFR implementation
