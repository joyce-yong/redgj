using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class skillButton : MonoBehaviour
{
    public GameObject tappySkill;

    void Start()
    {
        if (tappySkill != null)
        {
            tappySkill.SetActive(false);
        }
    }

    public void trappyskillTrigger()
    {
        if (tappySkill != null)
        {
            tappySkill.SetActive(true);
			StartCoroutine(PauseDuringSkill());
        }
    }
	
	IEnumerator PauseDuringSkill()
	{
		GameState.IsPausedBySkillTrigger = true;

		yield return new WaitForSeconds(5f);

		GameState.IsPausedBySkillTrigger = false;
	}
}