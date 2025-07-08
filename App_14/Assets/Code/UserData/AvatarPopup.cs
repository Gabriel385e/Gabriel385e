using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UserData
{
    public class AvatarPopup : MonoBehaviour
    {
        public Button[] avatarButtons;
        public Button choose;
        public ProfileSetupManager profileSetupManager;

        private int index;
        
        private void Start()
        {
            for (int i = 0; i < avatarButtons.Length; i++)
            {
                int index = i;
                avatarButtons[i].onClick.AddListener(() => OnAvatarClicked(index));
            }
            
            choose.onClick.AddListener(Choose);
        }

        private void OnDestroy()
        {
            choose.onClick.RemoveListener(Choose);
        }

        private void OnAvatarClicked(int index)
        {
            this.index = index;
            HighlightSelectedAvatar(index);
        }

        private void HighlightSelectedAvatar(int index)
        {
            for (int i = 0; i < avatarButtons.Length; i++)
            {
                avatarButtons[i].image.color = (i == index) ? Color.gray : Color.white;
            }
        }

        private void Choose()
        {
            profileSetupManager.SelectAvatar(index);
        }
    }
}