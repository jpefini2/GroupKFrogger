using System.Collections.Generic;

namespace FroggerStarter.Model
{
    /// <summary>The settings for a level</summary>
    public class LevelSettings
    {
        /// <summary>Gets or sets the road lane settings.</summary>
        /// <value>The road lane settings.</value>
        public List<LaneSettings> RoadLaneSettings { get; set; }

        /// <summary>Gets or sets the river lane settings.</summary>
        /// <value>The river lane settings.</value>
        public List<LaneSettings> RiverLaneSettings { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LevelSettings"/> class.</summary>
        public LevelSettings()
        {
            this.RoadLaneSettings = new List<LaneSettings>();
            this.RiverLaneSettings = new List<LaneSettings>();
        }
    }
}
