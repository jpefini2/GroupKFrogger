using FroggerStarter.View.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    /// <summary>A Truck</summary>
    /// <seealso cref="FroggerStarter.Model.Vehicle" />
    public class Truck : Vehicle
    {

        /// <summary>Initializes a new instance of the <see cref="Truck"/> class.</summary>
        /// <param name="orientation">The orientation.</param>
        /// <param name="speed"></param>
        public Truck(Direction orientation, int speed) : base(orientation, speed)
        {
            TruckSprite sprite = new TruckSprite();
            this.RotateSprite(sprite);
            this.Sprite = sprite;
        }
    }
}
