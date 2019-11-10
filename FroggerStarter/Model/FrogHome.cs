using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>A frog home. Can be filled or empty</summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class FrogHome : GameObject
    {
        /// <summary>Gets or sets a value indicating whether this instance is filled.</summary>
        /// <value>
        ///   <c>true</c> if this instance is filled; otherwise, <c>false</c>.</value>
        public bool IsFilled { get; set; }

        /// <summary>Gets the sprite for when the home is filled</summary>
        /// <value>The filled sprite.</value>
        public BaseSprite FilledSprite { get; }

        /// <summary>Initializes a new instance of the <see cref="FrogHome"/> class.</summary>
        public FrogHome()
        {
            Sprite = new FrogHomeSprite();
            this.IsFilled = false;
            this.FilledSprite = new FilledFrogHomeSprite {Visibility = Visibility.Collapsed};
        }

        /// <summary>Fills the home.</summary>
        public void FillHome()
        {
            this.IsFilled = true;
            Sprite.Visibility = Visibility.Collapsed;
            this.FilledSprite.Visibility = Visibility.Visible;
        }

        public void EmptyHome()
        {
            this.IsFilled = false;
            Sprite.Visibility = Visibility.Visible;
            this.FilledSprite.Visibility = Visibility.Collapsed;
        }
    }
}