using FroggerStarter.View.Sprites;
using System;
using System.Drawing;

namespace FroggerStarter.Model
{
    /// <summary>Manages the Player</summary>
    public class PlayerManager
    {
        private readonly Frog player;
        private readonly int xBounds;
        private readonly int lowerYBounds;
        private readonly int upperYBounds;

        /// <summary>Gets or sets the player's lives.</summary>
        /// <value>The lives.</value>
        public int Lives { get; set; }

        /// <summary>Gets or sets the player's score</summary>
        /// <value>The score.</value>
        public int Score { get; set; }

        /// <summary>Gets the collision box.</summary>
        /// <value>The collision box.</value>
        public Rectangle CollisionBox => this.player.CollisionBox;

        /// <summary>Gets the player's sprite.</summary>
        /// <value>The frog sprite.</value>
        public BaseSprite Sprite => this.player.Sprite;

        public BaseSprite WalkingSprite => this.player.WalkingSprite;

        /// <summary>Gets the death sprites.</summary>
        /// <value>The death sprites.</value>
        public BaseSprite[] DeathSprites => this.player.DeathSprites;

        public event EventHandler<PlayerMovingToShoulderEventArgs> PlayerMovingToShoulder;

        /// <summary>Occurs when [player lives updated].</summary>
        public event EventHandler<PlayerHitWallEventArgs> PlayerHitWall;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerManager" /> class.
        /// </summary>
        /// <param name="startingLives">The starting lives of the player</param>
        /// <param name="xBounds">The x bounds</param>
        /// <param name="lowerYBounds">The lower y bounds.</param>
        /// <param name="upperYBounds">The upper y bounds.</param>
        public PlayerManager(int startingLives, int xBounds, int lowerYBounds, int upperYBounds)
        {
            this.player = new Frog();
            this.Lives = startingLives;
            this.Score = 0;
            this.xBounds = xBounds;
            this.lowerYBounds = lowerYBounds;
            this.upperYBounds = upperYBounds;
        }

        /// <summary>Kills the player.</summary>
        public void KillPlayer()
        {
            this.player.PlayDeathAnimation();
            this.Lives--;
        }

        /// <summary>Moves the player to point instantly to the specified coordinates</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void MovePlayerToPoint(int x, int y)
        {
            this.player.X = x;
            this.player.Y = y;
        }

        /// <summary>
        /// Moves the player in the specified direction by his speed, as long
        /// as long as it remains within the specified bounds
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void MovePlayer(Direction direction)
        {
            if (this.player.IsDying)
            {
                return;
            }

            switch (direction)
            {
                case Direction.Left:
                    this.movePlayerLeft();
                    break;
                case Direction.Right:
                    this.movePlayerRight();
                    break;
                case Direction.Up:
                    this.movePlayerUp();
                    break;
                case Direction.Down:
                    this.movePlayerDown();
                    break;
            }
        }

        /// <summary>
        ///     Moves the player to the left.
        ///     Precondition: none
        ///     Postcondition: player.X = player.X@prev - player.Width
        /// </summary>
        private void movePlayerLeft()
        {
            if (this.player.X - this.player.SpeedX >= 0)
            {
                this.player.MoveLeft();
            }
            else
            {
                this.onPlayerHitWall();
            }
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: none
        ///     Postcondition: player.X = player.X@prev + player.Width
        /// </summary>
        private void movePlayerRight()
        {
            if (this.player.X + this.player.SpeedX + this.player.Width <= this.xBounds)
            {
                this.player.MoveRight();
            }
            else
            {
                this.onPlayerHitWall();
            }
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: none
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        private void movePlayerUp()
        {
            if (this.player.Y - this.player.SpeedY >= this.upperYBounds)
            {
                this.player.MoveUp();
            }
            else
            {
                this.onPlayerMovingToShoulder();
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        private void movePlayerDown()
        {
            if (this.player.Y + this.player.SpeedY + this.player.Width <= this.lowerYBounds)
            {
                this.player.MoveDown();
            }
            else
            {
                this.onPlayerHitWall();
            }
        }

        /// <summary>Sets the speed of the player</summary>
        /// <param name="xSpeed">The x speed.</param>
        /// <param name="ySpeed">The y speed.</param>
        public void SetSpeed(int xSpeed, int ySpeed)
        {
            this.player.SetSpeed(xSpeed, ySpeed);
        }

        private void onPlayerMovingToShoulder()
        {
            var data = new PlayerMovingToShoulderEventArgs { PositionOnShoulder = new Rectangle(this.player.X, this.player.Y - this.player.SpeedY, (int)this.player.Width, (int)this.player.Height) };
            this.PlayerMovingToShoulder?.Invoke(this, data);
        }

        private void onPlayerHitWall()
        {
            var data = new PlayerHitWallEventArgs();
            this.PlayerHitWall?.Invoke(this, data);
        }
    }

    public class PlayerHitWallEventArgs
    {
    }

    public class PlayerMovingToShoulderEventArgs : EventArgs
    {
        /// <summary>Gets or sets the remaining time.</summary>
        /// <value>The remaining time.</value>
        public Rectangle PositionOnShoulder { get; set; }
    }
}
