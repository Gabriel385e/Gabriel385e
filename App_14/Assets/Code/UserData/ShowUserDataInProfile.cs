using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UserData
{
    public class ShowUserDataInProfile : MonoBehaviour
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
            var user = string.IsNullOrEmpty(name) ? "Lilo" : name;

            string fullText = $"Hello, <b>{user}</b>";
            userNameText.text = fullText;

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