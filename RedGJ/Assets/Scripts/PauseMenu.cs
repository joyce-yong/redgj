using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]GameObject pauseMenu;

    public void Pause() 
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        Time.timeScale = 1;
		StartCoroutine(DelayedLoadScene("MainMenu"));
    }

    public void Resume()
    {
		StartCoroutine(ResumeWithDelay());
    }

    public void Retry()
    {
		Time.timeScale = 1;
        StartCoroutine(DelayedLoadScene(SceneManager.GetActiveScene().name));
    }
	
	private IEnumerator DelayedLoadScene(string sceneName)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(sceneName);
    }
	
	private IEnumerator ResumeWithDelay()
	{
		yield return new WaitForSecondsRealtime(0.3f);
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
}
