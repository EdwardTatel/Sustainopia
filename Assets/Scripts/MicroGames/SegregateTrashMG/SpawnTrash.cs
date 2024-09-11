using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    public GameObject trashPrefab; // Assign your trash prefab in the Inspector
    public int numberOfTrash = 10; // Number of trash objects to spawn

    // Define the range for spawning (editable in the Inspector)
    public float xMin = -5f;
    public float xMax = 5f;
    public float yMin = 0f;
    public float yMax = 5f;
    public float zMin = -5f;
    public float zMax = 5f;

    // If you want to visualize the spawn area
    private void OnDrawGizmosSelected()
    {
        // Draw a wire cube representing the spawn area
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((xMin + xMax) / 2, (yMin + yMax) / 2, (zMin + zMax) / 2);
        Vector3 size = new Vector3(xMax - xMin, yMax - yMin, zMax - zMin);
        Gizmos.DrawWireCube(center, size);
    }

    void Start()
    {
        SpawnTrashObjects();
    }

    void SpawnTrashObjects()
    {
        for (int i = 0; i < numberOfTrash; i++)
        {
            // Generate random position within the defined range
            float xPos = Random.Range(xMin, xMax);
            float yPos = Random.Range(yMin, yMax);
            float zPos = Random.Range(zMin, zMax);

            Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);
            Instantiate(trashPrefab, spawnPosition, Quaternion.Euler(30, 0,0));
        }
    }
}
