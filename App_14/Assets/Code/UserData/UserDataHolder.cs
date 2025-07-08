using System;
using UnityEngine;

namespace Code.UserData
{
    public class UserDataHolder
    {
        private static UserDataHolder _instance;
        public static UserDataHolder Instance => _instance ?? (_instance = new UserDataHolder());
        
        public event Action OnUserDataChanged;
        
        public string UserName { get; private set; }
        public int AvatarIndex { get; private set; }

        private const string UserNameKey = "UserName";
        private const string AvatarIndexKey = "AvatarIndex";

        private UserDataHolder()
        {
            LoadUserData();
        }

        public void SetUserData(string name, int avatarIndex)
        {
            UserName = name;
            AvatarIndex = avatarIndex;
            SaveUserData();
            OnUserDataChanged?.Invoke();
        }

        public bool IsUserDataSet()
        {
            return !string.IsNullOrEmpty(UserName);
        }

        private void SaveUserData()
        {
            PlayerPrefs.SetString(UserNameKey, UserName);
            PlayerPrefs.SetInt(AvatarIndexKey, AvatarIndex);
            PlayerPrefs.Save();
        }

        private void LoadUserData()
        {
            UserName = PlayerPrefs.GetString(UserNameKey, "");
            AvatarIndex = PlayerPrefs.GetInt(AvatarIndexKey, 0);
        }
    }
}