using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class AchievementService : MonoBehaviour
    {
        public static AchievementService Instance { get; private set; }
        public List<AchievementData> allAchievements;
        private Dictionary<int, AchievementData> achievementsDict = new();
        private const string PlayerPrefsKey = "AchievementsData";
        public event Action<AchievementData> OnAchievementUnlocked;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            achievementsDict = allAchievements.ToDictionary(a => a.Id, a => a);

            LoadAchievements();
        }

        public List<AchievementData> GetAllAchievements() => achievementsDict.Values.ToList();

        public bool Unlock(int achievementId)
        {
            if (achievementsDict.TryGetValue(achievementId, out var ach) && !ach.IsUnlocked)
            {
                ach.IsUnlocked = true;
                SaveAchievements();
                OnAchievementUnlocked?.Invoke(ach);
                return true;
            }

            return false;
        }
        
        public AchievementData GetAchievementById(int id)
        {
            return achievementsDict.TryGetValue(id, out AchievementData ach) ? ach : null;
        }

        private void SaveAchievements()
        {
            var list = achievementsDict.Values
                .Select(a => new AchievementSaveData { Id = a.Id, IsUnlocked = a.IsUnlocked }).ToList();
            var wrapper = new AchievementSaveDataWrapper(list);
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(PlayerPrefsKey, json);
            PlayerPrefs.Save();
        }

        private void LoadAchievements()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsKey)) return;
            var json = PlayerPrefs.GetString(PlayerPrefsKey);
            var wrapper = JsonUtility.FromJson<AchievementSaveDataWrapper>(json);
            if (wrapper?.Achievements != null)
            {
                foreach (var save in wrapper.Achievements)
                {
                    if (achievementsDict.TryGetValue(save.Id, out var ach))
                        ach.IsUnlocked = save.IsUnlocked;
                }
            }
        }
    }

    [Serializable]
    public class AchievementSaveDataWrapper
    {
        public List<AchievementSaveData> Achievements;
        public AchievementSaveDataWrapper(List<AchievementSaveData> achs) => Achievements = achs;
    }

    [Serializable]
    public class AchievementSaveData
    {
        public int Id;
        public bool IsUnlocked;
    }

    [Serializable]
    public class AchievementData
    {
        public int Id;
        public Sprite Icon;
        public bool IsUnlocked;
    }
}