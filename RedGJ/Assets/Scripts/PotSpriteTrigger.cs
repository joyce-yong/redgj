using UnityEngine;
using UnityEngine.UI;

public class PotSpriteTrigger : MonoBehaviour
{
    public Sprite coloredPotSprite;
    public Image[] potImages;
    public AudioClip mealCompleteSound;

    public UnityEngine.UI.Button tappySkillButton;
    public GameObject tappyLockSprite;

    public UnityEngine.UI.Button oguSkillButton;
    public GameObject oguLockSprite;

    private int completedMeals = 0;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (tappySkillButton != null)
            tappySkillButton.interactable = false;

        if (tappyLockSprite != null)
            tappyLockSprite.SetActive(true);

        if (oguSkillButton != null)
            oguSkillButton.interactable = false;

        if (oguLockSprite != null)
            oguLockSprite.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            if (completedMeals < potImages.Length)
            {
                potImages[completedMeals].sprite = coloredPotSprite;
                completedMeals++;


                if (completedMeals == 1 && tappySkillButton != null)
                {
                    tappySkillButton.interactable = true;
                    if (tappyLockSprite != null)
                        tappyLockSprite.SetActive(false);
                    Debug.Log("Tappy Skill Button is now enabled!");
                }

                if (completedMeals == 2 && oguSkillButton != null)
                {
                    oguSkillButton.interactable = true;
                    if (oguLockSprite != null)
                        oguLockSprite.SetActive(false);
                    Debug.Log("Ogu Skill Button is now enabled!");
                }

                if (mealCompleteSound != null && audioSource != null)
                    audioSource.PlayOneShot(mealCompleteSound);

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    foreach (Transform child in player.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }

                Food.ResetStack();

                if (completedMeals >= 3)
                {
                    Timer.instance.EndGame(true);
                }
            }

            Destroy(other.gameObject);
        }
    }

    public int GetCompletedMeals()
    {
        return completedMeals;
    }
}
