using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    public GameObject biggiePrefab;
    [Range(0f, 1f)] public float biggieSpawnChance = 0.2f; 

    public GameObject[] gm;
    private float timer = 1f;

    void Update()
    {
        if (!Timer.gameStarted) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            float pos_x = Random.Range(-0.9f, 2.5f);
            GameObject spawned;

            if (Random.value < biggieSpawnChance)
            {
                // Biggie
                spawned = Instantiate(biggiePrefab, new Vector3(pos_x, 3.0f, 0.1f), Quaternion.identity);
            }
            else
            {
                // food
                int index = Random.Range(0, gm.Length);
                spawned = Instantiate(gm[index], new Vector3(pos_x, 3.0f, 0.1f), Quaternion.identity);
            }

            Rigidbody2D rb = spawned.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0.005f;
            }

            timer = 2.0f;
        }

    }
}
