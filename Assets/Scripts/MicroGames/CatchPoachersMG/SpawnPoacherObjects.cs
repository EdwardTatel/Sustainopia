using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPoacherObjects : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefabToSpawn;  // The prefab to spawn
    public GameObject prefabToSpawn2;
    private int poacherCount;
    private int normalCount;
    public Vector3[] localSpawnAreaPoints = new Vector3[4];  // Define the 4 corners of the spawn area (relative to object) // Number of prefabs to spawn
    public float zOffset = 0f;        // Offset in the z-direction
    public float minSpawnDistance = 1.5f; // Minimum distance between spawned objects
    public int maxAttempts = 10;      // Maximum attempts to find a valid spawn location
    private GameObject parentObject;

    private List<Vector3> spawnedPositions = new List<Vector3>(); // Store already spawned positions

    private void Start()
    {
        parentObject = GameObject.Find("SpawnedObjects");
        SetDifficulty();
        SpawnObjects();
    }

    private void SetDifficulty()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.hard:
                poacherCount = 4;
                normalCount = 7;
                break;
            case MicroGameVariables.levels.medium:
                poacherCount = 3;
                normalCount = 5;
                break;
            default:
                poacherCount = 2;
                normalCount = 3;
                break;
        }
    }
    private void SpawnObjects()
    {
        for (int i = 0; i < poacherCount + normalCount; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            bool validPositionFound = false;

            // Try finding a valid position within the maximum number of attempts
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                // Generate a random local position within the spawn area
                Vector3 localSpawnPosition = GetRandomPointInSpawnArea();
                spawnPosition = transform.TransformPoint(localSpawnPosition); // Convert to world position

                // Check if the new spawn position is far enough from all previously spawned objects
                if (IsPositionValid(spawnPosition))
                {
                    validPositionFound = true;
                    break;
                }
            }

            // If a valid position is found, instantiate the object and store its position
            if (validPositionFound)
            {
                GameObject childObject;
                if (i <= poacherCount) childObject = Instantiate(prefabToSpawn, spawnPosition, transform.rotation);
                else childObject = Instantiate(prefabToSpawn2, spawnPosition, transform.rotation);
                childObject.transform.SetParent(parentObject.transform);
                spawnedPositions.Add(spawnPosition);
            }
        }
    }

    // Function to get a random point within the custom spawn area
    private Vector3 GetRandomPointInSpawnArea()
    {
        float randomT1 = Random.Range(0f, 1f);
        float randomT2 = Random.Range(0f, 1f);

        Vector3 pointA = Vector3.Lerp(localSpawnAreaPoints[0], localSpawnAreaPoints[1], randomT1);
        Vector3 pointB = Vector3.Lerp(localSpawnAreaPoints[3], localSpawnAreaPoints[2], randomT1);
        Vector3 randomPoint = Vector3.Lerp(pointA, pointB, randomT2);

        randomPoint.z += zOffset;
        return randomPoint;
    }

    // Check if the new spawn position is far enough from all previously spawned objects
    private bool IsPositionValid(Vector3 newPosition)
    {
        foreach (Vector3 pos in spawnedPositions)
        {
            if (Vector3.Distance(newPosition, pos) < minSpawnDistance)
            {
                return false; // Too close to another object
            }
        }
        return true; // Valid position
    }

    // Draw the spawn area in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (localSpawnAreaPoints.Length >= 4)
        {
            Vector3 worldPoint0 = transform.TransformPoint(localSpawnAreaPoints[0]);
            Vector3 worldPoint1 = transform.TransformPoint(localSpawnAreaPoints[1]);
            Vector3 worldPoint2 = transform.TransformPoint(localSpawnAreaPoints[2]);
            Vector3 worldPoint3 = transform.TransformPoint(localSpawnAreaPoints[3]);

            Gizmos.DrawLine(worldPoint0, worldPoint1);
            Gizmos.DrawLine(worldPoint1, worldPoint2);
            Gizmos.DrawLine(worldPoint2, worldPoint3);
            Gizmos.DrawLine(worldPoint3, worldPoint0);
        }
    }
}

