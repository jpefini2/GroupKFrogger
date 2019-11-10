using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;
using System;

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

        private readonly DispatcherTimer animationTimer;

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
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.setupDeathAnimation();
            this.IsDying = false;
            this.animationTimer = new DispatcherTimer();
            this.animationTimer.Tick += this.switchDeathSprite;
            this.animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void setupDeathAnimation()
        {
            this.DeathSprites = new BaseSprite[NumOfDeathAnimationFrames];

            var deathSprite1 = new FrogDeathSprite1 {Visibility = Visibility.Collapsed};
            var deathSprite2 = new FrogDeathSprite2 {Visibility = Visibility.Collapsed};
            var deathSprite3 = new FrogDeathSprite3 {Visibility = Visibility.Collapsed};
            var deathSprite4 = new FrogDeathSprite4 {Visibility = Visibility.Collapsed};

            this.DeathSprites[0] = deathSprite1;
            this.DeathSprites[1] = deathSprite2;
            this.DeathSprites[2] = deathSprite3;
            this.DeathSprites[3] = deathSprite4;
        }

        /// <summary>Plays the death animation.</summary>
        public void PlayDeathAnimation()
        {
            this.IsDying = true;
            Sprite.Visibility = Visibility.Collapsed;
            this.DeathSprites[0].Visibility = Visibility.Visible;
            this.animationTimer.Start();
        }

        private void switchDeathSprite(object sender, object e)
        {
            for (int i = 0; i < this.DeathSprites.Length; i++)
            {
                if (this.DeathSprites[i].Visibility == Visibility.Visible)
                {
                    if (i != (NumOfDeathAnimationFrames - 1))
                    {
                        this.DeathSprites[i].Visibility = Visibility.Collapsed;
                        this.DeathSprites[i + 1].Visibility = Visibility.Visible;
                        break;
                    }
                    else
                    {
                        this.DeathSprites[i].Visibility = Visibility.Collapsed;
                        Sprite.Visibility = Visibility.Visible;
                        this.animationTimer.Stop();
                        this.IsDying = false;
                        this.UpdateDeathSpritesLocation();
                    }
                }
            }
        }

        /// <summary>Moves the frog right along with its death sprites.
        /// Precondition: None
        /// Postcondition: X == X@prev + SpeedX</summary>
        public override void MoveRight()
        {
            base.MoveRight();
            this.UpdateDeathSpritesLocation();
        }

        /// <summary>Moves the frog left along with its death sprites.
        /// Precondition: None
        /// Postcondition: X == X@prev + SpeedX</summary>
        public override void MoveLeft()
        {
            base.MoveLeft();
            this.UpdateDeathSpritesLocation();
        }

        /// <summary>Moves the frog up along with its death sprites.
        /// Precondition: None
        /// Postcondition: Y == Y@prev - SpeedY</summary>
        public override void MoveUp()
        {
            base.MoveUp();
            this.UpdateDeathSpritesLocation();
        }

        /// <summary>Moves the frog down along with its death sprites.
        /// Precondition: None
        /// Postcondition: Y == Y@prev + SpeedY</summary>
        public override void MoveDown()
        {
            base.MoveDown();
            this.UpdateDeathSpritesLocation();
        }

        /// <summary>Updates the death sprites location to this frogs current location</summary>
        public void UpdateDeathSpritesLocation()
        {
            foreach (var deathSprite in this.DeathSprites)
            {
                deathSprite.RenderAt(X, Y);
            }
        }

        #endregion
    }
}