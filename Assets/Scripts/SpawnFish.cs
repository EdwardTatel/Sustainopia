using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject prefab1; 
    public GameObject prefab2;
    public GameObject gameManager;
    private int numberOfObjects = 10; 
    private Vector3 minXMaxX = new Vector3(-10f, 10f); 
    private Vector3 minYMaxY = new Vector3(3f, 5f);
    private Vector3 minZMaxZ = new Vector3(-5f, 5f); 
    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        int numPrefab1 = (int)(numberOfObjects * 0.7f); 
        int numPrefab2 = numberOfObjects - numPrefab1; 

        for (int i = 0; i < numPrefab1; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            Quaternion spawnRotation = GetRandomYRotation();

            GameObject newObject = Instantiate(prefab1, spawnPosition, spawnRotation);
            newObject.transform.SetParent(gameManager.transform, false);
        }

        for (int i = 0; i < numPrefab2; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            Quaternion spawnRotation = GetRandomYRotation();

            GameObject newObject = Instantiate(prefab2, spawnPosition, spawnRotation);
            newObject.transform.SetParent(gameManager.transform, false);
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

        return spawnPosition;
    }

    Quaternion GetRandomYRotation()
    {
        float randomYRotation = Random.Range(0f, 360f);
        return Quaternion.Euler(90f, randomYRotation, 0f);
    }

    bool IsPositionTooClose(Vector3 checkPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(checkPosition, 4f); 
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != null && collider.gameObject != gameObject)
            {
                return true; 
            }
        }

        return false; 
    }
}
