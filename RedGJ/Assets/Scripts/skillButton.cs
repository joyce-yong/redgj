using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class skillButton : MonoBehaviour
{
    public GameObject tappySkill, oguSkill;

    void Start()
    {
        if (tappySkill != null)
        {
            tappySkill.SetActive(false);
        }
		if (oguSkill != null)
        {
            oguSkill.SetActive(false);
        }
    }

    public void trappyskillTrigger()
    {
        if (tappySkill != null)
        {
            tappySkill.SetActive(true);
			StartCoroutine(PauseDuringSkill("tappy"));
        }
    }
	
	public void oguskillTrigger ()
	{
		if (oguSkill != null)
        {
            oguSkill.SetActive(true);
			StartCoroutine(PauseDuringSkill("ogu"));
        }
	}
	
	IEnumerator PauseDuringSkill(string skillType)
	{
		GameState.IsPausedBySkillTrigger = true;

		yield return new WaitForSeconds(5f);

		GameState.IsPausedBySkillTrigger = false;
		
		if (skillType == "tappy" && TappyFreezeManager.instance != null)
        {
            TappyFreezeManager.instance.ActivateFreeze();
        }
        else if (skillType == "ogu" && OguFeverManager.instance != null)
        {
            OguFeverManager.instance.ActivateFever();
        }
    }
}