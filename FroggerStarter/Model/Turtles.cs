using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>Turtles</summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class Turtles : Vehicle
    {
        /// <summary>Initializes a new instance of the <see cref="Turtles"/> class.</summary>
        /// <param name="orientation">The orientation.</param>
        /// <param name="speed"></param>
        public Turtles(Direction orientation, int speed) : base(orientation, speed)
        {
            var sprite = new TurtleSprite();
            RotateSprite(sprite);
            Sprite = sprite;
        }
    }
}
