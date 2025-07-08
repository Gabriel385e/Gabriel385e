using System;
using UnityEngine;

namespace Code
{
    public class StreakService
    {
        private static StreakService _instance;
        public static StreakService Instance => _instance ??= new StreakService();

        private int _streak;
        private DateTime _lastDate;

        public int Streak => _streak;

        private const string STREAK_KEY = "Streak";
        private const string LAST_DATE_KEY = "LastDate";
        private const int MAX_STREAK = 7;

        public void UpdateStreak()
        {
            DateTime today = DateTime.Now.Date;

            if (_lastDate == DateTime.MinValue)
            {
                _streak = 1;
            }
            else
            {
                int daysDiff = (today - _lastDate.Date).Days;

                if (daysDiff == 1)
                {
                    _streak++;
                    if (_streak > MAX_STREAK)
                        _streak = 1;
                }
                else if (daysDiff > 1)
                {
                    _streak = 1;
                }
            }

            _lastDate = today;
            Save();
        }
        
        public void Load()
        {
            _streak = PlayerPrefs.GetInt(STREAK_KEY, 0);
            string lastDateString = PlayerPrefs.GetString(LAST_DATE_KEY, "");
            _lastDate = string.IsNullOrEmpty(lastDateString) ? DateTime.MinValue : DateTime.Parse(lastDateString);
        }
        
        private void Save()
        {
            PlayerPrefs.SetInt(STREAK_KEY, _streak);
            PlayerPrefs.SetString(LAST_DATE_KEY, _lastDate.ToString());
            PlayerPrefs.Save();
        }
    }
}