using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class VehicleFactory
    {

        public VehicleFactory()
        {
        }

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
