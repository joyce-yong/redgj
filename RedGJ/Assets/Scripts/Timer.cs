using UnityEngine;
using TMPro;
using System.Collections;
using static UnityEngine.Rendering.BoolParameter;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timerDuration = 120f;
    private float currentTime;

    public TextMeshProUGUI minusText;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI startCountdownText;

    public float fadeDuration = 0.5f;
    public float displayTime = 1.0f;
    private Coroutine fadeRoutine;

    private bool timerRunning = false;
    private bool gameEnded = false;

    public static bool gameStarted = false;
    public static Timer instance;

    private PotSpriteTrigger potTracker;
	
	public string sceneName;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentTime = timerDuration;
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        minusText.gameObject.SetActive(false);
        gameStarted = false;

        potTracker = FindObjectOfType<PotSpriteTrigger>();

        StartCoroutine(ShowStartCountdown());
    }

    IEnumerator ShowStartCountdown()
    {
        startCountdownText.gameObject.SetActive(true);

        startCountdownText.text = "Ready...";
        yield return new WaitForSeconds(2f);

        startCountdownText.text = "Start!";
        yield return new WaitForSeconds(1f);

        startCountdownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        timerRunning = true;

        gameStarted = true;
    }

    void Update()
    {
        if (timerRunning && !gameEnded)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Clamp(currentTime, 0, timerDuration);

            UpdateTimerText();

            if (currentTime <= 0)
            {
                // Check if player completed 3 meals
                bool win = potTracker != null && potTracker.GetCompletedMeals() >= 3;
                EndGame(win);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                EndGame(true);
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void DecreaseTime(float amount)
    {
        if (timerRunning && !gameEnded)
        {
            currentTime -= amount;
            currentTime = Mathf.Clamp(currentTime, 0, timerDuration);
            UpdateTimerText();

            if (amount >= 10f) 
            {
                ShowMinus10();
            }
        }
    }


    void ShowMinus10()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeMinus10());
    }

    IEnumerator FadeMinus10()
    {
        minusText.text = "-10";
        minusText.color = new Color(1f, 0f, 0f, 0f); // red and transparent
        minusText.gameObject.SetActive(true);

        // Fade In
        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = t / fadeDuration;
            minusText.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }
        minusText.color = new Color(1f, 0f, 0f, 1f);

        yield return new WaitForSeconds(displayTime);

        // Fade Out
        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = 1f - (t / fadeDuration);
            minusText.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }
        minusText.color = new Color(1f, 0f, 0f, 0f);
        minusText.gameObject.SetActive(false);
    }

    public void EndGame(bool win)
    {
        if (gameEnded) return;

        timerRunning = false;
        gameEnded = true;
        gameStarted = false;

        if (win)
        {
            //winText.gameObject.SetActive(true);
			SceneManager.LoadScene(sceneName);
        }
        else
        {
            loseText.gameObject.SetActive(true);
        }
    }
}
