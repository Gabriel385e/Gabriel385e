using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class ColorGuessGame : MonoBehaviour
    {
        [Header("UI")]
        public Image colorDisplay;
        public Button[] optionButtons;
        public TMP_Text[] optionTexts;
        public TMP_Text progressText;
        public Slider progressSlider;
        public TMP_Text timerText;
        public Button confirmButton;
        public EndGamePanelController winPanelController;
        public EndGamePanelController losePanelController;

        [Header("Selected Answer UI")]
        public TMP_Text selectedAnswerText;

        [Header("Sprites")] 
        public Sprite normalSprite;
        public Sprite selectedSprite;

        [Header("Icons")]
        public Image icon;
        public Sprite iconCorrect;
        public Sprite iconWrong;

        private string correctHex;
        private int score = 0;
        private int totalQuestions;
        private int currentQuestion = 0;
        private int wrongAnswers = 0;
        private float timeLeft = 15f;
        private bool isAnswerGiven = false;
        private Coroutine timerCoroutine;
        private int selectedIndex = -1;
        private float totalGameTime = 0f;
        private float questionStartTime = 0f;

        void Start()
        {
            totalQuestions = Random.Range(10, 21);
            progressSlider.maxValue = totalQuestions;
            
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(OnConfirmAnswer);
            
            GenerateNewRound();
        }

        void GenerateNewRound()
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }

            if (currentQuestion >= totalQuestions || wrongAnswers >= 3)
            {
                EndGame();
                return;
            }

            ResetButtons();
            isAnswerGiven = false;
            timeLeft = 15f;

            Color color = new Color(Random.value, Random.value, Random.value);
            colorDisplay.color = color;
            correctHex = ColorUtility.ToHtmlStringRGB(color);

            int correctIndex = Random.Range(0, optionButtons.Length);
            HashSet<string> usedHex = new HashSet<string> { correctHex };

            for (int i = 0; i < optionButtons.Length; i++)
            {
                string hex = (i == correctIndex) ? correctHex : GenerateUniqueHex(usedHex);
                optionTexts[i].text = "#" + hex;
                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
            }

            currentQuestion++;
            progressSlider.value = currentQuestion;
            progressText.text = $"{currentQuestion}/{totalQuestions}";
            selectedAnswerText.text = "";
            icon.enabled = false;

            questionStartTime = Time.time;
            timerCoroutine = StartCoroutine(TimerRoutine());
        }

        void ResetButtons()
        {
            foreach (Button btn in optionButtons)
            {
                btn.image.sprite = normalSprite;
            }
            selectedIndex = -1;
            selectedAnswerText.text = "";
        }

        string GenerateUniqueHex(HashSet<string> existing)
        {
            string hex;
            do
            {
                Color color = new Color(Random.value, Random.value, Random.value);
                hex = ColorUtility.ToHtmlStringRGB(color);
            } while (!existing.Add(hex));

            return hex;
        }

        IEnumerator TimerRoutine()
        {
            while (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;

                int seconds = Mathf.CeilToInt(timeLeft);
                int minutes = seconds / 60;
                int secs = seconds % 60;
                timerText.text = $"{minutes:00}:{secs:00}";

                yield return null;
            }

            if (!isAnswerGiven)
            {
                wrongAnswers++;
                ShowTimeout();
            }
        }

        void ShowTimeout()
        {
            isAnswerGiven = true;
            selectedAnswerText.text = "No answer";
            icon.sprite = iconWrong;
            icon.enabled = true;
            float timeSpent = Time.time - questionStartTime;
            totalGameTime += timeSpent;
            StartCoroutine(NextQuestionDelay());
        }

        void OnOptionSelected(int index)
        {
            if (isAnswerGiven) return;

            ResetButtons();
            selectedIndex = index;
            optionButtons[index].image.sprite = selectedSprite;
            selectedAnswerText.text = optionTexts[index].text;
        }
        
        public void OnConfirmAnswer()
        {
            if (isAnswerGiven || selectedIndex == -1) return;

            isAnswerGiven = true;
            
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
            
            float timeSpent = Time.time - questionStartTime;
            totalGameTime += timeSpent;

            string selectedHex = optionTexts[selectedIndex].text;
            bool isCorrect = selectedHex == "#" + correctHex;

            icon.sprite = isCorrect ? iconCorrect : iconWrong;
            icon.enabled = true;

            if (isCorrect)
            {
                score++;
                PlayMusic.Instance.PlayCorrect();
            }
            else
            {
                wrongAnswers++;
                PlayMusic.Instance.PlayIncorrect();
            }

            StartCoroutine(NextQuestionDelay());
        }

        IEnumerator NextQuestionDelay()
        {
            yield return new WaitForSeconds(1f);
            GenerateNewRound();
        }

        void EndGame()
        {
            bool isWin = false;

            if (wrongAnswers >= 3)
            {
                losePanelController.Show(score, totalGameTime, totalQuestions);
                PlayMusic.Instance.PlayLose();
            }
            else
            {
                isWin = true;
                WasInGame.Instance.Win = true;
                winPanelController.Show(score, totalGameTime, totalQuestions);
                PlayMusic.Instance.PlayWin();
            }

            AllGameStatistics.Instance.RecordMatch(isWin, (int)totalGameTime, score);
        }
    }
}