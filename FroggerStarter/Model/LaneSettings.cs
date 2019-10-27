using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class LaneSettings
    {
        public Direction TrafficDirection { get; }

        public VehicleType TrafficType { get; }

        public int TrafficAmount { get; }

        public int StartingTrafficSpeed { get; }

        


        public LaneSettings(Direction trafficDirection, VehicleType trafficType, int trafficAmount, int startingTrafficSpeed)
        {
            this.TrafficDirection = trafficDirection;
            this.TrafficType = trafficType;
            this.TrafficAmount = trafficAmount;
            this.StartingTrafficSpeed = startingTrafficSpeed;
        }
    }

}
