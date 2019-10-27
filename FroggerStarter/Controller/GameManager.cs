using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;
using System.Drawing;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the vehicles as well as lives and score.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const int BottomLaneOffset = 5;
        private const int rowsOnScreen = 8;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private GameSettings gameSettings;
        private Canvas gameCanvas;
        private Rectangle finishLine;
        private DispatcherTimer timer;
        private PlayerManager playerManager;
        private RoadManager roadManager;

        /// <summary>Delegate for handling a change in player lives</summary>
        /// <param name="lives">The lives.</param>
        public delegate void PlayerLivesHandler(int lives);
        /// <summary>Occurs when [player lives updated].</summary>
        public event PlayerLivesHandler PlayerLivesUpdated;

        /// <summary>Delegate for handling a change in player score</summary>
        /// <param name="score">The score.</param>
        public delegate void PlayerScoreHandler(int score);
        /// <summary>Occurs when [player score updated].</summary>
        public event PlayerScoreHandler PlayerScoreUpdated;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     backgroundHeight &lt;= 0
        ///     or
        ///     backgroundWidth &lt;= 0
        /// </exception>
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;

            this.setupGameTimer();
        }

        #endregion

        #region Methods

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            this.timer.Start();
        }

        private void timerOnTick(object sender, object e)
        {
            this.roadManager.MoveTraffic();
            this.checkPlayerCollision();
        }

        private void checkPlayerCollision()
        {
            if (this.roadManager.VehiclesAreCollidingWith(this.playerManager.CollisionBox))
            {
                this.playerHit();
                
            }

            if (this.playerManager.CollisionBox.IntersectsWith(this.finishLine))
            {
                this.playerReachedFinish();
            }
        }

        private void playerHit()
        {
            this.playerManager.Lives--;
            this.onPlayerLivesUpdated();
            this.setPlayerToCenterOfBottomLane();
        }

        private void playerReachedFinish()
        {
            this.playerManager.Score++;
            this.onPlayerScoreUpdated();
            this.setPlayerToCenterOfBottomLane();
            this.roadManager.SpeedUpTraffic();
        }

        /// <summary>
        ///     Initializes the game working with appropriate classes to play frog
        ///     and vehicle on game screen.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="gamePage">The game page.</param>
        /// <exception cref="ArgumentNullException">gameCanvas</exception>
        public void InitializeGame(Canvas gamePage)
        {
            this.gameCanvas = gamePage ?? throw new ArgumentNullException(nameof(gamePage));
            this.gameSettings = new GameSettings();

            this.createAndPlacePlayer();
            this.createAndPlaceFinishLine();
            this.createAndPlaceRoad();
            
            /*this.createAndPopulateRoad();*/
        }

        private void createAndPlacePlayer()
        {
            this.playerManager = new PlayerManager(this.gameSettings.NumberOfStartingLives);
            this.gameCanvas.Children.Add(this.playerManager.Sprite);
            this.setPlayerToCenterOfBottomLane();
        }

        private void setPlayerToCenterOfBottomLane()
        {
            var x = (int) (this.backgroundWidth / 2 - this.playerManager.Sprite.Width / 2);
            var y = (int) (this.backgroundHeight - this.playerManager.Sprite.Height - BottomLaneOffset);
            this.playerManager.MovePlayerToPoint(x, y);
        }

        private void createAndPlaceFinishLine()
        { 
            const int x = 0;
            var y = (int) this.backgroundHeight / 8;
            var width = (int) this.backgroundWidth;
            var height = (int) this.backgroundHeight / 8;
            this.finishLine = new Rectangle(x, y, width, height);
        }

        private void createAndPlaceRoad()
        {
            var roadLength = (int)this.backgroundWidth;
            var laneWidth = (int)this.backgroundHeight / rowsOnScreen;
            var roadY = laneWidth * 2;

            this.roadManager = new RoadManager(roadY, roadLength, laneWidth);
            foreach (var laneSettings in this.gameSettings)
            {
                this.roadManager.AddLane(laneSettings);
            }

            foreach (var vehicle in this.roadManager)
            {
                this.gameCanvas.Children.Add(vehicle.Sprite);
            }
        }

        /// <summary>Moves the player in the specified direction</summary>
        /// <param name="direction">The direction.</param>
        public void MovePlayer(Direction direction)
        {
            this.playerManager.MovePlayer(direction, (int)this.backgroundWidth, (int)this.backgroundHeight);
        }

        private void onPlayerLivesUpdated()
        {
            this.PlayerLivesUpdated?.Invoke(this.playerManager.Lives);
        }

        private void onPlayerScoreUpdated()
        {
            this.PlayerScoreUpdated?.Invoke(this.playerManager.Score);
        }

        /// <summary>Stops the game.</summary>
        public void StopGame()
        {
            this.timer.Stop();
            this.playerManager.SetSpeed(0, 0);
        }

        #endregion
    }
}