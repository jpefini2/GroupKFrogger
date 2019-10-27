using FroggerStarter.View.Sprites;
using System.Drawing;

namespace FroggerStarter.Model
{
    /// <summary>Manages the Player</summary>
    public class PlayerManager
    {
        private readonly Frog player;

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

        /// <summary>Initializes a new instance of the <see cref="PlayerManager"/> class.</summary>
        /// <param name="startingLives">The starting lives of the player</param>
        public PlayerManager(int startingLives)
        {
            this.player = new Frog();
            this.Lives = startingLives;
            this.Score = 0;
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
        /// <param name="xBounds">The x bounds.</param>
        /// <param name="yBounds">The y bounds.</param>
        public void MovePlayer(Direction direction, int xBounds, int yBounds)
        {
            switch (direction)
            {
                case Direction.Left:
                    this.movePlayerLeft();
                    break;
                case Direction.Right:
                    this.movePlayerRight(xBounds);
                    break;
                case Direction.Up:
                    this.movePlayerUp();
                    break;
                default:
                    this.movePlayerDown(yBounds);
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
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: none
        ///     Postcondition: player.X = player.X@prev + player.Width
        /// </summary>
        private void movePlayerRight(int xBounds)
        {
            if (this.player.X + this.player.SpeedX + this.player.Width <= xBounds)
            {
                this.player.MoveRight();
            }
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: none
        ///     Postcondition: player.Y = player.Y@prev - player.Height
        /// </summary>
        private void movePlayerUp()
        {
            if (this.player.Y - this.player.SpeedY >= 0)
            {
                this.player.MoveUp();
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Postcondition: player.Y = player.Y@prev + player.Height
        /// </summary>
        private void movePlayerDown(int yBounds)
        {
            if (this.player.Y + this.player.SpeedY + this.player.Width <= yBounds)
            {
                this.player.MoveDown();
            }
        }

        /// <summary>Sets the speed of the player</summary>
        /// <param name="xSpeed">The x speed.</param>
        /// <param name="ySpeed">The y speed.</param>
        public void SetSpeed(int xSpeed, int ySpeed)
        {
            this.player.SetSpeed(xSpeed, ySpeed);
        }

    }
}
