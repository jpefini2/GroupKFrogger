using System.Collections;
using System.Collections.Generic;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    public class FrogHome : GameObject
    {
        private bool isFilled;
        private BaseSprite filledSprite;

        public FrogHome()
        {
            this.Sprite = new FrogHomeSprite();
            this.isFilled = false;
            this.filledSprite = new FilledFrogHomeSprite();
        }

        public void FillHome()
        {
            this.isFilled = true;
            this.Sprite = new FilledFrogHomeSprite();
        }
    }
}