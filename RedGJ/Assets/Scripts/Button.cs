using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string sceneName, sceneName2;
    public float delay = 0.5f;

    public void playGame()
    {
        StartCoroutine(LoadSceneWithDelay(sceneName));
    }

    public void backToMenu()
    {
        StartCoroutine(LoadSceneWithDelay(sceneName2));
    }

    IEnumerator LoadSceneWithDelay(string scene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}