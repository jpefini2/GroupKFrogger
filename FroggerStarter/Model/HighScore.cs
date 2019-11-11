using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    /// <summary>An entry in the HighScoreBoard</summary>
    public class HighScore
    {
        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public String Name { get; }

        /// <summary>Gets the score.</summary>
        /// <value>The score.</value>
        public int Score { get; }

        /// <summary>Gets the level.</summary>
        /// <value>The level.</value>
        public int Level { get;  }

        /// <summary>Converts to string.</summary>
        /// <value>To string.</value>
        public new String ToString => Name + ": Score: "+ Score + " Level: " + Level;

        /// <summary>Initializes a new instance of the <see cref="HighScore"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="score">The score.</param>
        /// <param name="level">The level.</param>
        public HighScore(String name, int score, int level)
        {
            this.Name = name;
            this.Score = score;
            this.Level = level;
            
        }
    }
}
