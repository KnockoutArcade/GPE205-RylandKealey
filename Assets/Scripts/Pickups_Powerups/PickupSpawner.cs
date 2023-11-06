using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    private GameObject spawnedPickup;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // if it is there, don't spawn anything
        if (spawnedPickup == null)
        {
            // if it is time to spawn a pickup
            if (Time.time > nextSpawnTime)
            {
                // Spawn it and set up the next time
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            // otherwise, the object still exists so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
