using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Button _btnCmp;
        [SerializeField] private string _scene;

        private void Awake()
        {
            _btnCmp.onClick.AddListener(() => SceneManager.LoadScene(_scene));
        }
    }
}