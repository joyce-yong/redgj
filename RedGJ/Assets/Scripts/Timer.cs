using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float timerDuration = 90f;
    private float currentTime;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI startCountdownText;

    private bool timerRunning = false;
    private bool gameEnded = false;

    public static bool gameStarted = false;
    public static Timer instance;

    private PotSpriteTrigger potTracker;

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
        }
    }


    public void EndGame(bool win)
    {
        if (gameEnded) return;

        timerRunning = false;
        gameEnded = true;
        gameStarted = false;

        if (win)
        {
            winText.gameObject.SetActive(true);
        }
        else
        {
            loseText.gameObject.SetActive(true);
        }
    }
}
