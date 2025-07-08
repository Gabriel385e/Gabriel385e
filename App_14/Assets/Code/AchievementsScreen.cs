using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class AchievementsScreen : MonoBehaviour
    {
        [SerializeField] private List<Image> achievements;
        [SerializeField] private Sprite locked;
        
        void OnEnable()
        {
            List<AchievementData> myAchievements = AchievementService.Instance.GetAllAchievements();
            
            for (int i = 0; i < achievements.Count; i++)
            {
                achievements[i].sprite = myAchievements[i].IsUnlocked ? myAchievements[i].Icon : locked;
                achievements[i].SetNativeSize();
            }
        }
    }
}