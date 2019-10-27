using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using FroggerStarter.Controller;
using Windows.UI.Xaml.Controls;
using System;

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
        }

        #endregion

        #region Methods

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayer(Model.Direction.Left);
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayer(Model.Direction.Right);
                    break;
                case VirtualKey.Up:
                    this.gameManager.MovePlayer(Model.Direction.Up);
                    break;
                case VirtualKey.Down:
                    this.gameManager.MovePlayer(Model.Direction.Down);
                    break;
            }
        }

        private void handlePlayerLivesChange(int lives)
        {
            this.livesTextBlock.Text = lives.ToString();
            if (lives <= 0)
            {
                this.gameManager.StopGame();
                displayGameOverDialog();
            }
        }        private void handlePlayerScoreChange(int score)
        {
            this.scoreTextBlock.Text = score.ToString();
            if (score >= 3)
            {
                this.gameManager.StopGame();
                displayYouWinDialog();
            }
        }        private static async void displayGameOverDialog()
        {
            var gameOverDialog = new ContentDialog
            {
                Title = "GAME-OVER",
                Content = "Better luck next time.",
                CloseButtonText = "Exit"
            };

            await gameOverDialog.ShowAsync();
            Environment.Exit(0);
        }        private static async void displayYouWinDialog()
        {
            var youWinDialog = new ContentDialog
            {
                Title = "YOU WIN",
                Content = "Congratulations!",
                CloseButtonText = "Exit"
            };

            await youWinDialog.ShowAsync();
            Environment.Exit(0);
        }

        #endregion
    }
}