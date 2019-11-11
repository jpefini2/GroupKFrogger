using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    public class HighScoreBoard
    {
        private const String highScoresRecord = "Resources/HighScores.txt";

        private ObservableCollection<HighScore> highScores;

        private HighScoreElement sortedBy;

        public ObservableCollection<HighScore> HighScores
        {
            get => highScores;
            set => highScores = value; 
        }

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

        public void AddHighScore(HighScore score)
        {
            this.highScores.Add(score);
            this.highScores = this.GetSortedBy(this.sortedBy);
        }

        public ObservableCollection<HighScore> GetSortedBySelected()
        {
            return this.GetSortedBy(this.sortedBy);
        }

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

        public ObservableCollection<HighScore> ClearScores()
        {
            var list = new ObservableCollection<HighScore>();
            this.highScores = list;
            return this.highScores;
        }
    }
}
