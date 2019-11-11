using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class HighScoreBoard
    {
        IList<HighScore> highScores;

        public HighScoreBoard()
        {
            highScores = new List<HighScore>();
        }

        public void AddHighScore(HighScore score)
        {
            this.highScores.Add(score);
        }
    }
}
