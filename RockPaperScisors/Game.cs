using System;

namespace RockPaperScisors
{
    class Game
    {
        #region Constants

        private const string DIVLINE = "---------------------------------------------------------------";

        #endregion

        #region Private Variables

        private Player playerOne;
        private Player playerTwo;
        private int numTurns = 0;
        private int numTies = 0;

        #endregion

        #region Constructor

        public Game()
        {
            setupGame();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The main method to play the game.
        /// </summary>
        public void playGame()
        {
            for (int i = 0; i < numTurns; i++)
            {
                makePlayerSelections();
                saveOpponentSelection();
                determineRoundWinner();
            }

            displayFinalScore();
            playAgain();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create the variables to start a new game.
        /// </summary>
        private void setupGame()
        {
            // Create the two player objects.
            Console.Write("Player One's Name: ");
            playerOne = new Player(Console.ReadLine());

            Console.Write("Player Two's Name: ");
            playerTwo = new Player(Console.ReadLine());

            // Get the number of turns.
            Console.Write("Number of Turns: ");
            Int32.TryParse(Console.ReadLine(), out numTurns);

            // The number of turns must be greater than one.
            if (numTurns <= 0)
            {
                // Keep reading until we get a valid number of turns.
                while (numTurns <= 0)
                {
                    Console.WriteLine("Invalid value for number of turns.  Try again.");
                    Console.Write("Number of Turns: ");
                    Int32.TryParse(Console.ReadLine(), out numTurns);
                }
            }
        }

        /// <summary>
        /// Display the final score the the players.
        /// </summary>
        private void displayFinalScore()
        {
            Console.WriteLine(DIVLINE);
            Console.WriteLine("Final Score");
            Console.WriteLine(DIVLINE);
            Console.WriteLine(String.Format("{0} - Score: {1}", playerOne.playerName, playerOne.playerScore));
            Console.WriteLine(String.Format("{0} - Score: {1}", playerTwo.playerName, playerTwo.playerScore));
            Console.WriteLine(String.Format("Number of Ties: {0}", numTies));

            if (playerOne.playerScore == playerTwo.playerScore)
            {
                Console.WriteLine("Game was a tie!");
            }
            else
            {
                string winner = "{0} wins with a score of {1}";

                if (playerOne.playerScore > playerTwo.playerScore)
                {
                    Console.WriteLine(String.Format(winner, playerOne.playerName, playerOne.playerScore));
                }
                else
                {
                    Console.WriteLine(String.Format(winner, playerTwo.playerName, playerTwo.playerScore));
                }
            }
        }

        /// <summary>
        /// Ask the players if they want to play again.  If they do, the reset the game,
        /// otherwise exit the game.
        /// </summary>
        private void playAgain()
        {
            Console.WriteLine(DIVLINE);
            Console.Write("Do you want to play again? (Y/N): ");
            string answer = Console.ReadLine();

            if (answer.ToUpper().StartsWith("Y"))
            {
                // Clear the console and setup a new game.
                Console.Clear();
                setupGame();
                playGame();
            }
            else
            {
                // Clear the console and end the game.
                Console.Clear();
            }
        }

        /// <summary>
        /// Have the players make their selections.
        /// </summary>
        private void makePlayerSelections()
        {
            playerOne.makeSelection();
            playerTwo.makeSelection();
        }

        /// <summary>
        /// Save the opponents last selection.
        /// </summary>
        private void saveOpponentSelection()
        {
            playerOne.opponentLastSelection(playerTwo.playerSelection);
            playerTwo.opponentLastSelection(playerOne.playerSelection);
        }

        /// <summary>
        /// Determine who won the round.
        /// </summary>
        private void determineRoundWinner()
        {
            Console.WriteLine(DIVLINE);
            Console.WriteLine(String.Format("{0} selected {1} - {2} selected {3}",
                playerOne.playerName,
                playerOne.playerSelection,
                playerTwo.playerName,
                playerTwo.playerSelection));

            // First check to see if the selections are the same.  If they are it's a tie.
            if (playerOne.playerSelection == playerTwo.playerSelection)
            {
                numTies++;
                Console.WriteLine("Tie!");
            }
            else
            {
                // Since we have already checked for a tie, we only need to test the other
                // two possible values against what was selected for player one.
                // playerTwo will win if the "if" statement is true, otherwise playerOne will win.
                switch (playerOne.playerSelection)
                {
                    // Player one chose "Rock"
                    case "Rock":
                        if (playerTwo.playerSelection == "Paper")
                        {
                            Console.WriteLine("Paper covers rock");
                            playerTwo.playerScore += 1;
                        }
                        else
                        {
                            Console.WriteLine("Rock crushes scisors");
                            playerOne.playerScore += 1;
                        }

                        break;

                    // Player one chose "Paper"
                    case "Paper":
                        if (playerTwo.playerSelection == "Scisors")
                        {
                            Console.WriteLine("Scisors cut paper");
                            playerTwo.playerScore += 1;
                        }
                        else
                        {
                            Console.WriteLine("Paper covers rock");
                            playerOne.playerScore += 1;
                        }

                        break;

                    // Player one chose "Scisors"
                    case "Scisors":
                        if (playerTwo.playerSelection == "Rock")
                        {
                            Console.WriteLine("Rock crushes scisors");
                            playerTwo.playerScore += 1;
                        }
                        else
                        {
                            Console.WriteLine("Scisors cut paper");
                            playerOne.playerScore += 1;
                        }

                        break;
                }
            }
        }

        #endregion
    }
}