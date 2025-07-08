using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class AchievementWindow : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
            _icon.SetNativeSize();
            PlayMusic.Instance.PlayAchievement();
        }
    }
}