
namespace FroggerStarter.Model
{
    public class VehicleFactory
    {
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
                default:
                    vehicle = new Truck(orientation, speed);
                    break;
            }

            return vehicle;
        }
    }
}
