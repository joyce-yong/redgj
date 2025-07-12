using UnityEngine;

public class MenuDropItemSpawner : MonoBehaviour
{
    [Header("Drop Settings")]
    public GameObject[] itemPrefabs;     
    public float spawnInterval = 0.1f;   
    public float xRange = 8f;           
    public float spawnY = 10f;           

        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        Instantiate(itemPrefabs[index], spawnPos, Quaternion.identity);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
