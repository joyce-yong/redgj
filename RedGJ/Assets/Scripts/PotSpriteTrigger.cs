using UnityEngine;
using UnityEngine.UI;

public class PotSpriteTrigger : MonoBehaviour
{
    public Sprite coloredPotSprite;
    public Image potImage;

    private bool isMealCompleted = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isMealCompleted && other.CompareTag("Food"))
        {
            Debug.Log("Food delivered to table!");
            potImage.sprite = coloredPotSprite;
            isMealCompleted = true;

            
            Destroy(other.gameObject);
        }
    }
}
