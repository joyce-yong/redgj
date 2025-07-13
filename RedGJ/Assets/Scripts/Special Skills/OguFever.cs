using UnityEngine;
using System.Collections;
using TMPro;

public class OguFeverManager : MonoBehaviour
{
    public static OguFeverManager instance;

    public float feverDuration = 8f;
    private bool isFeverActive = false;

    public GameObject feverSpritePrefab;
    private GameObject spawnedFeverSprite;


    [Header("UI")]
    public TextMeshProUGUI feverCountdownText; 
    public Vector3 countdownOffset = new Vector3(0, 2, 0); 

    [Header("Audio")]
    public AudioSource feverAudioSource; 

    [Header("Visual")]
    public Camera mainCamera; 
    public Color feverColor = Color.red;
    private Color originalColor;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (feverCountdownText != null)
        {
            feverCountdownText.gameObject.SetActive(false);
        }
    }
    public void ActivateFever()
    {
        if (isFeverActive) return;
        StartCoroutine(FeverRoutine());
    }

    IEnumerator FeverRoutine()
    {
        isFeverActive = true;
        Debug.Log("Ogu Fever Started!");

        if (feverSpritePrefab != null)
        {
            spawnedFeverSprite = Instantiate(feverSpritePrefab);
        }


        if (feverAudioSource != null)
        {
            feverAudioSource.Play();
        }

        if (mainCamera != null)
        {
            originalColor = mainCamera.backgroundColor;
            mainCamera.backgroundColor = feverColor;
        }

        if (feverCountdownText != null)
        {
            feverCountdownText.fontSize = 120;
            feverCountdownText.text = "OGU FEVER!!";
            feverCountdownText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        float countdown = feverDuration;

        while (countdown > 0)
        {
            int count = Mathf.CeilToInt(countdown);

            if (feverCountdownText != null)
            {
                feverCountdownText.text = count.ToString();
                feverCountdownText.fontSize = 200;

            }

            countdown -= Time.deltaTime;
            yield return null;
        }
        if (feverCountdownText != null)
        {
            feverCountdownText.fontSize = 120;
            feverCountdownText.text = "FEVER OVER!";
        }

        yield return new WaitForSeconds(1f);

        if (feverCountdownText != null)
        {
            feverCountdownText.gameObject.SetActive(false);
        }

        if (feverAudioSource != null)
        {
            feverAudioSource.Stop();
        }

        if (mainCamera != null)
        {
            mainCamera.backgroundColor = originalColor;
        }

        if (spawnedFeverSprite != null)
        {
            Destroy(spawnedFeverSprite);
        }


        isFeverActive = false;
        Debug.Log("Ogu Fever Ended.");
    }

    public bool IsFeverActive()
    {
        return isFeverActive;
    }
}
