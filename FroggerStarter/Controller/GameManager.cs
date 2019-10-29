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

        private Canvas gameCanvas;
        private DispatcherTimer timer;

        private DispatcherTimer countDownTimer;
        private int timeRemaining;

        private PlayerManager playerManager;
        private RoadManager roadManager;
        private FrogHomeManager frogHomeManager;
        private GameSettings gameSettings;

        public event EventHandler<PlayerLivesUpdatedEventArgs> PlayerLivesUpdated;
        public event EventHandler<PlayerScoreUpdatedEventArgs> PlayerScoreUpdated;
        public event EventHandler<RemainingTimeUpdatedEventArgs> RemainingTimeUpdated;

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
            this.setupCountDownTimer();
        }

        #endregion

        #region Methods

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.gameTimerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            this.timer.Start();
        }
        private void gameTimerOnTick(object sender, object e)
        {
            this.roadManager.MoveTraffic();
            this.checkPlayerCollision();
        }

        private void setupCountDownTimer()
        {
            this.countDownTimer = new DispatcherTimer();
            this.countDownTimer.Tick += this.countDownTimerOnTick;
            this.countDownTimer.Interval = new TimeSpan(0, 0, 0, 1);
            this.countDownTimer.Start();
        }

        private void countDownTimerOnTick(object sender, object e)
        {
            this.timeRemaining--;
            this.onRemainingTimeUpdated();

            if (this.timeRemaining == 0)
            {
                this.playerHit();
            }
        }

        private void checkPlayerCollision()
        {
            if (this.roadManager.VehiclesAreCollidingWith(this.playerManager.CollisionBox))
            {
                this.playerHit();
                
            }

            if (this.frogHomeManager.isCollidingWithEmptyHome(this.playerManager.CollisionBox))
            {
                
                this.playerReachedHome();
            }
        }

        private void playerHit()
        {
            this.playerManager.PlayDeathAnimation();
            this.playerManager.Lives--;
            this.onPlayerLivesUpdated();
            this.setPlayerToCenterOfBottomLane();

            this.timeRemaining = this.gameSettings.TimeLimit;
            this.onRemainingTimeUpdated();
        }

        private void playerReachedHome()
        {
            this.frogHomeManager.FillHomesIntersectingWith(this.playerManager.CollisionBox);
            this.playerManager.Score += this.timeRemaining * this.gameSettings.ScoreModifier;
            this.onPlayerScoreUpdated();
            this.setPlayerToCenterOfBottomLane();

            this.timeRemaining = this.gameSettings.TimeLimit;
            this.onRemainingTimeUpdated();
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
            this.timeRemaining = this.gameSettings.TimeLimit;
            
            this.createAndPlacePlayer();
            this.createAndPlaceFrogHomes();
            this.createAndPlaceRoad();
        }

        private void createAndPlacePlayer()
        {
            this.playerManager = new PlayerManager(this.gameSettings.NumberOfStartingLives);
            this.gameCanvas.Children.Add(this.playerManager.Sprite);

            foreach (var sprite in this.playerManager.DeathSprites)
            {
                this.gameCanvas.Children.Add(sprite);
            }

            this.setPlayerToCenterOfBottomLane();
        }

        private void setPlayerToCenterOfBottomLane()
        {
            var x = (int) (this.backgroundWidth / 2 - this.playerManager.Sprite.Width / 2);
            var y = (int) (this.backgroundHeight - this.playerManager.Sprite.Height - BottomLaneOffset);
            this.playerManager.MovePlayerToPoint(x, y);
        }

        private void createAndPlaceFrogHomes()
        {
            var y = ((int) (this.backgroundHeight / rowsOnScreen));
            this.frogHomeManager = new FrogHomeManager(y, (int) this.backgroundWidth, this.gameSettings.NumberOfFrogHomes);

            foreach (var frogHome in this.frogHomeManager)
            {
                this.gameCanvas.Children.Add(frogHome.Sprite);
                this.gameCanvas.Children.Add(frogHome.FilledSprite);
            }
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

        public bool PlayerHasWon()
        {
            return this.frogHomeManager.AllHomesAreFilled();
        }
        
        private void onPlayerLivesUpdated()
        {
            var data = new PlayerLivesUpdatedEventArgs { PlayerLives = this.playerManager.Lives};
            this.PlayerLivesUpdated?.Invoke(this, data);
        }

        private void onPlayerScoreUpdated()
        {
            var data = new PlayerScoreUpdatedEventArgs { PlayerScore = this.playerManager.Score };
            this.PlayerScoreUpdated?.Invoke(this, data);
        }

        private void onRemainingTimeUpdated()
        {
            var data = new RemainingTimeUpdatedEventArgs { RemainingTime = this.timeRemaining };
            this.RemainingTimeUpdated?.Invoke(this, data);
        }

        /// <summary>Stops the game.</summary>
        public void StopGame()
        {
            this.timer.Stop();
            this.countDownTimer.Stop();
            this.playerManager.SetSpeed(0, 0);
        }

        #endregion
    }

    public class PlayerLivesUpdatedEventArgs : EventArgs
    {
        public int PlayerLives { get; set; }
    }

    public class PlayerScoreUpdatedEventArgs : EventArgs
    {
        public int PlayerScore { get; set; }
    }

    public class RemainingTimeUpdatedEventArgs : EventArgs
    {
        public int RemainingTime { get; set; }
    }
}