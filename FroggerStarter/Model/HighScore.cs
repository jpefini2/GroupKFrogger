
namespace FroggerStarter.Model
{
    public class HighScore
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level { get;  }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <value>
        /// To string.
        /// </value>
        public new string ToString => this.Name + ": Score: "+ this.Score + " Level: " + this.Level;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScore"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="score">The score.</param>
        /// <param name="level">The level.</param>
        public HighScore(string name, int score, int level)
        {
            this.Name = name;
            this.Score = score;
            this.Level = level;
        }
    }
}
