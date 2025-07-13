using UnityEngine;

public class DropItemChecker : MonoBehaviour
{
    public MenuDropItemSpawner spawner;

    void Update()
    {
        if (transform.position.y < -70f)
        {
            if (spawner != null)
            {
                spawner.StopSpawning();
            }
            else
            {
                Debug.LogWarning("Spawner not assigned on " + gameObject.name);
            }
        }
    }
}
