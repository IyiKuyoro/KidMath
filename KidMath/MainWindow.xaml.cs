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

namespace KidMath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Serializable]
    public partial class MainWindow : Window
    {
        #region Fields
        public Settings GameSettings;
        #endregion Fields

        #region Methods
        /// <summary>
        /// Fades out and collapses all the buttons on the start screen.
        /// </summary>
        private void FadeOutButtons()
        {
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };
            fadeAnimation.Completed += CollapseStartPage;

            cmdStartGame.BeginAnimation(Button.OpacityProperty, fadeAnimation);
            cmdSettings.BeginAnimation(Button.OpacityProperty, fadeAnimation);
            cmdHistory.BeginAnimation(Button.OpacityProperty, fadeAnimation);
            cmdAbout.BeginAnimation(Button.OpacityProperty, fadeAnimation);
        }
        /// <summary>
        /// Collapses all settings controls
        /// </summary>
        private void CollapseSettingsScreen()
        {
            settingsScreen.Visibility = Visibility.Collapsed;
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
            cmdStartGame.BeginAnimation(Button.OpacityProperty, fadeAnimation);
            cmdSettings.BeginAnimation(Button.OpacityProperty, fadeAnimation);
            cmdHistory.BeginAnimation(Button.OpacityProperty, fadeAnimation);
            cmdAbout.BeginAnimation(Button.OpacityProperty, fadeAnimation);
        }
        private void ReturnToStartScreen()
        {
            //Collapses settings screen and display start screen
            CollapseSettingsScreen();
            container.Visibility = Visibility.Visible;
            ShowStartScreenControls();
        }

        #region EventHandlers
        /// <summary>
        /// Collapses all the buttons on the start screen.
        /// </summary>
        /// <param name="sender">The object that triggers this event.</param>
        /// <param name="e">The parameters passed by the object.</param>
        private void CollapseStartPage(object sender, EventArgs e)
        {
            container.Visibility = Visibility.Collapsed;
            settingsScreen.Visibility = Visibility.Visible;
        }
        private void cmdSettings_Click(object sender, RoutedEventArgs e)
        {
            FadeOutButtons();
        }
        /// <summary>
        /// Checks the box that was clicked and adjusts the settings.operationsList accordinly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            ReturnToStartScreen();
        }
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            ReturnToStartScreen();
        }
        private void cmbDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbQuestions.Items.Clear();

            //Re-Populate Questions ComboBox
            int curQs = GameSettings.MinQuestions;
            while (curQs <= GameSettings.MaxQuestions)
            {
                cmbQuestions.Items.Add(string.Format(curQs + " Questions"));
                curQs += 5;
            }

            GameSettings.Questions = GameSettings.Time / 2;
            cmbQuestions.SelectedItem = cmbQuestions.Items[0];
        }
        #endregion EventHandlers
        #endregion Methods

        #region Contructors
        public MainWindow()
        {
            InitializeComponent();
            GameSettings = new Settings();

            DataContext = GameSettings;

            //Populate Duration ComboBox
            cmbDuration.ItemsSource = new string[] { "10 Seconds", "20 Seconds", "30 Seconds", "40 Seconds", "50 Seconds", "60 Seconds" };
            //Populate MaxOperand ComboBox
            cmbMaxOperand.ItemsSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
        #endregion Contructors
    }
}