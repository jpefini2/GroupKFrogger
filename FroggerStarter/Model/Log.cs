using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>A Log</summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class Log : Vehicle
    {
        /// <summary>Initializes a new instance of the <see cref="Log"/> class.</summary>
        /// <param name="orientation">The orientation.</param>
        /// <param name="speed"></param>
        public Log(Direction orientation, int speed) : base(orientation, speed)
        {
            var sprite = new LogSprite();
            RotateSprite(sprite);
            Sprite = sprite;
        }
    }
}
