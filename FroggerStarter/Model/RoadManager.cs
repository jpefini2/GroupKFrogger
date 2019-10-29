using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

        /// <summary>Sets the speed of the traffic in the specified lane to the specified speed</summary>
        /// <param name="laneIndex">Index of the lane.</param>
        /// <param name="speed">The speed.</param>
        public void SetLaneToSpeed(int laneIndex, int speed)
        {
            this.lanes[laneIndex].SetTrafficSpeed(speed);
        }

        /// <summary>Speeds up traffic in all lanes</summary>
        public void SpeedUpTraffic()
        {
            foreach (var lane in this.lanes)
            {
                lane.SpeedUpTraffic();
            }
        }

        /// <summary>Moves the traffic in all lanes</summary>
        public void MoveTraffic()
        {
            foreach (var lane in this.lanes)
            {
                lane.MoveTraffic();
            }
        }

        /// <summary>Adds a lane to the road with the given specifications</summary>
        /// <param name="trafficDirection">The traffic direction.</param>
        /* public void AddLane(Direction trafficDirection)
         {
             var laneY = this.y + this.lanes.Count * this.laneWidth;
             var lane = new Lane(laneY, this.laneLength, this.laneWidth,  trafficDirection);
             this.lanes.Add(lane);
         }*/

        public void AddLane(LaneSettings laneSettings)
        {
            var laneY = this.y + this.lanes.Count * this.laneWidth;
            var lane = new Lane(laneY, this.laneLength, this.laneWidth, laneSettings.TrafficDirection);

            var vehicles = new Vehicle[laneSettings.TrafficAmount];
            for (int i = 0; i < laneSettings.TrafficAmount; i++)
            {
                var vehicle = new Vehicle(laneSettings.TrafficType, laneSettings.TrafficDirection);
                vehicle.SetSpeed(laneSettings.StartingTrafficSpeed, 0);
                vehicles[i] = vehicle;
            }
            lane.AddVehicles(vehicles);
            lane.HideAllVehicles();
            lane.RevealRandomVehicle();
            this.lanes.Add(lane);
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



        public IEnumerator<Vehicle> GetEnumerator()
        {
            foreach (var lane in this.lanes)
            {
                foreach (var vehicle in lane)
                {
                    yield return vehicle;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
