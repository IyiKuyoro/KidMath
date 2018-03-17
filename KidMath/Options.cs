using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidMath
{
    [Serializable]
    public class Options : DependencyObject
    {
        #region Enumerations
        public enum Operations
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }
        #endregion Enumerations

        #region Fields
        /// <summary>
        /// This is the list of operations that can be performed in the game.
        /// </summary>
        public readonly List<Operations> operationsList;
        #endregion Fields

        #region Properties
        /// <summary>
        /// This is the name of the player
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            private set { SetValue(NameProperty, value); }
        }
        /// <summary>
        /// This is the duration of the game.
        /// </summary>
        public int Time
        {
            get
            {
                return (int)GetValue(TimeProperty);
            }
            set
            {
                SetValue(TimeProperty, value);
            }
        }
        /// <summary>
        /// This is the number of questions for in the game.
        /// </summary>
        public int Questions
        {
            get
            {
                return (int)GetValue(QuestionsProperty);
            }
            set
            {
                SetValue(QuestionsProperty, value);
            }
        }
        /// <summary>
        /// This is the maximum number to be used as operand.
        /// </summary>
        public int MaxOperand { get; private set; }

        #region DependencyProperties
        private static DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Options), new PropertyMetadata("Player"));
        private static DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(int), typeof(Options), new PropertyMetadata("30"));
        private static DependencyProperty QuestionsProperty = DependencyProperty.Register("Questions", typeof(int), typeof(Options), new PropertyMetadata("15"));
        #endregion DependencyProperties
        #endregion Properties

        #region Contructors
        /// <summary>
        /// Constructs the options of a game.
        /// </summary>
        /// <param name="time">The time allocated for the game.</param>
        /// <param name="questions">The number of questions in the game.</param>
        /// <param name="maxOperand">The maximum operand that the game can have.</param>
        /// <param name="operations">The list of opperations that can be used in the game.</param>
        public Options(string name, int time,int questions,int maxOperand, List<Operations> operations)
        {
            this.Name = name;
            this.Time = time;
            this.Questions = questions;
            this.MaxOperand = maxOperand;
            operationsList = operations;
        }
        #endregion Contructors
    }
}
