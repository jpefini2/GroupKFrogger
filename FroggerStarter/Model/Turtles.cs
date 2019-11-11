using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TurtleSprite sprite = new TurtleSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}
