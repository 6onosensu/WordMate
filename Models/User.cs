using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMate.Models
{
    public class User
    {
        public string UserName { get; set; }
        public List<UserLevel> Levels { get; set; }
        public UserLevel CurrentLevel { get; set; }

        public User(string userName) 
        {
            UserName = userName;
            Levels = new List<UserLevel>();
        }

        public void AddLevel(UserLevel level)
        {
            Levels.Add(level);
            if (CurrentLevel == null)
            {
                CurrentLevel = level;
            }
        }   

        public void SetCurrentLevel(int levelNumber)
        {
            CurrentLevel = Levels.FirstOrDefault(l => l.LevelNumber == levelNumber);
        }
    }
}
