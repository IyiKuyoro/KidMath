using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidMath
{
    enum Operation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
    class Game
    {
        #region Fields
        private readonly Settings settings;
        private bool isCompleted;
        private List<string> questions;
        private List<double> givenAns;
        private List<double> correctAns;
        public List<Operation> operations;
        private double score;
        #endregion Fields

        #region Properties
        public Settings Settings
        {
            get { return settings; }
        }
        public bool IsCompleted
        {
            get { return isCompleted; }
        }
        public double Score
        {
            get
            {
                return score;
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Returns a string array of all the questions that was asked.
        /// </summary>
        /// <returns>A sting array of asked questions</returns>
        public string[] GetQuestions()
        {
            return questions.ToArray();
        }
        /// <summary>
        /// Returns an integer array of all the answers that where provided
        /// </summary>
        /// <returns>An array of integers</returns>
        public double[] GetGivenAns()
        {
            return givenAns.ToArray();
        }
        /// <summary>
        /// Returns an integer array of the correct answers to the questions asked
        /// </summary>
        /// <returns>An integer array or correct answers</returns>
        public double[] GetCorrectAns()
        {
            return correctAns.ToArray();
        }
        /// <summary>
        /// Saves a copy of the question asked into a string array
        /// </summary>
        /// <param name="firstOperand">The first operand in the question</param>
        /// <param name="secondOperand">The second operand in the question</param>
        /// <param name="operation">The operator</param>
        public void SaveQuestion(int firstOperand, int secondOperand, Operation operation)
        {
            if (operation == Operation.Addition)
            {
                questions.Add(string.Format("{0} + {1}", firstOperand, secondOperand));
                correctAns.Add((double)firstOperand + (double)secondOperand);
            }
            else if (operation == Operation.Subtraction)
            {
                questions.Add(string.Format("{0} - {1}", firstOperand, secondOperand));
                correctAns.Add((double)firstOperand - (double)secondOperand);
            }
            else if (operation == Operation.Multiplication)
            {
                questions.Add(string.Format("{0} x {1}", firstOperand, secondOperand));
                correctAns.Add((double)firstOperand * (double)secondOperand);
            }
            else
            {
                questions.Add(string.Format("{0} / {1}", firstOperand, secondOperand));
                correctAns.Add((double)firstOperand / (double)secondOperand);
            }
        }
        /// <summary>
        /// Saves a copy of the answer the user provides for the question
        /// </summary>
        /// <param name="ans">The users answer</param>
        public void SaveAns(double ans)
        {
            givenAns.Add(ans);
        }
        /// <summary>
        /// Ends the game by computing the users score.
        /// </summary>
        public void EndGame()
        {
            isCompleted = true;
            double correct = 0;
            int i = 0;
            foreach(int ans in correctAns)
            {
                if (i < givenAns.Count)
                {
                    if (ans == givenAns[i])
                    {
                        correct++;
                    }
                    i++;
                }
            }

            score = ((correct / settings.Questions) * 100);
        }
        #endregion Methods

        #region Constructor
        public Game(Settings gameSettings)
        {
            isCompleted = false;
            settings = gameSettings;
            score = 0;
            questions = new List<string>();
            givenAns = new List<double>();
            correctAns = new List<double>();
            operations = new List<Operation>();

            if(settings.WillAdd)
                operations.Add(Operation.Addition);
            if (settings.WillSubtract)
                operations.Add(Operation.Subtraction);
            if (settings.WillMultiply)
                operations.Add(Operation.Multiplication);
            if (settings.WillDivide)
                operations.Add(Operation.Division);
        }
        #endregion Constructor
    }
}
