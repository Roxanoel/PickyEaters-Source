using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] GameObject gameWonPopup;
    [SerializeField] TextMeshProUGUI attemptsTakenText;

    [SerializeField] int baseAttempts = 0;
    int currentAttempts = 0;

    TextMeshProUGUI attemptsDisplayText;

    private void Awake()
    {
        attemptsDisplayText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        gameManager.onAttemptFailed += UpdateAttemptsAndDisplay;
        gameManager.onAttemptSuccessful += WinGame;
    }

    private void OnDisable()
    {
        gameManager.onAttemptFailed -= UpdateAttemptsAndDisplay;
        gameManager.onAttemptSuccessful -= WinGame;
    }

    private void Start()
    {
        currentAttempts = baseAttempts;
    }

    void UpdateAttemptsAndDisplay()
    {
        currentAttempts ++;
        attemptsDisplayText.text = $"Attempts: {currentAttempts}";
        /*if (currentLives <= 0)
        {
            GameOver();
            return;
        }*/
    }

    private void GameOver()
    {
        StartCoroutine(WaitForSecondsBeforeGameOver());
    }

    private void WinGame()
    {
        gameWonPopup.SetActive(true);
        UpdateAttemptsAndDisplay();
        attemptsTakenText.text = $"It only took you {currentAttempts} attempts!";
    }

    private IEnumerator WaitForSecondsBeforeGameOver()
    {
        yield return new WaitForSeconds(4.5f);
        gameOverPopup.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
