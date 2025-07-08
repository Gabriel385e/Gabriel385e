using UnityEngine;

namespace Code
{
    public class PlayMusic : MonoBehaviour
    {
        public static PlayMusic Instance { get; private set; }

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _sfxSource;

        [Header("SFX Clips")]
        public AudioClip correctClip;
        public AudioClip incorrectClip;
        public AudioClip winClip;
        public AudioClip loseClip;
        public AudioClip achievementClip;

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

        public void PlayCorrect()
        {
            PlaySFX(correctClip);
        }

        public void PlayIncorrect()
        {
            PlaySFX(incorrectClip);
        }

        public void PlayWin()
        {
            PlaySFX(winClip);
        }

        public void PlayLose()
        {
            PlaySFX(loseClip);
        }

        public void PlayAchievement()
        {
            PlaySFX(achievementClip);
        }

        private void PlaySFX(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
        }
    }
}