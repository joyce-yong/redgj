using UnityEngine;
using UnityEngine.UI;

public class PotSpriteTrigger : MonoBehaviour
{
    public Sprite coloredPotSprite;
    public Image[] potImages;

    private int completedMeals = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            if (completedMeals < potImages.Length)
            {
                potImages[completedMeals].sprite = coloredPotSprite;
                completedMeals++;

                Debug.Log("Meal " + completedMeals + " completed!");

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    foreach (Transform child in player.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }

                Food.ResetStack();
            }

            Destroy(other.gameObject);
        }
    }
}
