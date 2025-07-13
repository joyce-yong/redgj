using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    public GameObject biggiePrefab;
    [Range(0f, 1f)] public float biggieSpawnChance = 0.2f;

    public GameObject[] gm;
    private float timer = 1f;
    private float elapsedGameTime = 0f;

    void Update()
    {
        if (!Timer.gameStarted || GameState.IsPausedBySkillTrigger)
            return;

        elapsedGameTime += Time.deltaTime;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            float pos_x = Random.Range(-0.9f, 2f);
            GameObject spawned;

            bool inFever = OguFeverManager.instance != null && OguFeverManager.instance.IsFeverActive();
            bool canSpawnBiggie = !inFever && (elapsedGameTime >= 10f) && Random.value < biggieSpawnChance;

            if (canSpawnBiggie)
            {
                if (biggiePrefab == null)
                {
                    Debug.LogWarning("Biggie prefab is not assigned!");
                    return;
                }

                spawned = Instantiate(biggiePrefab, new Vector3(pos_x, 3.0f, 0.1f), Quaternion.identity);
            }
            else
            {
                if (gm == null || gm.Length == 0)
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


            Rigidbody2D rb = spawned.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0.005f;
            }


            timer = inFever ? 0.3f : 2.0f;
        }
    }
}
