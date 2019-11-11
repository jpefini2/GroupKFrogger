using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using FroggerStarter.Controller;
using FroggerStarter.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage
    {
        #region Data members

        private readonly double applicationHeight = (double) Application.Current.Resources["AppHeight"];
        private readonly double applicationWidth = (double) Application.Current.Resources["AppWidth"];
        private readonly GameManager gameManager;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="GamePage"/> class.</summary>
        public GamePage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size
                {Width = this.applicationWidth, Height = this.applicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView()
                           .SetPreferredMinSize(new Size(this.applicationWidth, this.applicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;
            this.gameManager = new GameManager(this.applicationHeight, this.applicationWidth);
            this.gameManager.InitializeGame(this.canvas);

            this.gameManager.PlayerLivesUpdated += this.handlePlayerLivesChange;
            this.gameManager.PlayerScoreUpdated += this.handlePlayerScoreChange;
            this.gameManager.RemainingTimeUpdated += this.handleRemainingTimeChange;
            this.gameManager.GameOver += this.handleGameOver;

            this.highScoresListView.ItemsSource = this.gameManager.HighScoreBoard.HighScores;

            this.gameManager.StopGame();
        }

        #endregion

        #region Methods

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayer(Direction.Left);
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayer(Direction.Right);
                    break;
                case VirtualKey.Up:
                    this.gameManager.MovePlayer(Direction.Up);
                    break;
                case VirtualKey.Down:
                    this.gameManager.MovePlayer(Direction.Down);
                    break;
            }
        }

        private void handlePlayerLivesChange(object sender, PlayerLivesUpdatedEventArgs e)
        {
            this.livesTextBlock.Text = e.PlayerLives.ToString();
        }

        private void handlePlayerScoreChange(object sender, PlayerScoreUpdatedEventArgs e)
        {
            this.scoreTextBlock.Text = e.PlayerScore.ToString();
        }

        private void handleRemainingTimeChange(object sender, RemainingTimeUpdatedEventArgs e)
        {
            this.timeTextBlock.Text = e.RemainingTime.ToString();
        }

        private void handleGameOver(object sender, GameOverEventArgs e)
        {
            this.gameOverSprite.Visibility = Visibility.Visible;
            this.highScoreBoard.Visibility = Visibility.Visible;
            this.showAddScoreElements();
            this.playAgainButton.Visibility = Visibility.Visible;
        }

        #endregion

        private void NameTextBox_TextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        {
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.gameManager.SavePlayerScore(this.nameTextBox.Text);
            this.highScoresListView.ItemsSource = this.gameManager.HighScoreBoard.GetSortedBySelected();

            this.addScoreButton.Visibility = Visibility.Collapsed;
            this.nameTextBox.Text = "";
        }

        private void SortByNameButton_Click(object sender, RoutedEventArgs e)
        {
            this.highScoresListView.ItemsSource = this.gameManager.HighScoreBoard.GetSortedBy(HighScoreElement.Name);
        }

        private void SortByScoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.highScoresListView.ItemsSource = this.gameManager.HighScoreBoard.GetSortedBy(HighScoreElement.Score);
        }

        private void SortByLevelButton_Click(object sender, RoutedEventArgs e)
        {
            this.highScoresListView.ItemsSource = this.gameManager.HighScoreBoard.GetSortedBy(HighScoreElement.Level);
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            this.highScoreBoard.Visibility = Visibility.Collapsed;
            this.playAgainButton.Visibility = Visibility.Collapsed;
            this.gameOverSprite.Visibility = Visibility.Collapsed;
            this.gameManager.RestartGame();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.startScreen.Visibility = Visibility.Collapsed;
            this.highScoreBoard.Visibility = Visibility.Collapsed;
            this.closeScoreBoardButton.Visibility = Visibility.Collapsed;
            this.gameManager.RestartGame();
        }

        private void ViewScoreBoardButton_Click(object sender, RoutedEventArgs e)
        {
            this.highScoreBoard.Visibility = Visibility.Visible;
            this.hideAddScoreElements();
            this.closeScoreBoardButton.Visibility = Visibility.Visible;

        }

        private void ResetScoreBoardButton_Click(object sender, RoutedEventArgs e)
        {
            this.highScoresListView.ItemsSource = this.gameManager.HighScoreBoard.ClearScores();
        }

        private void CloseScoreBoardButton_Click(object sender, RoutedEventArgs e)
        {
            this.highScoreBoard.Visibility = Visibility.Collapsed;
            this.closeScoreBoardButton.Visibility = Visibility.Collapsed;
        }

        private void hideAddScoreElements()
        {
            this.enterNameTextBlock.Visibility = Visibility.Collapsed;
            this.nameTextBox.Visibility = Visibility.Collapsed;
            this.addScoreButton.Visibility = Visibility.Collapsed;
        }

        private void showAddScoreElements()
        {
            this.enterNameTextBlock.Visibility = Visibility.Visible;
            this.nameTextBox.Visibility = Visibility.Visible;
            this.addScoreButton.Visibility = Visibility.Visible;
        }
    }
}