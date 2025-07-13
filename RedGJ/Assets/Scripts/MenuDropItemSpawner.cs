using UnityEngine;

public class MenuDropItemSpawner : MonoBehaviour
{
    [Header("Drop Settings")]
    public GameObject[] itemPrefabs;     
    public float spawnInterval = 0.03f;   
    public float xRange = 8f;           
    public float spawnY = 10f;   

	[Header("Stop Spawning")]
	public float stopYThreshold = -70f;
	private bool stopSpawning = false;
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnItem), 0f, spawnInterval);
        
    }
    void SpawnItem()
    {
        if (itemPrefabs.Length == 0) return;

        float xPos = Random.Range(-xRange, xRange);
        Vector3 spawnPos = new Vector3(xPos, spawnY, 0f);

        int index = Random.Range(0, itemPrefabs.Length);
        GameObject newItem = Instantiate(itemPrefabs[index], spawnPos, Quaternion.identity);
		
		DropItemChecker checker = newItem.AddComponent<DropItemChecker>();
        checker.spawner = this;
    }
    

    public void StopSpawning()
    {
        stopSpawning = true;
        CancelInvoke(nameof(SpawnItem));
    }
}
