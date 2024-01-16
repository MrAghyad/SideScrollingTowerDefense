using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    
    public GameObject[] objects; // Assign the prefab you want to spawn in the inspector
    public Transform spawnAreaStart;
    public Transform spawnAreaEnd;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(8f, 16f)); // Wait for 8 to 16 seconds


            float randomX = Random.Range(spawnAreaStart.position.x, spawnAreaEnd.position.x); // Get a random x value between x1 and x2
            Vector3 spawnPosition = new Vector3(randomX, spawnAreaStart.position.y, 0); // Set the spawn position

            Instantiate(objects[Random.Range(0, 2)], spawnPosition, Quaternion.identity); // Spawn the object
        }
    }
}