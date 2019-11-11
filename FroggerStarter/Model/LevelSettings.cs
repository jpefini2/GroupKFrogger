using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class LevelSettings
    {
        public List<LaneSettings> RoadLaneSettings { get; set; }

        public List<LaneSettings> RiverLaneSettings { get; set; }

        public LevelSettings()
        {
            this.RoadLaneSettings = new List<LaneSettings>();
            this.RiverLaneSettings = new List<LaneSettings>();
        }
    }
}
