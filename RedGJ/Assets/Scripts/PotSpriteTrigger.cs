using UnityEngine;
using UnityEngine.UI;

public class PotSpriteTrigger : MonoBehaviour
{
    public Sprite coloredPotSprite;          // The colored version of the pot
    public Image[] potImages;                // All black pot images (3 for this level)

    private int completedMeals = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            if (completedMeals < potImages.Length)
            {
                // Change the current pot to the colored version
                potImages[completedMeals].sprite = coloredPotSprite;
                completedMeals++;

                Debug.Log("Meal " + completedMeals + " completed!");
            }

            Destroy(other.gameObject);
        }
    }
}
