using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the vehicles as well as lives and score.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const int TopLaneOffset = 55;
        private const int BottomLaneOffset = 5;
        private const int RowsOnScreen = 13;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private Canvas gameCanvas;
        private DispatcherTimer timer;

        private DispatcherTimer countDownTimer;
        private int timeRemaining;

        private PlayerManager playerManager;
        private RoadManager roadManager;
        private RiverManager riverManager;
        private FrogHomeManager frogHomeManager;
        private SoundManager soundManager;
        private PowerupManager powerupManager;
        private GameSettings gameSettings;

        /// <summary>
        /// Gets or sets the high score board.
        /// </summary>
        /// <value>
        /// The high score board.
        /// </value>
        public HighScoreBoard HighScoreBoard { get; set; }

        private int currentLevel;

        /// <summary>Occurs when [player lives updated].</summary>
        public event EventHandler<PlayerLivesUpdatedEventArgs> PlayerLivesUpdated;

        /// <summary>Occurs when [player score updated].</summary>
        public event EventHandler<PlayerScoreUpdatedEventArgs> PlayerScoreUpdated;

        /// <summary>Occurs when [remaining time updated].</summary>
        public event EventHandler<RemainingTimeUpdatedEventArgs> RemainingTimeUpdated;

        /// <summary>
        /// Occurs when [game over].
        /// </summary>
        public event EventHandler<GameOverEventArgs> GameOver;

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
            this.riverManager.MoveTraffic();
            this.checkRoadCollision();
            this.checkRiverCollision();
            this.checkPowerupCollision();
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

            if (this.timeRemaining <= 5)
            {
                this.soundManager.PlayTimeRunningOutSound();
            }

            if (this.timeRemaining == 0)
            {
                this.killPlayer();
            }
        }

        private void checkRoadCollision()
        {
            if (this.roadManager.VehiclesAreCollidingWith(this.playerManager.CollisionBox))
            {
                this.playerHitByVehicle();
            }
        }

        private void checkRiverCollision()
        {
            if (this.playerManager.CollisionBox.IntersectsWith(this.riverManager.CollisionBox))
            {
                var isOnFlotationDevice = false;
                foreach (var vehicle in this.riverManager)
                {
                    if (this.playerManager.CollisionBox.IntersectsWith(vehicle.CollisionBox))
                    {
                        isOnFlotationDevice = true;
                        this.playerManager.MovePlayerWith(vehicle);
                    }
                }

                if (!isOnFlotationDevice)
                {
                    this.playerDrowns();
                }
            }
        }

        /// <summary>
        /// Kills the player.
        /// </summary>
        private void killPlayer()
        {
            this.playerManager.KillPlayer();
            this.onPlayerLivesUpdated();
            this.setPlayerToCenterOfBottomLane();
            this.roadManager.ResetToOneVehiclePerLane();
            this.timeRemaining = this.gameSettings.TimeLimit;
            this.onRemainingTimeUpdated();

            if (this.playerManager.Lives == 0)
            {
                this.gameOver();
            }
            
        }

        private void playerHitByVehicle()
        {
            if (!this.powerupManager.IsInvincibilityActive)
            {
                this.killPlayer();
                this.soundManager.PlayVehicleCollisionSound();
            }
        }

        private void playerDrowns()
        {
            this.killPlayer();
            this.soundManager.PlayerSplashSound();
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame()
        {
            this.currentLevel = 1;
            this.playerManager.Lives = this.gameSettings.NumberOfStartingLives;
            this.playerManager.Score = 0;
            this.timeRemaining = this.gameSettings.TimeLimit;
            this.unloadLevelAssets();
            this.createAndPlaceRoad();
            this.createAndPlaceRiver();
            this.createAndPlacePlayer();
            this.powerupManager.StartSpawningPowerups();

            this.timer.Start();
            this.countDownTimer.Start();
            this.playerManager.SetSpeed(50, 50);

            this.onPlayerScoreUpdated();
            this.onPlayerLivesUpdated();
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

            if (this.frogHomeManager.AllHomesAreFilled())
            {
                this.soundManager.PlayLevelCompletedSound();
                if (this.currentLevel < this.gameSettings.NumberOfLevels)
                {
                    this.loadNextLevel();
                }
                else
                {
                    this.gameOver();
                }           
            }
        }

        private void checkPowerupCollision()
        {
            foreach (var powerup in this.powerupManager)
            {
                if (powerup.Sprite.Visibility == Visibility.Visible && this.playerManager.CollisionBox.IntersectsWith(powerup.CollisionBox))
                {
                    switch (powerup)
                    {
                        case ExtraTimePowerup _:
                            this.timeRemaining += 10;
                            this.onRemainingTimeUpdated();
                            break;
                        case TemporaryInvincibilityPowerup _:
                            this.soundManager.PlayInvincibilityActiveSound();
                            break;
                    }

                    this.powerupManager.PickedUp(powerup);
                    this.soundManager.PlayPowerUpTakenSound();
                }
            }
        }

        private void gameOver()
        {
            this.StopGame();
            this.soundManager.PlayGameOverSound();
            this.unloadLevelAssets();
            this.onGameOver();
        }

        private void loadNextLevel()
        {
            this.unloadLevelAssets();
            this.currentLevel++;
            this.createAndPlaceRoad();
            this.createAndPlaceRiver();
        }

        private void unloadLevelAssets()
        {
            foreach (var vehicle in this.roadManager)
            {
                this.gameCanvas.Children.Remove(vehicle.Sprite);
            }
            foreach (var vehicle in this.riverManager)
            {
                this.gameCanvas.Children.Remove(vehicle.Sprite);
            }
            foreach (var sprite in this.playerManager.DeathSprites)
            {
                this.gameCanvas.Children.Remove(sprite);
            }
            this.gameCanvas.Children.Remove(this.playerManager.Sprite);
            this.gameCanvas.Children.Remove(this.playerManager.WalkingSprite);
            this.frogHomeManager.EmptyAllHomes();
        }

        /// <summary>
        /// Saves the player score.
        /// </summary>
        /// <param name="name">The name.</param>
        public void SavePlayerScore(string name)
        {
            var highScore = new HighScore(name, this.playerManager.Score, this.currentLevel);
            this.HighScoreBoard.AddHighScore(highScore);
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
            this.soundManager = new SoundManager();
            this.HighScoreBoard = new HighScoreBoard();
            
            this.timeRemaining = this.gameSettings.TimeLimit;
            this.currentLevel = 1;
            this.createAndPlaceRoad();
            this.createAndPlaceRiver();
            this.createAndPlacePlayer();
            this.createAndPlacePowerups();
            this.createAndPlaceFrogHomes();
            
        }

        private void createAndPlacePlayer()
        {
            var xBound = (int)this.backgroundWidth;
            var lowerYBound = (int)this.backgroundHeight;
            var upperYBound = TopLaneOffset + ((int)this.backgroundHeight - (BottomLaneOffset + TopLaneOffset)) / RowsOnScreen;

            this.playerManager = new PlayerManager(this.gameSettings.NumberOfStartingLives, xBound, lowerYBound, upperYBound);
            this.gameCanvas.Children.Add(this.playerManager.Sprite);
            this.gameCanvas.Children.Add(this.playerManager.WalkingSprite);

            foreach (var sprite in this.playerManager.DeathSprites)
            {
                this.gameCanvas.Children.Add(sprite);
            }

            this.setPlayerToCenterOfBottomLane();
            this.playerManager.PlayerMovingToShoulder += this.handlePlayerMovingToShoulder;
            this.playerManager.PlayerHitWall += this.handlePlayerHitWall;
        }

        private void setPlayerToCenterOfBottomLane()
        {
            var x = (int) (this.backgroundWidth / 2 - this.playerManager.Sprite.Width / 2);
            var y = (int) (this.backgroundHeight - this.playerManager.Sprite.Height - BottomLaneOffset);
            this.playerManager.MovePlayerToPoint(x, y);
        }

        private void createAndPlaceFrogHomes()
        {
            const int y = TopLaneOffset;
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
            var laneWidth = ((int)this.backgroundHeight - (BottomLaneOffset + TopLaneOffset)) / RowsOnScreen;
            var roadY = laneWidth * 7 + TopLaneOffset;

            this.roadManager = new RoadManager(roadY, roadLength, laneWidth);
            foreach (var laneSettings in this.gameSettings.LevelSettings[this.currentLevel - 1].RoadLaneSettings)
            {
                this.roadManager.AddLane(laneSettings);
            }

            foreach (var vehicle in this.roadManager)
            {
                this.gameCanvas.Children.Add(vehicle.Sprite);
            }
        }

        private void createAndPlaceRiver()
        {
            var riverLength = (int)this.backgroundWidth;
            var laneWidth = ((int)this.backgroundHeight - (BottomLaneOffset + TopLaneOffset)) / RowsOnScreen;
            var riverY = laneWidth + TopLaneOffset;

            this.riverManager = new RiverManager(riverY, riverLength, laneWidth);
            foreach (var laneSettings in this.gameSettings.LevelSettings[this.currentLevel - 1].RiverLaneSettings)
            {
                this.riverManager.AddLane(laneSettings);
            }

            foreach (var vehicle in this.riverManager)
            {
                this.gameCanvas.Children.Add(vehicle.Sprite);
            }
        }

        private void createAndPlacePowerups()
        {
            var xBound = (int)this.backgroundWidth;
            var lowerYBound = (int)this.backgroundHeight;
            var upperYBound = TopLaneOffset + ((int)this.backgroundHeight - (BottomLaneOffset + TopLaneOffset)) / RowsOnScreen;

            this.powerupManager = new PowerupManager(xBound, lowerYBound, upperYBound);
            foreach (var powerup in this.powerupManager)
            {
                this.gameCanvas.Children.Add(powerup.Sprite);
            }
        }

        /// <summary>Moves the player in the specified direction</summary>
        /// <param name="direction">The direction.</param>
        public void MovePlayer(Direction direction)
        {
            this.playerManager.MovePlayer(direction);
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

        private void onGameOver()
        {
            var data = new GameOverEventArgs { Score = this.playerManager.Score, Level = this.currentLevel };
            this.GameOver?.Invoke(this, data);
        }

        /// <summary>Stops the game.</summary>
        public void StopGame()
        {
            this.timer.Stop();
            this.countDownTimer.Stop();
            this.playerManager.SetSpeed(0, 0);
            this.powerupManager.StopSpawningPowerups();
        }

        private void handlePlayerMovingToShoulder(object sender, PlayerMovingToShoulderEventArgs e)
        {
            if (!this.frogHomeManager.IsCollidingWithEmptyHome(e.PositionOnShoulder))
            {
                return;
            }

            this.playerReachedHome();
            this.soundManager.PlayFilledHomeSound();
        }

        private void handlePlayerHitWall(object sender, PlayerHitWallEventArgs e)
        {
            this.soundManager.PlayHittingWallSound();
        }

        #endregion
    }

    /// <summary>
    /// Event args for game over
    /// </summary>
    public class GameOverEventArgs
    {
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level { get; set; }
    }

    /// <summary>Event args for change in player lives</summary>
    /// <seealso cref="System.EventArgs" />
    public class PlayerLivesUpdatedEventArgs : EventArgs
    {
        /// <summary>Gets or sets the player lives.</summary>
        /// <value>The player lives.</value>
        public int PlayerLives { get; set; }
    }

    /// <summary>Event args for change in player score</summary>
    /// <seealso cref="System.EventArgs" />
    public class PlayerScoreUpdatedEventArgs : EventArgs
    {
        /// <summary>Gets or sets the player score.</summary>
        /// <value>The player score.</value>
        public int PlayerScore { get; set; }
    }

    /// <summary>Event args for change in remaining time</summary>
    /// <seealso cref="System.EventArgs" />
    public class RemainingTimeUpdatedEventArgs : EventArgs
    {
        /// <summary>Gets or sets the remaining time.</summary>
        /// <value>The remaining time.</value>
        public int RemainingTime { get; set; }
    }
}