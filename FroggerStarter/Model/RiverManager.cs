using System;
using System.Drawing;

namespace FroggerStarter.Model
{
    /// <summary>Manages a River</summary>
    /// <seealso cref="FroggerStarter.Model.LaneManager" />
    public class RiverManager : LaneManager
    {
        /// <summary>Gets or sets the collision box.</summary>
        /// <value>The collision box.</value>
        public Rectangle CollisionBox { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RiverManager"/> class.</summary>
        /// <param name="y">The y.</param>
        /// <param name="laneLength">Length of the lane.</param>
        /// <param name="laneWidth">Width of the lane.</param>
        /// <exception cref="ArgumentOutOfRangeException">y
        /// or
        /// laneLength
        /// or
        /// laneWidth</exception>
        public RiverManager(int y, int laneLength, int laneWidth) : base(y, laneLength, laneWidth)
        {
            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }
            if (laneLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(laneLength));
            }
            if (laneWidth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(laneWidth));
            }

            this.CollisionBox = new Rectangle(0, y, laneLength, 0);
        }

        /// <summary>Adds the lane.</summary>
        /// <param name="laneSettings">The lane settings.</param>
        public new void AddLane(LaneSettings laneSettings)
        {
            var laneY = Y + Lanes.Count * LaneWidth;
            var lane = new Lane(laneY, LaneLength, LaneWidth, laneSettings.TrafficDirection);
            this.fillLaneWithVehicles(laneSettings, lane);
            Lanes.Add(lane);

            this.CollisionBox = new Rectangle(0, Y, LaneLength, Lanes.Count * LaneWidth);
        }

        private void fillLaneWithVehicles(LaneSettings laneSettings, Lane lane)
        {
            var vehicles = new Vehicle[laneSettings.TrafficAmount];
            for (var i = 0; i < laneSettings.TrafficAmount; i++)
            {
                var vehicle = VehicleFactory.MakeVehicle(laneSettings.TrafficType, laneSettings.TrafficDirection, laneSettings.StartingTrafficSpeed);
                vehicles[i] = vehicle;
            }
            lane.SetAndSpaceVehicles(vehicles);
        }

        /// <summary>Moves the traffic in all lanes</summary>
        public new void MoveTraffic()
        {
            this.moveTrafficForwardInAllLanes();
        }

        private void moveTrafficForwardInAllLanes()
        {
            foreach (var lane in Lanes)
            {
                lane.MoveTraffic();
            }
        }
    }
}
