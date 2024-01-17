using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    
    public GameObject[] objects; 
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
            yield return new WaitForSeconds(Random.Range(5f, 10f)); 


            float randomX = Random.Range(spawnAreaStart.position.x, spawnAreaEnd.position.x); 
            Vector3 spawnPosition = new Vector3(randomX, spawnAreaStart.position.y, 0); 

            Instantiate(objects[Random.Range(0, 2)], spawnPosition, Quaternion.identity);
        }
    }
}