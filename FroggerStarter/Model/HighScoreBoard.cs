using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FroggerStarter.Model
{
    public class HighScoreBoard
    {
        private const string HighScoresRecord = "Resources/HighScores.txt";

        private HighScoreElement sortedBy;

        public ObservableCollection<HighScore> HighScores { get; set; }

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

        public void AddHighScore(HighScore score)
        {
            this.HighScores.Add(score);
            this.HighScores = this.GetSortedBy(this.sortedBy);
        }

        public ObservableCollection<HighScore> GetSortedBySelected()
        {
            return this.GetSortedBy(this.sortedBy);
        }

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

        public ObservableCollection<HighScore> ClearScores()
        {
            var list = new ObservableCollection<HighScore>();
            this.HighScores = list;
            return this.HighScores;
        }
    }
}
