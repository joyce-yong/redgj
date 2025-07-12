using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class skillButton : MonoBehaviour
{
    public GameObject tappySkill; // 👈 Assign this in the Inspector

    void Start()
    {
        if (tappySkill != null)
        {
            tappySkill.SetActive(false); // Make sure it's hidden at start
        }
    }

    public void trappyskillTrigger()
    {
        if (tappySkill != null)
        {
            tappySkill.SetActive(true); // Show the skill trigger when button is clicked
        }
    }
}