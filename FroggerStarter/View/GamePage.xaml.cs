using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using FroggerStarter.Controller;
using FroggerStarter.View.Sprites;

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

        private void handlePlayerLivesChange(object sender, PlayerLivesUpdatedEventArgs e)
        {
            this.livesTextBlock.Text = e.PlayerLives.ToString();
            if (e.PlayerLives <= 0)
            {
                this.gameManager.StopGame();
                this.canvas.Children.Add(new GameOverSprite());
            }
        }        private void handlePlayerScoreChange(object sender, PlayerScoreUpdatedEventArgs e)
        {
            this.scoreTextBlock.Text = e.PlayerScore.ToString();
            if (this.gameManager.PlayerHasWon())
            {
                this.gameManager.StopGame();
                this.canvas.Children.Add(new GameOverSprite());
            }
        }

        private void handleRemainingTimeChange(object sender, RemainingTimeUpdatedEventArgs e)
        {
            this.timeTextBlock.Text = e.RemainingTime.ToString();
        }

        #endregion
    }
}