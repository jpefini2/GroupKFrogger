namespace FroggerStarter.Model
{
    /// <summary>The settings for an individual lane of the road</summary>
    public class LaneSettings
    {
        /// <summary>Gets the traffic direction.</summary>
        /// <value>The traffic direction.</value>
        public Direction TrafficDirection { get; }

        /// <summary>Gets the type of the traffic.</summary>
        /// <value>The type of the traffic.</value>
        public VehicleType TrafficType { get; }

        /// <summary>Gets the traffic amount.</summary>
        /// <value>The traffic amount.</value>
        public int TrafficAmount { get; }

        /// <summary>Gets the starting traffic speed.</summary>
        /// <value>The starting traffic speed.</value>
        public int StartingTrafficSpeed { get; }

        /// <summary>Initializes a new instance of the <see cref="LaneSettings"/> class.</summary>
        /// <param name="trafficDirection">The traffic direction.</param>
        /// <param name="trafficType">Type of the traffic.</param>
        /// <param name="trafficAmount">The traffic amount.</param>
        /// <param name="startingTrafficSpeed">The starting traffic speed.</param>
        public LaneSettings(Direction trafficDirection, VehicleType trafficType, int trafficAmount, int startingTrafficSpeed)
        {
            this.TrafficDirection = trafficDirection;
            this.TrafficType = trafficType;
            this.TrafficAmount = trafficAmount;
            this.StartingTrafficSpeed = startingTrafficSpeed;
        }
    }

}
