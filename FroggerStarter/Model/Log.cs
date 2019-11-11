using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            LogSprite sprite = new LogSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}
