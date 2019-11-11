using System;
using FroggerStarter.View.Sprites;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace FroggerStarter.Model
{
    /// <summary>A Vehicle. Travels in a straight line.</summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public abstract class Vehicle : GameObject
    {
        public Direction Orientation { get; } 

        /// <summary>Initializes a new instance of the <see cref="Vehicle"/> class.</summary>
        /// <param name="vehicleType">Type of the vehicle.</param>
        /// <param name="orientation">The orientation.</param>
        public Vehicle(Direction orientation, int speed)
        {
            this.Orientation = orientation;
            this.SetSpeed(speed, 0);
        }

        protected void RotateSprite(BaseSprite vehicleSprite)
        {
            if (vehicleSprite == null)
            {
                throw new ArgumentNullException(nameof(vehicleSprite));
            }

            if (this.Orientation == Direction.Left)
            {
                vehicleSprite.RenderTransformOrigin = new Point(0.5, 0.5);
                vehicleSprite.RenderTransform = new ScaleTransform { ScaleX = -1 };
            }
        }

        /// <summary>Moves the vehicle in the direction of its orientation</summary>
        public void MoveForward()
        {
            if (this.Orientation == Direction.Left)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }
    }
}
