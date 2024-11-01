using UnityEngine;
using System.Collections.Generic;

public class SpawnFish : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject gameManager;
    private Vector2 minXMaxX = new Vector2(-10f, 10f);
    private Vector2 minYMaxY = new Vector2(4f, 7f);
    private Vector2 minZMaxZ = new Vector2(-5f, 5f);

    private List<Vector3> spawnPositions = new List<Vector3>();
    private float minDistance = 4f; // Minimum distance between spawned objects

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        int numPrefab1 = 3;
        int numPrefab2 = 2;
        if (MicroGameVariables.GetDifficulty() == MicroGameVariables.levels.easy)
        {
            numPrefab1 = 3;
            numPrefab2 = 2;
        }
        else if (MicroGameVariables.GetDifficulty() == MicroGameVariables.levels.medium)
        {
            numPrefab1 = 5;
            numPrefab2 = 3;
        }
        else
        {
            numPrefab1 = 6;
            numPrefab2 = 4;
        }

        for (int i = 0; i < numPrefab1; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            Quaternion spawnRotation = GetRandomYRotation();

            GameObject newObject = Instantiate(prefab1, spawnPosition, spawnRotation);
            newObject.transform.SetParent(gameManager.transform, false);
            spawnPositions.Add(spawnPosition);
        }

        for (int i = 0; i < numPrefab2; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            Quaternion spawnRotation = GetRandomYRotation();

            GameObject newObject = Instantiate(prefab2, spawnPosition, spawnRotation);
            newObject.transform.SetParent(gameManager.transform, false);
            spawnPositions.Add(spawnPosition);
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool positionFound = false;
        int maxAttempts = 50;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(minXMaxX.x, minXMaxX.y);
            float randomY = Random.Range(minYMaxY.x, minYMaxY.y);
            float randomZ = Random.Range(minZMaxZ.x, minZMaxZ.y);

            spawnPosition = new Vector3(randomX, randomY, randomZ);

            if (!IsPositionTooClose(spawnPosition))
            {
                positionFound = true;
                break;
            }
        }

        if (!positionFound)
        {
        }

        return spawnPosition;
    }

    Quaternion GetRandomYRotation()
    {
        float randomYRotation = Random.Range(0f, 360f);
        return Quaternion.Euler(90f, randomYRotation, 0f);
    }

    bool IsPositionTooClose(Vector3 checkPosition)
    {
        foreach (Vector3 position in spawnPositions)
        {
            if (Vector3.Distance(position, checkPosition) < minDistance)
            {
                return true;
            }
        }

        return false;
    }
}   