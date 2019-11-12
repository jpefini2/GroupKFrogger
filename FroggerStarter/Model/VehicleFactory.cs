
namespace FroggerStarter.Model
{
    /// <summary>Creates Vehicle Objects</summary>
    public class VehicleFactory
    {
        /// <summary>Makes the vehicle.</summary>
        /// <param name="type">The type.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="speed">The speed.</param>
        /// <returns></returns>
        public Vehicle MakeVehicle(VehicleType type, Direction orientation, int speed)
        {
            Vehicle vehicle;

            switch (type)
            {
                case VehicleType.Car:
                    vehicle = new Car(orientation, speed);
                    break;
                case VehicleType.TurboCar:
                    vehicle = new TurboCar(orientation, speed);
                    break;
                case VehicleType.Turtles:
                    vehicle = new Turtles(orientation, speed);
                    break;
                case VehicleType.Log:
                    vehicle = new Log(orientation, speed);
                    break;
                default:
                    vehicle = new Truck(orientation, speed);
                    break;
            }

            return vehicle;
        }
    }
}
