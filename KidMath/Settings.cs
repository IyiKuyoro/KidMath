using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidMath
{
    public class Settings : Window
    {
        #region Enum
        public enum Operations
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }
        #endregion Enum

        #region Fields
        #endregion Fields

        #region Properties
        /// <summary>
        /// The duration of the game
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
        public int MaxQuestions
        {
            get
            {
                return Time;
            }
        }
        public int MinQuestions
        {
            get
            {
                return Time/ 2;
            }
        }
        /// <summary>
        /// The number of questions to be answered
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
        /// This represents the maximum operand that will be used in generating questions.
        /// </summary>
        /// 
        public int MaxOperand
        {
            get
            {
                return (int)GetValue(MaxOperandProperty);
            }
            set
            {
                SetValue(MaxOperandProperty, value);
            }
        }
        /// <summary>
        /// The name of the player
        /// </summary>
        public string PlayerName
        {
            get
            {
                return (string)GetValue(PlayerNameProperty);
            }
            set
            {
                SetValue(PlayerNameProperty, value);
            }
        }

        #region DependencyProperties
        DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(int), typeof(Settings), new PropertyMetadata(10));
        DependencyProperty QuestionsProperty = DependencyProperty.Register("Questions", typeof(int), typeof(Settings), new PropertyMetadata(5));
        DependencyProperty MaxOperandProperty = DependencyProperty.Register("MaxOperand", typeof(int), typeof(Settings), new PropertyMetadata(9));
        DependencyProperty PlayerNameProperty = DependencyProperty.Register("PlayerName", typeof(string), typeof(Settings), new PropertyMetadata("Player"));
        #endregion DependencyProerties
        #endregion Properties

        #region Constructors
        public Settings()
        {
        }
        #endregion Constructors
    }
}