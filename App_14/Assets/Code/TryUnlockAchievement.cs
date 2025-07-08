using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class TryUnlockAchievement : MonoBehaviour
    {
        [SerializeField] private AchievementWindow window;
        
        private void Start()
        {
            UnlockAchievement();
        }

        private void UnlockAchievement()
        {
            if (WasInGame.Instance.Win == false)
                return;
            
            // float roll = Random.value;
            
            // if (roll > 0.2f)
                // return;

            List<AchievementData> locked = AchievementService.Instance.GetAllAchievements()
                .Where(a => !a.IsUnlocked)
                .ToList();

            if (locked.Count == 0)
                return;

            int idx = Random.Range(0, locked.Count);
            AchievementData achievementToUnlock = locked[idx];

            AchievementService.Instance.Unlock(achievementToUnlock.Id);
            window.SetIcon(AchievementService.Instance.GetAchievementById(achievementToUnlock.Id).Icon);
            window.gameObject.SetActive(true);
        }
    }
}