using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Windows.Forms;

namespace KidMath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        public Settings gameSettings;
        private SerializableSettings serializableSettings;
        private Game game;
        private Timer gameTimer;
        private int questionsCounter;
        #endregion Fields

        #region Property
        private int Clock
        {
            get
            {
                return (int)GetValue(ClockProperty);
            }
            set
            {
                SetValue(ClockProperty, value);
            }
        }

        DependencyProperty ClockProperty = DependencyProperty.Register("Clock", typeof(int), typeof(MainWindow), new PropertyMetadata());
        #endregion Property

        #region Methods
        /// <summary>
        /// Fades out and collapses all the buttons on the start screen.
        /// </summary>
        private void FadeOutButtons(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };

            //I am doing this because using one animation for all buttons causes threading issues down the line.
            DoubleAnimation fadeAnimation1 = fadeAnimation.Clone();
            DoubleAnimation fadeAnimation2 = fadeAnimation.Clone();
            DoubleAnimation fadeAnimation3 = fadeAnimation.Clone();

            fadeAnimation.Completed += CollapseStartPage;
            if((System.Windows.Controls.Button)sender == cmdSettings)
                fadeAnimation.Completed += ShowSettingsScreen;

            if ((System.Windows.Controls.Button)sender == cmdStartGame)
            {
                fadeAnimation.Completed += ShowGameScreen;
                fadeAnimation.Completed += StartGame;
            }

            cmdStartGame.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, fadeAnimation);
            cmdSettings.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, fadeAnimation1);
            cmdHistory.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, fadeAnimation2);
            cmdAbout.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, fadeAnimation3);
        }

        private void StartGame(object sender, EventArgs e)
        {
            //Control the timer
            gameTimer = new Timer
            {
                Interval = 1000,
                Enabled = true
            };
            gameTimer.Tick += ReduceTime;
            gameTimer.Start();

            this.answer.Focus();
            messageBox.Text = "Let's play!";
            GenerateQuestion();
        }

        private void EndGame()
        {
            answer.IsEnabled = false;
        }

        /// <summary>
        /// Randormly creates a question.
        /// </summary>
        private void GenerateQuestion()
        {
            if (questionsCounter == game.Settings.Questions)
            {
                EndGame();
            }
            else
            {
                try
                {
                    Random rnd = new Random();
                    int firstOperand = rnd.Next(0, gameSettings.MaxOperand + 1);
                    int secondOperand = rnd.Next(0, gameSettings.MaxOperand + 1);
                    int opIndex = rnd.Next(0, game.operations.Count);

                    //Generate the operation
                    Operation operation = game.operations[opIndex];
                    string operationText;

                    //Display question
                    switch (operation)
                    {
                        case (Operation.Addition):
                            operationText = "+";
                            @operator.Text = operationText;
                            break;
                        case (Operation.Subtraction):
                            operationText = "-";
                            @operator.Text = operationText;
                            break;
                        case (Operation.Multiplication):
                            operationText = "x";
                            @operator.Text = operationText;
                            break;
                        case (Operation.Division):
                            operationText = "/";
                            @operator.Text = operationText;
                            break;
                        default:
                            throw new Exception("An error has occured generating the operation.");
                    }
                    txtFirstOperand.Text = firstOperand.ToString();
                    txtSecondOperand.Text = secondOperand.ToString();

                    game.SaveQuestion(firstOperand, secondOperand, operation);

                    questionsCounter++;     //increase quesions count
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void ReduceTime(object sender, EventArgs e)
        {
            if (Clock > 0)
            {
                Clock -= 1;

                if (Clock == 5)
                {
                    timer.Foreground = new SolidColorBrush(Colors.Red);
                    messageBox.Text = "Hurry Up!";
                }
            }
            else
            {
                gameTimer.Stop();
                EndGame();
            }
        }

        /// <summary>
        /// Fade in all the start screen controls.
        /// </summary>
        private void ShowStartScreenControls()
        {
            //Set visibility property of controls to visible.
            cmdStartGame.Visibility = Visibility.Visible;
            cmdHistory.Visibility = Visibility.Visible;
            cmdSettings.Visibility = Visibility.Visible;

            //Create fade animation
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };

            //Apply fade animation to the controls
            cmdStartGame.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, fadeAnimation);
            cmdSettings.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, fadeAnimation);
            cmdHistory.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, fadeAnimation);
            cmdAbout.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, fadeAnimation);
        }

        private void ReturnToStartScreen()
        {
            //Collapses settings screen and display start screen
            if(settingsScreen.Visibility == Visibility.Visible)
                settingsScreen.Visibility = Visibility.Collapsed;
            if (gameScreen.Visibility == Visibility.Visible)
                gameScreen.Visibility = Visibility.Collapsed;

            container.Visibility = Visibility.Visible;
            ShowStartScreenControls();
        }

        private void ReadSettings()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream fStream = new FileStream("settings.dat", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                serializableSettings = (SerializableSettings)formatter.Deserialize(fStream);
            }
            gameSettings.Time = serializableSettings.time;
            gameSettings.Questions = serializableSettings.questions;
            gameSettings.MaxOperand = serializableSettings.maxOperand;
            gameSettings.PlayerName = serializableSettings.playerName;
            gameSettings.WillAdd = serializableSettings.add;
            gameSettings.WillSubtract = serializableSettings.subtract;
            gameSettings.WillMultiply = serializableSettings.multiply;
            gameSettings.WillDivide = serializableSettings.divide;
        }

        private void WriteSettings()
        {
            //Save the settings data.
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream fStream = new FileStream("settings.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializableSettings.time = gameSettings.Time;
                serializableSettings.questions = gameSettings.Questions;
                serializableSettings.maxOperand = gameSettings.MaxOperand;
                serializableSettings.playerName = gameSettings.PlayerName;
                serializableSettings.add = gameSettings.WillAdd;
                serializableSettings.subtract = gameSettings.WillSubtract;
                serializableSettings.multiply = gameSettings.WillMultiply;
                serializableSettings.divide = gameSettings.WillDivide;

                formatter.Serialize(fStream, serializableSettings);
            }
        }

        /// <summary>
        /// Collapses all the buttons on the start screen.
        /// </summary>
        /// <param name="sender">The object that triggers this event.</param>
        /// <param name="e">The parameters passed by the object.</param>
        private void CollapseStartPage(object sender, EventArgs e)
        {
            container.Visibility = Visibility.Collapsed;
        }

        private void cmdSettings_Click(object sender, RoutedEventArgs e)
        {
            FadeOutButtons(sender, e);
        }

        private void ShowSettingsScreen(object sender, EventArgs e)
        {
            settingsScreen.Visibility = Visibility.Visible;
        }

        private void ShowGameScreen(object sender, EventArgs e)
        {
            gameScreen.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Checks the box that was clicked and adjusts the settings.operationsList accordinly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            ReadSettings();
            ReturnToStartScreen();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            WriteSettings();
            ReturnToStartScreen();
        }

        private void cmbDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbQuestions.Items.Clear();

            //Re-Populate Questions ComboBox
            int curQs = gameSettings.MinQuestions;
            while (curQs <= gameSettings.MaxQuestions)
            {
                cmbQuestions.Items.Add(string.Format(curQs + " Questions"));
                curQs += 5;
            }

            gameSettings.Questions = gameSettings.Time / 2;
            cmbQuestions.SelectedItem = cmbQuestions.Items[0];
        }

        private void cmdStartGame_Click(object sender, RoutedEventArgs e)
        {
            game = new Game(gameSettings);
            Clock = game.Settings.Time;     //initiates the timer to the game duration.
            FadeOutButtons(sender, e);
        }

        private void answer_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    game.SaveAns(Convert.ToInt32(this.answer.Text));
                    this.answer.Focus();
                    this.answer.Clear();
                    if (questionsCounter != 1)
                        messageBox.Text = string.Format("{0} questions to go!", game.Settings.Questions - questionsCounter);
                    else
                        messageBox.Text = string.Format("{0} question left.", game.Settings.Questions - questionsCounter);
                    GenerateQuestion();
                }
                catch(FormatException)
                {
                    this.messageBox.Text = "Oops! That was not a number.";
                    this.answer.Focus();
                    this.answer.Clear();
                }

                e.Handled = true;
            }
        }

        private void cmdEndGame_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
        }
        #endregion Methods

        #region Contructors
        public MainWindow()
        {
            InitializeComponent();
            gameSettings = new Settings();
            questionsCounter = 0;

            //Map GameSettings class to an instance of SerializableSettings
            serializableSettings = new SerializableSettings();
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists("settings.dat"))
            {
                ReadSettings();
            }
            else
            {
                WriteSettings();
            }

            DataContext = gameSettings;

            //Populate Duration ComboBox
            cmbDuration.ItemsSource = new string[] { "10 Seconds", "20 Seconds", "30 Seconds", "40 Seconds", "50 Seconds", "60 Seconds" };
            //Populate MaxOperand ComboBox
            cmbMaxOperand.ItemsSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
        #endregion Contructors
    }
}