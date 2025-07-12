using UnityEngine;
using System.Collections;
using TMPro; 

public class TappyFreezeManager : MonoBehaviour
{
    public float freezeDuration = 10f;
    private bool isFreezing = false;

    private Coroutine freezeRoutine;
    public static TappyFreezeManager instance;

    [Header("Visual Effect")]
    public GameObject iceCubePrefab;
    private GameObject spawnedIceCube;
    public Vector3 iceCubeSpawnPosition = new Vector3(0, 1, 0);

    [Header("Countdown UI")]
    public TextMeshProUGUI freezeCountdownText; 

    [Header("Audio")]
    public AudioSource freezeAudioSource;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (freezeCountdownText != null)
        {
            freezeCountdownText.gameObject.SetActive(false);
        }
    }

    public void ActivateFreeze()
    {
        if (isFreezing) return;

        if (freezeRoutine != null)
            StopCoroutine(freezeRoutine);

        freezeRoutine = StartCoroutine(FreezeTimeCoroutine());
    }

    IEnumerator FreezeTimeCoroutine()
    {
        isFreezing = true;
        Timer.instance.SetTimeFrozen(true);

        // Spawn Ice Cube Visual
        if (iceCubePrefab != null)
        {
            spawnedIceCube = Instantiate(iceCubePrefab, iceCubeSpawnPosition, Quaternion.identity);
        }

        // Start Sound
        if (freezeAudioSource != null)
        {
            freezeAudioSource.Play();
        }

        // Show countdown text
        if (freezeCountdownText != null)
        {
            freezeCountdownText.gameObject.SetActive(true);
            freezeCountdownText.fontSize = 120;
            freezeCountdownText.text = "TappyFreeze!";
        }

        yield return new WaitForSeconds(1f); // Show "TappyFreeze!" for 1 second

        float countdown = freezeDuration;

        while (countdown > 0)
        {
            int displayCount = Mathf.CeilToInt(countdown);

            if (freezeCountdownText != null)
            {
                freezeCountdownText.fontSize = 200;
                freezeCountdownText.text = displayCount.ToString();

            }

            countdown -= Time.deltaTime;
            yield return null;
        }

        // Show "FREEZE OVER!"
        if (freezeCountdownText != null)
        {
            freezeCountdownText.fontSize = 120;
            freezeCountdownText.text = "FREEZE OVER!";
        }

        if (freezeAudioSource != null)
        {
            freezeAudioSource.Stop();
        }

        if (spawnedIceCube != null)
            Destroy(spawnedIceCube, 1f);

        yield return new WaitForSeconds(1f);

        if (freezeCountdownText != null)
            freezeCountdownText.gameObject.SetActive(false);

        Timer.instance.SetTimeFrozen(false);
        isFreezing = false;

        Debug.Log("Tappy Freeze Ended.");
    }



    public bool IsTimeFrozen()
    {
        return isFreezing;
    }
}
