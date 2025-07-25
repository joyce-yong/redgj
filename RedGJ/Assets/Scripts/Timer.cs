using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{	
    public float timerDuration = 120f;
    private float currentTime;

    public TextMeshProUGUI minusText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI startCountdownText;

    public float fadeDuration = 0.5f;
    public float displayTime = 1.0f;
    private Coroutine fadeRoutine;

    private bool timerRunning = false;
    private bool gameEnded = false;

    public static bool gameStarted = false;
    public static Timer instance;

    private PotSpriteTrigger potTracker;

    public GameObject tutorialPanel;

    public string sceneName, sceneName2;

    private bool isTimeFrozenExternally = false;

    public TextMeshProUGUI tutorialCountdownText;

    public AudioClip minusTenSound; // Assign this in the Inspector
    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        currentTime = timerDuration;
        timerText.gameObject.SetActive(false);
        minusText.gameObject.SetActive(false);
        gameStarted = false;

        potTracker = FindObjectOfType<PotSpriteTrigger>();

        StartCoroutine(StartWithTutorial());

        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator StartWithTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }

        if (tutorialCountdownText != null)
        {
            tutorialCountdownText.gameObject.SetActive(true);

            for (int i = 10; i > 0; i--)
            {
                tutorialCountdownText.text = "Game starts in: " + i;
                yield return new WaitForSeconds(1f);
            }

            tutorialCountdownText.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(10f);
        }

        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }

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
		if (timerRunning && !gameEnded && !GameState.IsPausedBySkillTrigger && !isTimeFrozenExternally)
		{
			currentTime -= Time.deltaTime;
			currentTime = Mathf.Clamp(currentTime, 0, timerDuration);
			UpdateTimerText();

			if (currentTime <= 0)
			{
				bool win = potTracker != null && potTracker.GetCompletedMeals() >= 3;
				EndGame(win);
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
        if (timerRunning && !gameEnded && !isTimeFrozenExternally)
        {
            currentTime -= amount;
            currentTime = Mathf.Clamp(currentTime, 0, timerDuration);
            UpdateTimerText();

            if (amount >= 10f)
            {
                ShowMinus10();
                if (audioSource != null && minusTenSound != null)
                {
                    audioSource.PlayOneShot(minusTenSound);
                }
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

        StartCoroutine(DelayedSceneLoad(win));
    }


    IEnumerator DelayedSceneLoad(bool win)
    {
        yield return new WaitForSeconds(2f);

        if (win)
        {
            
            
            SceneManager.LoadScene(sceneName);

        }
        else
        {
            
            SceneManager.LoadScene(sceneName2);
        }
    }

    public void SetTimeFrozen(bool freeze)
    {
        isTimeFrozenExternally = freeze;
    }
}
