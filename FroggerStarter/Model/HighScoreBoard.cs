using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    /// <summary>The high scores</summary>
    public class HighScoreBoard
    {
        private const String highScoresRecord = "Resources/HighScores.txt";

        private ObservableCollection<HighScore> highScores;

        private HighScoreElement sortedBy;

        /// <summary>Gets or sets the high scores.</summary>
        /// <value>The high scores.</value>
        public ObservableCollection<HighScore> HighScores
        {
            get => highScores;
            set => highScores = value; 
        }

        /// <summary>Initializes a new instance of the <see cref="HighScoreBoard"/> class.</summary>
        public HighScoreBoard()
        {
            highScores = new ObservableCollection<HighScore>();
            this.sortedBy = HighScoreElement.Score;
            this.populateScoreBoard();
            this.highScores = this.GetSortedBy(HighScoreElement.Score);
        }

        private void populateScoreBoard()
        {
            using (var reader = new StreamReader(highScoresRecord))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split(" ");
                    String name = data[0];
                    int score = Int32.Parse(data[1]);
                    int level = Int32.Parse(data[2]);

                    this.AddHighScore(new HighScore(name, score, level));
                }
            }
        }

        /// <summary>Adds the high score.</summary>
        /// <param name="score">The score.</param>
        public void AddHighScore(HighScore score)
        {
            this.highScores.Add(score);
            this.highScores = this.GetSortedBy(this.sortedBy);
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
            if (highScoreElement == HighScoreElement.Name)
            {
                this.sortedBy = HighScoreElement.Name;

                var list = new ObservableCollection<HighScore>(HighScores.OrderBy(x => x.Name).ThenByDescending(x => x.Score).ThenByDescending(x => x.Level).ToList());
                this.highScores = list;
                return list;
            }
            else if (highScoreElement == HighScoreElement.Score)
            {
                this.sortedBy = HighScoreElement.Score;

                var list = new ObservableCollection<HighScore>(HighScores.OrderByDescending(x => x.Score).ThenBy(x => x.Name).ThenByDescending(x => x.Level).ToList());
                this.highScores = list;
                return list;
            }
            else
            {
                this.sortedBy = HighScoreElement.Level;

                var list = new ObservableCollection<HighScore>(HighScores.OrderByDescending(x => x.Level).ThenByDescending(x => x.Score).ThenBy(x => x.Name).ToList());
                this.highScores = list;
                return list;
            }  
        }

        /// <summary>Clears the scores.</summary>
        /// <returns></returns>
        public ObservableCollection<HighScore> ClearScores()
        {
            var list = new ObservableCollection<HighScore>();
            this.highScores = list;
            return this.highScores;
        }
    }
}
