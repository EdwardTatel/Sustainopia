using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    private int numberOfPrefabs = 3; // Number of prefabs to spawn
    private float xOffsetRange = .20f; // Range for x offset
    private float zOffsetRange = .20f; // Range for z offset
    private float minDistance = 1f;

    public GameObject soilPrefab; // The prefab to instantiate
    public GameObject treePrefab; // The prefab to instantiate

    public List<Vector3> spawnedPositions = new List<Vector3>();

    public List<GameObject> spawnedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        SetDifficulty();
        SpawnPrefabs();
    }

    // Update is called once per frame
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

            if (i == 1) instantiatedObject = Instantiate(treePrefab, spawnPosition, Quaternion.Euler(90, 0, 0), transform);
            else instantiatedObject = Instantiate(soilPrefab, spawnPosition, Quaternion.Euler(90, 0, 0), transform);
            spawnedPositions.Add(spawnPosition); // Track the position of this prefab
            spawnedObjects.Add(instantiatedObject);
        }
    }
    void SetDifficulty()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.medium:
                numberOfPrefabs = 3;
                break;
            case MicroGameVariables.levels.hard:
                numberOfPrefabs = 4;
                break;
            default:
                numberOfPrefabs = 2;
                break;
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
