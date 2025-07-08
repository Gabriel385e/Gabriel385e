using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class WindowOpener : MonoBehaviour
    {
        [SerializeField] private Button _btnCmp;
        [SerializeField] private GameObject _wnd;

        private void Awake()
        {
            _btnCmp.onClick.AddListener(() => _wnd.gameObject.SetActive(true));
        }
    }
}