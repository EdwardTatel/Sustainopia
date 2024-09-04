using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefabToSpawn;  // The prefab to spawn
    public Vector2 xRange = new Vector2(-5, 5);  // Range of X coordinates for spawning
    public Vector2 yRange = new Vector2(-5, 5);  // Range of Y coordinates for spawning
    public int numberOfSpawns = 10;  // Number of prefabs to spawn

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
            // Generate random X and Y within the specified ranges
            float randomX = Random.Range(xRange.x, xRange.y);
            float randomY = Random.Range(yRange.x, yRange.y);

            // Create the spawn position
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Instantiate the prefab at the spawn position
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    // Draw the spawn range in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Calculate the center of the spawn area
        Vector3 center = new Vector3((xRange.x + xRange.y) / 2, (yRange.x + yRange.y) / 2, 0);

        // Calculate the size of the spawn area
        Vector3 size = new Vector3(xRange.y - xRange.x, yRange.y - yRange.x, 0);

        // Draw the rectangle representing the spawn area
        Gizmos.DrawWireCube(center, size);
    }
}