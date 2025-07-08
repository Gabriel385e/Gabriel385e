using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code
{
    public class MatchHistory : MonoBehaviour
    {
        [SerializeField] private MatchCard prefab;
        [SerializeField] private Transform content;
        [SerializeField] private TextMeshProUGUI bestTime;
        [SerializeField] private TextMeshProUGUI totalScore;

        private List<MatchCard> _created = new();
        
        private void OnEnable()
        {
            Reload();
        }

        private void Reload()
        {
            foreach (MatchCard card in _created)
            {
                Destroy(card.gameObject);
            }
            
            foreach (AllGameStatistics.MatchResult match in AllGameStatistics.Instance.GetAllMatches())
            {
                MatchCard card = Instantiate(prefab, content);
                card.Init(match.isWin ? "Win" : "Lose", match.time, match.score);
                _created.Add(card);
            }

            totalScore.text = AllGameStatistics.Instance.TotalScore.ToString();
            Time();
        }
        
        private void Time()
        {
            int time = AllGameStatistics.Instance.GetFastestWinTime();
            string timeString;
            
            if (time <= 0)
            {
                timeString = "0";
            }
            else
            {
                int minutes = time / 60;
                int secs = time % 60;
                timeString = $"{minutes:00}:{secs:00}";
            }
            
            bestTime.text = timeString;
        }
    }
}