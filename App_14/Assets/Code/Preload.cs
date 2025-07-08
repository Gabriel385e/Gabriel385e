using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class Preload : MonoBehaviour
    {
        [SerializeField] private GameObject _icon;
        
        private void Start()
        {
            Pulsation();
            StartCoroutine(Enumerate());
        }

        private IEnumerator Enumerate()
        {
            Application.targetFrameRate = 100;
            AllGameStatistics.Instance.LoadStatistics();
            StreakService.Instance.Load();
            StreakService.Instance.UpdateStreak();
            
            yield return new WaitForSeconds(2f);
            
            SceneManager.LoadScene("Main");
        }

        private void Pulsation()
        {
            _icon.transform
                .DOScale(1.1f, 0.55f)
                .SetLoops(-2, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }
}