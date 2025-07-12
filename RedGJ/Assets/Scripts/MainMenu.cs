using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public string sceneName;
    public AudioSource audioSource;
    public float delayBeforeLoad = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        StartCoroutine(PlaySoundAndLoadScene());

    }
    IEnumerator PlaySoundAndLoadScene()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        StartCoroutine(PlaySoundAndQuit());

        IEnumerator PlaySoundAndQuit()
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);
            }

            Application.Quit();

            
        }
    }
}
