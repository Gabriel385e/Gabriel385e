using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UserData
{
    public class ShowUserData : MonoBehaviour
    {
        public TextMeshProUGUI userNameText;
        public Image avatarImage;
        public Sprite[] avatarSprites;

        private void Start()
        {
            UpdateUI();
            UserDataHolder.Instance.OnUserDataChanged += UpdateUI;
        }

        private void OnDestroy()
        {
            UserDataHolder.Instance.OnUserDataChanged -= UpdateUI;
        }

        private void UpdateUI()
        {
            string name = UserDataHolder.Instance.UserName;
            int avatarIndex = UserDataHolder.Instance.AvatarIndex;

            userNameText.text = string.IsNullOrEmpty(name) ? "Lilo" : name;

            if (avatarIndex >= 0 && avatarIndex < avatarSprites.Length)
            {
                avatarImage.sprite = avatarSprites[avatarIndex];
            }
            else
            {
                Debug.LogWarning("Invalid avatar index.");
            }
        }
    }
}