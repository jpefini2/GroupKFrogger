using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class Car : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="orientation">The orientation.</param>
        /// <param name="speed">The speed.</param>
        public Car(Direction orientation, int speed) : base(orientation, speed)
        {
            var sprite = new CarSprite();
            RotateSprite(sprite);
            Sprite = sprite;
        }
    }
}
