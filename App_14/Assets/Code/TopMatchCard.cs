using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class TopMatchCard : MonoBehaviour
    {
        [SerializeField] private Image place;
        [SerializeField] private Sprite[] places;
        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private TextMeshProUGUI score;

        public void Init(int time, int score, int index)
        {
            int minutes = time / 60;
            int secs = time % 60;
            this.time.text = $"{minutes:00}:{secs:00}";
            this.score.text = score.ToString();
            
            place.sprite = places[index];
            place.SetNativeSize();
        }
    }
}