using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    /// <summary>Creates Vehicle Objects</summary>
    public class VehicleFactory
    {

        /// <summary>Initializes a new instance of the <see cref="VehicleFactory"/> class.</summary>
        public VehicleFactory()
        {
        }

        /// <summary>Makes the vehicle.</summary>
        /// <param name="type">The type.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="speed">The speed.</param>
        /// <returns></returns>
        public Vehicle MakeVehicle(VehicleType type, Direction orientation, int speed)
        {
            Vehicle vehicle;

            if (type == VehicleType.Car)
            {
                vehicle = new Car(orientation, speed);
            }
            else if (type == VehicleType.TurboCar)
            {
                vehicle = new TurboCar(orientation, speed);
            }
            else if (type == VehicleType.Turtles)
            {
                vehicle = new Turtles(orientation, speed);
            }
            else if (type == VehicleType.Log)
            {
                vehicle = new Log(orientation, speed);
            }
            else
            {
                vehicle = new Truck(orientation, speed);
            }

            return vehicle;
        }
    }
}
