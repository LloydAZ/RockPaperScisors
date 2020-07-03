using System;
using System.Collections.Generic;

namespace RockPaperScisors
{
    class Player
    {
        #region Private Variables and Collections

        private Dictionary<string, int> opponentSelections = new Dictionary<string, int>();
        private string[] selections = { "Rock", "Paper", "Scisors" };
        private int score = 0;

        #endregion

        #region Properties

        public string playerName { get; set; }

        public string playerSelection { get; private set; }

        public int playerScore
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }

        #endregion

        #region Constructors

        public Player(string playerName)
        {
            this.playerName = playerName;
            this.score = 0;

            opponentSelections.Add("Rock", 0);
            opponentSelections.Add("Paper", 0);
            opponentSelections.Add("Scisors", 0);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The player makes their selection of either Rock, Paper, or Scisors.
        /// </summary>
        public void makeSelection()
        {
            playerSelection = getSelection(predictOpponentSelection());
        }

        /// <summary>
        /// Store the opponents last selection into the dictionary.
        /// </summary>
        /// <param name="selected"></param>
        public void opponentLastSelection(string selected)
        {
            opponentSelections[selected] += 1;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// We are going to try to predict what the opponent is going to select based upon
        /// their past selections.  Whatever selection they have chosen the most will be
        /// what we are predicting they will choose for this round.
        /// </summary>
        /// <returns>The predicted value that the opponent will choose.</returns>
        private string predictOpponentSelection()
        {
            string highestSelected = String.Empty;
            int highestValue = 0;

            // Loop through the dictionary and compare the values for each item to see which
            // one is the highest and use that as the prediction.
            foreach (KeyValuePair<string, int> selected in opponentSelections)
            {
                if (selected.Value > highestValue)
                {
                    highestSelected = selected.Key;
                    highestValue = selected.Value;
                }
            }

            // If we don't have any values, then make a random selection.
            if (highestValue == 0)
            {
                Random random = new Random();
                highestSelected = selections[random.Next(3)];
            }

            return highestSelected;
        }

        /// <summary>
        /// Using the predicted value for the opponent, we want to use the selection that
        /// will win against theirs.
        /// However, we are adding in a bit of randomness as well.  We are going to roll a
        /// random number between 0 and 9.  If we roll a 0, we will randomly select something
        /// to return.  If we roll anything, we will return the predicted winning solution.
        /// </summary>
        /// <param name="selected">The value that we are predicting our opponent to use.</param>
        /// <returns>The value that we are going to use, either the predicted winning value,
        /// or the random value.</returns>
        private string getSelection(string selected)
        {
            string retVal = String.Empty;

            switch (selected)
            {
                case "Rock":
                    retVal = "Paper";
                    break;
                case "Paper":
                    retVal = "Scisors";
                    break;
                case "Scisors":
                    retVal = "Rock";
                    break;
            }

            // We are going to determine if we are going to use the prediction determined above,
            // or just try randomly selecting a value.
            Random random = new Random();

            // If we roll a zero we select something random.
            // If we roll anything else we use the predicted value.
            int randomVal = random.Next(10);

            if (randomVal == 0)
            {
                retVal = selections[random.Next(3)];
            }

            return retVal;
        }

        #endregion
    }
}