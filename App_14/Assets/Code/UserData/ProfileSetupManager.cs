using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UserData
{
    public class ProfileSetupManager : MonoBehaviour
    {
        public GameObject window;
        public TMP_InputField nameInputField;
        public Button getStartedButton;
        public Button avatarEditButton;
        public GameObject avatarPopup;
        public Image selectedAvatarDisplay;
        public Sprite[] avatarSprites;

        private int selectedAvatarIndex;

        public void SelectAvatar(int index)
        {
            selectedAvatarIndex = index;
            selectedAvatarDisplay.sprite = avatarSprites[index];
            avatarPopup.SetActive(false);
        }

        private void Start()
        {
            if (UserDataHolder.Instance.IsUserDataSet())
                Destroy(gameObject);
            else
                window.SetActive(true);
            
            avatarPopup.SetActive(false);
            getStartedButton.interactable = false;

            avatarEditButton.onClick.AddListener(OpenAvatarPopup);
            getStartedButton.onClick.AddListener(SubmitUserData);
            nameInputField.onValueChanged.AddListener(OnNameChanged);
        }

        private void OnNameChanged(string value)
        {
            bool isValid = !string.IsNullOrWhiteSpace(value);
            getStartedButton.interactable = isValid;
        }

        private void OpenAvatarPopup()
        {
            avatarPopup.SetActive(true);
        }
        
        private void SubmitUserData()
        {
            string userName = nameInputField.text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                getStartedButton.interactable = false;
                return;
            }

            UserDataHolder.Instance.SetUserData(userName, selectedAvatarIndex);
            Destroy(gameObject);
        }
    }
}