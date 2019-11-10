using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class LevelSettings : IEnumerable<LaneSettings>
    {
        private readonly List<LaneSettings> laneSettings;

        public LevelSettings(List<LaneSettings> laneSettings)
        {
            this.laneSettings = laneSettings;
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<LaneSettings> GetEnumerator()
        {
            return this.laneSettings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
