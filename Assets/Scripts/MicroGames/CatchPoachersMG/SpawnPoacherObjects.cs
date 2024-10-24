using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoacherObjects : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefabToSpawn;   // The prefab to spawn (for poachers)
    public GameObject prefabToSpawn2;  // The second prefab to spawn (for other objects)
    public Sprite animalSprite1;       // First sprite for animal objects
    public Sprite animalSprite2;       // Second sprite for animal objects
    private int poacherCount;          // Number of poachers to spawn
    private int normalCount;           // Number of other objects to spawn

    private GameObject parentObject;   // The parent object to store the spawned objects
    private List<Transform> spawnPoints;  // List of available spawn points

    private void Start()
    {
        parentObject = GameObject.Find("SpawnedObjects");

        // Find all spawn points with the tag "SpawnPoach"
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoach");
        spawnPoints = new List<Transform>();

        foreach (GameObject spawnPointObject in spawnPointObjects)
        {
            spawnPoints.Add(spawnPointObject.transform);
        }

        SetDifficulty();
        SpawnObjects();
    }

    private void SetDifficulty()
    {
        // Set poacher and normal object counts based on difficulty
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.hard:
                poacherCount = 4;
                normalCount = 7;
                break;
            case MicroGameVariables.levels.medium:
                poacherCount = 3;
                normalCount = 7;
                break;
            default:
                poacherCount = 2;
                normalCount = 7;
                break;
        }
    }

    private void SpawnObjects()
    {
        int totalObjectsToSpawn = poacherCount + normalCount;

        // Ensure we have enough spawn points
        if (spawnPoints.Count < totalObjectsToSpawn)
        {
            Debug.LogError("Not enough spawn points for the number of objects to spawn!");
            return;
        }

        // Shuffle the spawn points list to randomize spawn locations
        ShuffleSpawnPoints();

        // Spawn the poachers
        for (int i = 0; i < poacherCount; i++)
        {
            SpawnPrefab(prefabToSpawn, spawnPoints[i], isAnimal: false);
        }

        // Spawn the other objects (animals)
        for (int i = poacherCount; i < totalObjectsToSpawn; i++)
        {
            SpawnPrefab(prefabToSpawn2, spawnPoints[i], isAnimal: true);
        }
    }

    // Spawn a prefab at a specific spawn point and apply randomization
    private void SpawnPrefab(GameObject prefab, Transform spawnPoint, bool isAnimal)
    {
        GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        spawnedObject.transform.SetParent(parentObject.transform);
        spawnedObject.transform.rotation = Quaternion.Euler(56, spawnedObject.transform.rotation.eulerAngles.y, spawnedObject.transform.rotation.eulerAngles.z);

        // Access the SpriteRenderer in the child and apply random flipping
        SpriteRenderer spriteRenderer = spawnedObject.GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Randomly flip the sprite on the x-axis
            bool flipX = Random.value > 0.5f;
            spriteRenderer.flipX = flipX;

            // If the object is an animal, randomly choose between two sprites
            if (isAnimal)
            {
                Sprite randomSprite = Random.value > 0.5f ? animalSprite1 : animalSprite2;
                spriteRenderer.sprite = randomSprite;
            }
        }
    }

    // Shuffle the list of spawn points to randomize their order
    private void ShuffleSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }

    // Draw gizmos for debugging spawn points in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoach");

        foreach (GameObject spawnPoint in spawnPointObjects)
        {
            Gizmos.DrawWireSphere(spawnPoint.transform.position, 0.5f); // Draw small spheres at spawn points
        }
    }
}
