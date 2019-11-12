using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FroggerStarter.Model
{
    /// <summary>The high scores</summary>
    public class HighScoreBoard
    {
        private const string HighScoresRecord = "Resources/HighScores.txt";

        private HighScoreElement sortedBy;

        /// <summary>Gets or sets the high scores.</summary>
        /// <value>The high scores.</value>
        public ObservableCollection<HighScore> HighScores { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HighScoreBoard"/> class.</summary>
        public HighScoreBoard()
        {
            this.HighScores = new ObservableCollection<HighScore>();
            this.sortedBy = HighScoreElement.Score;
            this.populateScoreBoard();
            this.HighScores = this.GetSortedBy(HighScoreElement.Score);
        }

        private void populateScoreBoard()
        {
            using (var reader = new StreamReader(HighScoresRecord))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split(" ");
                    var name = data[0];
                    var score = int.Parse(data[1]);
                    var level = int.Parse(data[2]);

                    this.AddHighScore(new HighScore(name, score, level));
                }
            }
        }

        /// <summary>Adds the high score.</summary>
        /// <param name="score">The score.</param>
        public void AddHighScore(HighScore score)
        {
            this.HighScores.Add(score);
            this.HighScores = this.GetSortedBy(this.sortedBy);
        }

        /// <summary>Gets the sorted by selected.</summary>
        /// <returns></returns>
        public ObservableCollection<HighScore> GetSortedBySelected()
        {
            return this.GetSortedBy(this.sortedBy);
        }

        /// <summary>Gets the sorted by.</summary>
        /// <param name="highScoreElement">The high score element.</param>
        /// <returns></returns>
        public ObservableCollection<HighScore> GetSortedBy(HighScoreElement highScoreElement)
        {
            switch (highScoreElement)
            {
                case HighScoreElement.Name:
                {
                    this.sortedBy = HighScoreElement.Name;

                    var list = new ObservableCollection<HighScore>(this.HighScores.OrderBy(x => x.Name).ThenByDescending(x => x.Score).ThenByDescending(x => x.Level).ToList());
                    this.HighScores = list;
                    return list;
                }
                case HighScoreElement.Score:
                {
                    this.sortedBy = HighScoreElement.Score;

                    var list = new ObservableCollection<HighScore>(this.HighScores.OrderByDescending(x => x.Score).ThenBy(x => x.Name).ThenByDescending(x => x.Level).ToList());
                    this.HighScores = list;
                    return list;
                }
                default:
                {
                    this.sortedBy = HighScoreElement.Level;

                    var list = new ObservableCollection<HighScore>(this.HighScores.OrderByDescending(x => x.Level).ThenByDescending(x => x.Score).ThenBy(x => x.Name).ToList());
                    this.HighScores = list;
                    return list;
                }
            }
        }

        /// <summary>Clears the scores.</summary>
        /// <returns></returns>
        public ObservableCollection<HighScore> ClearScores()
        {
            var list = new ObservableCollection<HighScore>();
            this.HighScores = list;
            return this.HighScores;
        }
    }
}
