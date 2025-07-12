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

            bool inFever = OguFeverManager.instance != null && OguFeverManager.instance.IsFeverActive();

            if (!inFever && Random.value < biggieSpawnChance)
            {
                // Safety check for biggie prefab
                if (biggiePrefab == null)
                {
                    Debug.LogWarning("Biggie prefab is not assigned!");
                    return;
                }

                spawned = Instantiate(biggiePrefab, new Vector3(pos_x, 3.0f, 0.1f), Quaternion.identity);
            }
            else
            {
                // SAFETY CHECK for food prefab array
                if (gm.Length == 0)
                {
                    Debug.LogWarning("No food prefabs assigned to gm array!");
                    return;
                }

                int index = Random.Range(0, gm.Length);
                if (gm[index] == null)
                {
                    Debug.LogWarning("Food prefab at index " + index + " is null!");
                    return;
                }

                spawned = Instantiate(gm[index], new Vector3(pos_x, 3.0f, 0.1f), Quaternion.identity);
            }

            // Assign gravity
            Rigidbody2D rb = spawned.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0.005f;
            }

            // Spawn rate
            timer = inFever ? 0.3f : 2.0f;
        }
    }


}
