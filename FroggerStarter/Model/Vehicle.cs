using System;
using FroggerStarter.View.Sprites;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace FroggerStarter.Model
{
    /// <summary>A Vehicle. Travels in a straight line.</summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Vehicle : GameObject
    {
        private readonly Direction orientation;

        /// <summary>Initializes a new instance of the <see cref="Vehicle"/> class.</summary>
        /// <param name="vehicleType">Type of the vehicle.</param>
        /// <param name="orientation">The orientation.</param>
        public Vehicle(VehicleType vehicleType, Direction orientation)
        {
            this.orientation = orientation;
            Sprite = this.makeSprite(vehicleType);
        }

        private BaseSprite makeSprite(VehicleType vehicleType)
        {
            if (vehicleType == VehicleType.Car)
            {
                var carSprite = new CarSprite();
                this.rotateSprite(carSprite);
                return carSprite;
            }

            var truckSprite = new TruckSprite();
            this.rotateSprite(truckSprite);
            return truckSprite;
        }

        private void rotateSprite(BaseSprite vehicleSprite)
        {
            if (vehicleSprite == null)
            {
                throw new ArgumentNullException(nameof(vehicleSprite));
            }

            if (this.orientation == Direction.Left)
            {
                vehicleSprite.RenderTransformOrigin = new Point(0.5, 0.5);
                vehicleSprite.RenderTransform = new ScaleTransform { ScaleX = -1 };
            }
        }

        /// <summary>Moves the vehicle in the direction of its orientation</summary>
        public void MoveForward()
        {
            if (this.orientation == Direction.Left)
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
