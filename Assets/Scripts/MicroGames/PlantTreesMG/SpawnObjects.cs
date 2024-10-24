using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    private int numberOfPrefabs = 3; // Number of prefabs to spawn
    private float minDistance = .1f;

    public GameObject soilPrefab; // The prefab to instantiate
    public GameObject treePrefab; // The prefab to instantiate

    public List<Vector3> spawnedPositions = new List<Vector3>();
    public List<GameObject> spawnedObjects = new List<GameObject>();

    // Square spawn area settings (editable in Inspector)
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // Width and height of the spawn area
    public float zOffset = 0f; // Z Offset for the entire spawn area

    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty();
        SpawnPrefabs();
    }

    // Visualize the spawn area with Gizmos in the Editor
    void OnDrawGizmos()
    {
        // Draw the spawn area as a wireframe square
        Gizmos.color = Color.green;
        Vector3 offsetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset);
        Gizmos.DrawWireCube(offsetPosition, new Vector3(spawnAreaSize.x, 0, spawnAreaSize.y));
    }

    // Method to spawn prefabs in valid positions
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

    // Set the difficulty and number of prefabs to spawn
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

    // Get a valid spawn position within the defined spawn area
    Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 100;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float xOffset = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float zOffsetLocal = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
            Vector3 spawnPosition = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset + zOffsetLocal);

            if (IsPositionValid(spawnPosition))
            {
                return spawnPosition;
            }
        }
        return Vector3.zero; // Return a zero vector if no valid position is found
    }

    // Check if the spawn position is valid by comparing distances
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
