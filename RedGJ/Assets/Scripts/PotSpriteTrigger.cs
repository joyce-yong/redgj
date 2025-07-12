using UnityEngine;
using UnityEngine.UI;

public class PotSpriteTrigger : MonoBehaviour
{
    public Sprite coloredPotSprite;
    public Image[] potImages;
	public AudioClip mealCompleteSound;

    private int completedMeals = 0;
	private AudioSource audioSource;
	
	void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            if (completedMeals < potImages.Length)
            {
                potImages[completedMeals].sprite = coloredPotSprite;
                completedMeals++;

                Debug.Log("Meal " + completedMeals + " completed!");
				
				if (mealCompleteSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(mealCompleteSound);
                }

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
