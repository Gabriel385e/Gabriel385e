using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code
{
    public class EnterGameButton : MonoBehaviour
    {
        [SerializeField] private Button _btnCmp;
        [SerializeField] private string _scene;

        private void Awake()
        {
            _btnCmp.onClick.AddListener(() =>
            {
                WasInGame.Instance.Win = false;
                SceneManager.LoadScene(_scene);
            });
        }
    }
}