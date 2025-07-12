using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public string sceneName, sceneName2;

    void Start()
    {
		
    }
    void Update()
    {
		
    }
    public void playGame()
    {
        SceneManager.LoadScene(sceneName);
    }
	public void backToMenu()
    {
        SceneManager.LoadScene(sceneName2);
    }
}
