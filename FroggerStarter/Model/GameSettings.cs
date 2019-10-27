using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class GameSettings : IEnumerable<LaneSettings>

    {
    private List<LaneSettings> laneSettings;
    public int NumberOfStartingLives { get; }


    public GameSettings()
    {
        this.NumberOfStartingLives = 3;
        this.laneSettings = new List<LaneSettings>();

        this.laneSettings.Add(new LaneSettings(Direction.Right, VehicleType.Car, 3, 6));
        this.laneSettings.Add(new LaneSettings(Direction.Left, VehicleType.Truck, 2, 5));
        this.laneSettings.Add(new LaneSettings(Direction.Left, VehicleType.Car, 3, 4));
        this.laneSettings.Add(new LaneSettings(Direction.Right, VehicleType.Truck, 3, 3));
        this.laneSettings.Add(new LaneSettings(Direction.Left, VehicleType.Car, 2, 2));
    }

        public IEnumerator<LaneSettings> GetEnumerator()
        {
            return this.laneSettings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
