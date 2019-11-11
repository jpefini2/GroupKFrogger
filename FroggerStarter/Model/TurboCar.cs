using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>A faster car</summary>
    /// <seealso cref="FroggerStarter.Model.Car" />
    public class TurboCar : Car
    {
        /// <summary>Initializes a new instance of the <see cref="TurboCar"/> class.</summary>
        /// <param name="orientation">The orientation.</param>
        /// <param name="speed">The speed.</param>
        public TurboCar(Direction orientation, int speed) : base(orientation, speed * 2)
        {
            var sprite = new TurboCarSprite();
            RotateSprite(sprite);
            Sprite = sprite;
        }
    }
}
