using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class EndGamePanelController : MonoBehaviour
    {
        public TMP_Text bestTimeText;

        public Slider progressSlider;
        public TMP_Text progressText;

        public void Show(int score, float bestTime, int total)
        {
            bestTimeText.text = FormatTime(bestTime);
            progressSlider.maxValue = total;
            progressSlider.value = score;
            progressText.text = $"{score}/{total}";
            gameObject.SetActive(true);
        }
        
        private string FormatTime(float totalSeconds)
        {
            int minutes = Mathf.FloorToInt(totalSeconds / 60f);
            int seconds = Mathf.FloorToInt(totalSeconds % 60f);
            return $"{minutes:00}:{seconds:00}";
        }
    }
}