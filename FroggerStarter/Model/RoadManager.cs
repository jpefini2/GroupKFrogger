using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    /// <summary>Manages a road consisting of a collection of lanes</summary>
    public class RoadManager : IEnumerable<Vehicle>
    {
        private readonly int y;
        private readonly int laneLength;
        private readonly int laneWidth;
        private readonly List<Lane> lanes;

        private DispatcherTimer revealVehicleTimer;

        /// <summary>Initializes a new instance of the <see cref="RoadManager"/> class.</summary>
        /// <param name="y">The y.</param>
        /// <param name="laneLength">Length of the lane.</param>
        /// <param name="laneWidth">Width of the lane.</param>
        /// <exception cref="ArgumentOutOfRangeException">y cannot be less than 0
        /// or
        /// length cannot be less than 0
        /// or
        /// width cannot be less than 0</exception>
        public RoadManager(int y, int laneLength, int laneWidth)
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

            this.y = y;
            this.laneLength = laneLength;
            this.laneWidth = laneWidth;
            this.lanes = new List<Lane>();
            this.setupRevealVehicleTimer();
        }

        private void setupRevealVehicleTimer()
        {
            this.revealVehicleTimer = new DispatcherTimer();
            this.revealVehicleTimer.Tick += this.revealVehicleTimerOnTick;
            this.revealVehicleTimer.Interval = new TimeSpan(0, 0, 0, 10);
            this.revealVehicleTimer.Start();
        }

        private void revealVehicleTimerOnTick(object sender, object e)
        {
            foreach (var lane in this.lanes)
            {
                lane.RevealRandomVehicle();
            }
        }

        /// <summary>Moves the traffic in all lanes</summary>
        public void MoveTraffic()
        {
            this.moveTrafficForwardInAllLanes();
        }

        private void moveTrafficForwardInAllLanes()
        {
            foreach (var lane in this.lanes)
            {
                lane.MoveTraffic();
            }
        }

        /// <summary>Adds the lane.</summary>
        /// <param name="laneSettings">The lane settings.</param>
        public void AddLane(LaneSettings laneSettings)
        {
            var laneY = this.y + this.lanes.Count * this.laneWidth;
            var lane = new Lane(laneY, this.laneLength, this.laneWidth, laneSettings.TrafficDirection);
            FillLaneWithVehicles(laneSettings, lane);
            lane.HideAllVehicles();
            lane.RevealRandomVehicle();
            this.lanes.Add(lane);
        }

        private static void FillLaneWithVehicles(LaneSettings laneSettings, Lane lane)
        {
            var vehicles = new Vehicle[laneSettings.TrafficAmount];
            for (var i = 0; i < laneSettings.TrafficAmount; i++)
            {
                var vehicle = new Vehicle(laneSettings.TrafficType, laneSettings.TrafficDirection);
                vehicle.SetSpeed(laneSettings.StartingTrafficSpeed, 0);
                vehicles[i] = vehicle;
            }
            lane.SetAndSpaceVehicles(vehicles);
        }

        /// <summary>Checks if any vehicle in the road is colliding with the given collisionBox</summary>
        /// <param name="collisionBox">The bounding box.</param>
        /// <returns></returns>
        public bool VehiclesAreCollidingWith(Rectangle collisionBox)
        {
            var isHit = false;
            foreach (var vehicle in this)
            {
                if (collisionBox.IntersectsWith(vehicle.CollisionBox) && vehicle.Sprite.Visibility == Visibility.Visible)
                {
                    isHit = true;
                    break;
                }
            }
            return isHit;
        }

        /// <summary>Hides all but one randomly chosen vehicle in each lane</summary>
        public void ResetToOneVehiclePerLane()
        {
            foreach (var lane in this.lanes)
            {
                lane.HideAllVehicles();
                lane.RevealRandomVehicle();
            }
            this.revealVehicleTimer.Stop();
            this.revealVehicleTimer.Start();
        }



        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Vehicle> GetEnumerator()
        {
            return this.lanes.SelectMany(lane => lane).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
