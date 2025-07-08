using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class AllGameStatistics
    {
        private static AllGameStatistics _instance;
        public static AllGameStatistics Instance => _instance ??= new AllGameStatistics();

        public int TotalScore { get; private set; }
        public List<MatchResult> matchHistory { get; private set; }
        
        private AllGameStatistics()
        {
            matchHistory = new List<MatchResult>();
        }
        
        public void RecordMatch(bool win, int time, int score)
        {
            if (matchHistory.Count > 50)
            {
                matchHistory.RemoveAt(0);
            }
            
            matchHistory.Add(new MatchResult(win, time, score));

            TotalScore += score;
            SaveStatistics();
            
            Debug.Log($"{win} - {time} - {score}");
        }
        
        public void LoadStatistics()
        {
            string path = GetSavePath();

            if (!File.Exists(path))
            {
                RecordMatch(true, 80, 8);
                RecordMatch(false, 65, 6);
                RecordMatch(true, 100, 13);
                RecordMatch(false, 42, 2);
                RecordMatch(true, 77, 10);
                return;
            }

            string json = File.ReadAllText(path);
            SerializableStatistics data = JsonUtility.FromJson<SerializableStatistics>(json);
            TotalScore = data.TotalScore;
            matchHistory = data.matchHistory.Select(m => new MatchResult(m.isWin, m.time, m.score)).ToList();
        }
        
        public List<MatchResult> GetTop10Matches()
        {
            return matchHistory
                .Where(m => m.isWin) 
                .OrderByDescending(m => m.score / m.time)
                .Reverse()
                .Take(10)
                .ToList();
        }
        
        public List<MatchResult> GetAllMatches()
        {
            List<MatchResult> all = matchHistory.ToList();
            all.Reverse();
            return all;
        }
        
        public int GetFastestWinTime()
        {
            IEnumerable<MatchResult> winMatches = matchHistory.Where(m => m.isWin);
            if (!winMatches.Any())
                return 0;

            return winMatches.Min(m => m.time);
        }

        private void SaveStatistics()
        {
            SerializableStatistics data = new SerializableStatistics
            {
                matchHistory = matchHistory.Select(m => new MatchResult()
                {
                    isWin = m.isWin,
                    time = m.time,
                    score = m.score
                }).ToList()
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetSavePath(), json);
        }

        private string GetSavePath()
        {
            return Path.Combine(Application.persistentDataPath, "game_stats.json");
        }
        
        [System.Serializable]
        public struct MatchResult
        {
            public bool isWin;
            public int time;
            public int score;

            public MatchResult(bool isWin, int time, int score)
            {
                this.isWin = isWin;
                this.time = time;
                this.score = score;
            }
        }
        
        [System.Serializable]
        public class SerializableStatistics
        {
            public int TotalScore;
            public List<MatchResult> matchHistory = new();
        }
    }
}