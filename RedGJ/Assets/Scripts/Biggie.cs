using UnityEngine;

public class Biggie : MonoBehaviour
{
    private static Transform playerTransform;

    public AudioClip spawnSound;
    public AudioClip biggieImpactSound;
    private AudioSource audioSource;

    void Start()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }

        audioSource = GetComponent<AudioSource>();

        if (spawnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }

        // Ignore floor collisions
        GameObject[] floorObjects = GameObject.FindGameObjectsWithTag("Floor");
        Collider2D myCollider = GetComponent<Collider2D>();

        foreach (GameObject floor in floorObjects)
        {
            Collider2D floorCollider = floor.GetComponent<Collider2D>();
            if (floorCollider != null && myCollider != null)
            {
                Physics2D.IgnoreCollision(myCollider, floorCollider);
            }
        }
    }


    void FixedUpdate()
    {
        if (Timer.gameStarted)
        {
            transform.position -= new Vector3(0f, 0.12f, 0f);

            if (transform.position.y < -6f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.transform == Food.GetTopOfStack())
        {
            if (biggieImpactSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(biggieImpactSound);
            }

            Food.RemoveTopStackedFoods(3);

            Destroy(gameObject, 1f); 
        }
    }

}
