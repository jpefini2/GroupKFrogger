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

        public Vehicle MakeVehicle(VehicleType type, Direction orientation)
        {
            Vehicle vehicle;

            if (type == VehicleType.Car)
            {
                vehicle = new Car(orientation);
            }
            else if (type == VehicleType.TurboCar)
            {
                vehicle = new TurboCar(orientation);
            }
            else
            {
                vehicle = new Truck(orientation);
            }

            return vehicle;
        }
    }
}
