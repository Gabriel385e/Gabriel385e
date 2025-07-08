using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class TabBar : MonoBehaviour
    {
        [System.Serializable]
        public class Tab
        {
            public Button button;
            public GameObject page;
            public Sprite activeSprite;
            public Sprite inactiveSprite;
        }

        public Tab[] tabs;

        private void Start()
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                int index = i;
                tabs[i].button.onClick.AddListener(() => OnTabClicked(index));
            }

            OnTabClicked(0);
        }

        private void OnTabClicked(int index)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                bool isActive = i == index;
                tabs[i].page.SetActive(isActive);
                tabs[i].button.image.sprite = isActive ? tabs[i].activeSprite : tabs[i].inactiveSprite;
                tabs[i].button.image.SetNativeSize();
            }
        }
    }
}