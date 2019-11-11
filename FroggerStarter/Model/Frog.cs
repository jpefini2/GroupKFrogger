using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;
using System;
using Windows.UI.Xaml.Media;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Defines the frog model
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frog : GameObject
    {
        #region Data members

        private const int NumOfDeathAnimationFrames = 4;
        private const int SpeedXDirection = 50;
        private const int SpeedYDirection = 50;

        private DispatcherTimer deathAnimationTimer;
        private DispatcherTimer walkingAnimationTimer;

        public BaseSprite WalkingSprite { get; }

        /// <summary>
        /// Gets the death sprites that make up the frames of the death animation
        /// </summary>
        /// <value>The death sprites.</value>
        public BaseSprite[] DeathSprites { get; private set; }

        /// <summary>Gets or sets a value indicating whether the frog is dying.</summary>
        /// <value>
        ///   <c>true</c> true if the death animation is playing; otherwise, false<c>false</c>.</value>
        public bool IsDying { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frog" /> class.
        /// </summary>
        public Frog()
        {
            Sprite = new FrogSprite();
            this.WalkingSprite = new WalkingFrogSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.IsDying = false;
            this.setupDeathAnimation();
            this.setupWalkingAnimation();
        }

        private void setupDeathAnimation()
        {
            this.DeathSprites = new BaseSprite[NumOfDeathAnimationFrames];
            this.DeathSprites[0] = new FrogDeathSprite1 { Visibility = Visibility.Collapsed };
            this.DeathSprites[1] = new FrogDeathSprite2 { Visibility = Visibility.Collapsed };
            this.DeathSprites[2] = new FrogDeathSprite3 { Visibility = Visibility.Collapsed };
            this.DeathSprites[3] = new FrogDeathSprite4 { Visibility = Visibility.Collapsed };

            this.deathAnimationTimer = new DispatcherTimer();
            this.deathAnimationTimer.Tick += this.switchDeathSprite;
            this.deathAnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void setupWalkingAnimation()
        {
            this.WalkingSprite.Visibility = Visibility.Collapsed;

            this.walkingAnimationTimer = new DispatcherTimer();
            this.walkingAnimationTimer.Tick += this.stopWalking;
            this.walkingAnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
        }

        /// <summary>
        /// Stops the walking animation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void stopWalking(object sender, object e)
        {
            if (!this.IsDying)
            {
                Sprite.Visibility = Visibility.Visible;
            }
            this.WalkingSprite.Visibility = Visibility.Collapsed;
            this.walkingAnimationTimer.Stop();
        }

        /// <summary>
        /// Starts the walking animation.
        /// </summary>
        public void StartWalking()
        {
            this.WalkingSprite.Visibility = Visibility.Visible;
            Sprite.Visibility = Visibility.Collapsed;
            this.walkingAnimationTimer.Start();
        }

        /// <summary>
        /// Plays the death animation.
        /// </summary>
        public void PlayDeathAnimation()
        {
            this.IsDying = true;
            Sprite.Visibility = Visibility.Collapsed;
            this.DeathSprites[0].Visibility = Visibility.Visible;
            this.deathAnimationTimer.Start();
        }

        private void switchDeathSprite(object sender, object e)
        {
            for (var i = 0; i < this.DeathSprites.Length; i++)
            {
                if (this.DeathSprites[i].Visibility == Visibility.Visible)
                {
                    if (i != NumOfDeathAnimationFrames - 1)
                    {
                        this.DeathSprites[i].Visibility = Visibility.Collapsed;
                        this.DeathSprites[i + 1].Visibility = Visibility.Visible;
                        break;
                    }

                    this.DeathSprites[i].Visibility = Visibility.Collapsed;
                    Sprite.Visibility = Visibility.Visible;
                    this.deathAnimationTimer.Stop();
                    this.IsDying = false;
                    this.UpdateDeathSpritesLocation();
                }
            }
        }

        /// <summary>Moves the frog right along with its death sprites.
        /// Precondition: None
        /// Postcondition: X == X@prev + SpeedX</summary>
        public override void MoveRight()
        {
            this.StartWalking();
            this.rotateToFace(Direction.Right);
            base.MoveRight();
            this.UpdateDeathSpritesLocation();
            this.WalkingSprite.RenderAt(X, Y);
        }

        /// <summary>Moves the frog left along with its death sprites.
        /// Precondition: None
        /// Postcondition: X == X@prev + SpeedX</summary>
        public override void MoveLeft()
        {
            this.StartWalking();
            this.rotateToFace(Direction.Left);
            base.MoveLeft();
            this.UpdateDeathSpritesLocation();
            this.WalkingSprite.RenderAt(X, Y);
        }

        /// <summary>Moves the frog up along with its death sprites.
        /// Precondition: None
        /// Postcondition: Y == Y@prev - SpeedY</summary>
        public override void MoveUp()
        {
            this.StartWalking();
            this.rotateToFace(Direction.Up);
            base.MoveUp();
            this.UpdateDeathSpritesLocation();
            this.WalkingSprite.RenderAt(X, Y);
        }

        /// <summary>Moves the frog down along with its death sprites.
        /// Precondition: None
        /// Postcondition: Y == Y@prev + SpeedY</summary>
        public override void MoveDown()
        {
            this.StartWalking();
            this.rotateToFace(Direction.Down);
            base.MoveDown();
            this.UpdateDeathSpritesLocation();
            this.WalkingSprite.RenderAt(X, Y);
        }

        /// <summary>
        /// Updates the death sprites location to this frogs current location
        /// </summary>
        public void UpdateDeathSpritesLocation()
        {
            foreach (var deathSprite in this.DeathSprites)
            {
                deathSprite.RenderAt(X, Y);
            }
        }

        private void rotateToFace(Direction direction)
        {
            var angle = 0;
            switch (direction)
            {
                case Direction.Down:
                    angle = 180;
                    break;
                case Direction.Right:
                    angle = 90;
                    break;
                case Direction.Left:
                    angle = -90;
                    break;
            }

            var rotate = new RotateTransform();
            rotate.CenterX += Width / 2;
            rotate.CenterY += Height / 2;
            Sprite.RenderTransform = rotate;
            this.WalkingSprite.RenderTransform = rotate;
            rotate.Angle = angle;
        }

        #endregion
    }
}