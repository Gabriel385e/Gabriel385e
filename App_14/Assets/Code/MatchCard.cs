using TMPro;
using UnityEngine;

namespace Code
{
    public class MatchCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI type;
        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private TextMeshProUGUI score;

        public void Init(string type, int time, int score)
        {
            this.type.text = type;
            
            int minutes = time / 60;
            int secs = time % 60;
            this.time.text = $"{minutes:00}:{secs:00}";

            this.score.text = $"Score {score}";
        }
    }
}