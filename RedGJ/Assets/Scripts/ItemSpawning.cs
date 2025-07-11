using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    public GameObject[] gm; 
    private float timer = 1f;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {

            float pos_x = Random.Range(-3.0f, 3.0f);
            int index = Random.Range(0, gm.Length); 

            // Spawn object
            GameObject spawned = Instantiate(gm[index], new Vector3(pos_x, 6.0f, 0.1f), Quaternion.identity);

            Rigidbody2D rb = spawned.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0.01f; 
            }

            timer = 1.0f;
        }
    }
}
