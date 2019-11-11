using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class HighScore
    {
        public String Name { get; }

        public int Score { get; }

        public int Level { get;  }

        public HighScore(String name, int score, int level)
        {
            this.Name = name;
            this.Score = score;
            this.Level = level;
        }
    }
}
