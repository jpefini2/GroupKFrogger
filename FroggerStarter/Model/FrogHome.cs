using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class FrogHome : GameObject
    {
        public bool IsFilled { get; set; }

        public BaseSprite FilledSprite { get; }

        public FrogHome()
        {
            this.Sprite = new FrogHomeSprite();
            this.IsFilled = false;
            this.FilledSprite = new FilledFrogHomeSprite();

            this.FilledSprite.Visibility = Visibility.Collapsed;
        }

        public void FillHome()
        {
            this.IsFilled = true;
            this.Sprite.Visibility = Visibility.Collapsed;
            this.FilledSprite.Visibility = Visibility.Visible;
        }
    }
}