using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class StreakUI : MonoBehaviour
    {
        private const string Day3ShownKey = "Day3StreakShownDate";
        
        public Image[] streakStars;
        public Sprite openedStar;
        public Sprite closedStar;
        public TextMeshProUGUI streakText;
        public GameObject day3Streak;

        private void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            StreakService service = StreakService.Instance;
            streakText.text = $"{service.Streak} day streak";

            for (int i = 0; i < streakStars.Length; i++)
            {
                streakStars[i].sprite = i < service.Streak ? openedStar : closedStar;
                streakStars[i].SetNativeSize();
            }

            if (service.Streak == 3 && !WasDay3PopupShownToday())
            {
                day3Streak.gameObject.SetActive(true);
                SaveDay3PopupShown();
            }
        }

        private bool WasDay3PopupShownToday()
        {
            string savedDate = PlayerPrefs.GetString(Day3ShownKey, "");
            string today = DateTime.Now.ToString("yyyyMMdd");
            return savedDate == today;
        }

        private void SaveDay3PopupShown()
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            PlayerPrefs.SetString(Day3ShownKey, today);
            PlayerPrefs.Save();
        }
    }
}