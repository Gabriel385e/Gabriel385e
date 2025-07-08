using UnityEngine;

namespace Code
{
    public class WasInGame : MonoBehaviour
    {
        public static WasInGame Instance { get; private set; }
        public bool Win { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}