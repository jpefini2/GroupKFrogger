using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    /// <summary>Manages a road consisting of a collection of lanes</summary>
    public class RoadManager : LaneManager
    {
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
        public RoadManager(int y, int laneLength, int laneWidth) : base(y, laneLength, laneWidth)
        {
            this.setupRevealVehicleTimer();
        } 

        private void setupRevealVehicleTimer()
        {
            this.revealVehicleTimer = new DispatcherTimer();
            this.revealVehicleTimer.Tick += this.revealVehicleTimerOnTick;
            this.revealVehicleTimer.Interval = new TimeSpan(0, 0, 0, 10);
            this.revealVehicleTimer.Start();
        }

        /// <summary>Adds the lane.</summary>
        /// <param name="laneSettings">The lane settings.</param>
        public override void AddLane(LaneSettings laneSettings)
        {
            base.AddLane(laneSettings);
            this.ResetToOneVehiclePerLane();
        }

        private void revealVehicleTimerOnTick(object sender, object e)
        {
            foreach (var lane in this.lanes)
            {
                lane.RevealRandomVehicle();
            }
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
    }
}
