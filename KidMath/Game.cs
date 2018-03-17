using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidMath
{
    public class Game : DependencyObject
    {
        #region Fields

        #endregion Fields

        #region Properties
        /// <summary>
        /// This is the options of the game
        /// </summary>
        public Options GameOptions { get; private set; }
        /// <summary>
        /// This is the score of the game.
        /// </summary>
        public int Score
        {
            get;
            private set;
        }
        /// <summary>
        /// Is the game in progress or has it been concluded?
        /// </summary>
        public bool Finished
        {
            get;
            private set;
        }

        #region DependencyProperties
        private static DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(Game), new PropertyMetadata(0));
        private static DependencyProperty FinishedProperty = DependencyProperty.Register("Finished", typeof(bool), typeof(Game), new PropertyMetadata(false));
        #endregion DependencyProperties
        #endregion Properties

        #region Contructors
        /// <summary>
        /// Constructs an instance of a game.
        /// </summary>
        /// <param name="playerName">This is the name of the player.</param>
        public Game(Options settings)
        {
            this.GameOptions = settings;
        }
        #endregion Contructors
    }
}
