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
        /// <summary>
        /// Will the game include the addition operation?
        /// </summary>
        public bool WillAdd
        {
            get
            {
                return (bool)GetValue(WillAddProperty);
            }
            set
            {
                SetValue(WillAddProperty, value);
            }
        }
        /// <summary>
        /// Will the game include the subtraction operation?
        /// </summary>
        public bool WillSubtract
        {
            get
            {
                return (bool)GetValue(WillSubtractProperty);
            }
            set
            {
                SetValue(WillSubtractProperty, value);
            }
        }
        /// <summary>
        /// Will the game include the subtraction operation?
        /// </summary>
        public bool WillMultiply
        {
            get
            {
                return (bool)GetValue(WillMultiplyProperty);
            }
            set
            {
                SetValue(WillMultiplyProperty, value);
            }
        }
        /// <summary>
        /// Will the game include the subtraction operation?
        /// </summary>
        public bool WillDivide
        {
            get
            {
                return (bool)GetValue(WillDivideProperty);
            }
            set
            {
                SetValue(WillDivideProperty, value);
            }
        }

        #region DependencyProperties
        DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(int), typeof(Settings), new PropertyMetadata(10));
        DependencyProperty QuestionsProperty = DependencyProperty.Register("Questions", typeof(int), typeof(Settings), new PropertyMetadata(5));
        DependencyProperty MaxOperandProperty = DependencyProperty.Register("MaxOperand", typeof(int), typeof(Settings), new PropertyMetadata(9));
        DependencyProperty PlayerNameProperty = DependencyProperty.Register("PlayerName", typeof(string), typeof(Settings), new PropertyMetadata("Player"));
        DependencyProperty WillAddProperty = DependencyProperty.Register("WillAdd", typeof(bool), typeof(Settings), new PropertyMetadata(true));
        DependencyProperty WillSubtractProperty = DependencyProperty.Register("WillSubtract", typeof(bool), typeof(Settings), new PropertyMetadata(false));
        DependencyProperty WillMultiplyProperty = DependencyProperty.Register("WillMultiply", typeof(bool), typeof(Settings), new PropertyMetadata(false));
        DependencyProperty WillDivideProperty = DependencyProperty.Register("WillDivide", typeof(bool), typeof(Settings), new PropertyMetadata(false));
        #endregion DependencyProerties
        #endregion Properties

        #region Constructors
        public Settings()
        {
        }
        #endregion Constructors
    }
}