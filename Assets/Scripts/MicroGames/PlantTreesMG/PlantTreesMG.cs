using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTreesMG : MonoBehaviour
{
    public GameObject soilPrefab; // The prefab to instantiate
    public GameObject treePrefab; // The prefab to instantiate
    public int numberOfPrefabs = 2; // Number of prefabs to spawn
    public float xOffsetRange = .10f; // Range for x offset
    public float zOffsetRange = .10f; // Range for z offset
    public float minDistance = .2f; // Minimum distance between prefabs

    private List<Vector3> spawnedPositions = new List<Vector3>();

    public List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            GameObject instantiatedObject;

            // Adjust distance if necessary to ensure all objects are spawned
            while (spawnPosition == Vector3.zero)
            {
                minDistance -= 0.1f; // Decrease minimum distance incrementally
                spawnPosition = GetValidSpawnPosition();
            }

            if(i == 1) instantiatedObject = Instantiate(treePrefab, spawnPosition, Quaternion.Euler(90,0,0), transform);
            else instantiatedObject = Instantiate(soilPrefab, spawnPosition, Quaternion.Euler(90, 0, 0), transform);
            spawnedPositions.Add(spawnPosition); // Track the position of this prefab
            spawnedObjects.Add(instantiatedObject);
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 100;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float xOffset = Random.Range(-xOffsetRange, xOffsetRange);
            float zOffset = Random.Range(-zOffsetRange, zOffsetRange);
            Vector3 spawnPosition = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset);

            if (IsPositionValid(spawnPosition))
            {
                return spawnPosition;
            }
        }
        return Vector3.zero; // Return a zero vector if no valid position is found
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 existingPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, existingPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}