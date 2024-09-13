using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMate.Models
{
    public class UserLevel
    {
        public string LevelName { get; set; }
        public int LevelNumber { get; set; }
        public List<string> VocabularyWords { get; set; }
        public double Progress { get; set; }

        public UserLevel(string levelName, int levelNumber)
        {
            LevelName = levelName;
            LevelNumber = levelNumber;
            VocabularyWords = new List<string>();
            Progress = 0;
        }

        public void AddWord(string word)
        {
            VocabularyWords.Add(word);
        }

        public void UpdateProgress(double progress)
        {
            Progress = progress;
        }

        public override string ToString()
        {
            return $"{LevelName} (Level {LevelNumber}) - Progress: {Progress}%";
        }
    }
}
