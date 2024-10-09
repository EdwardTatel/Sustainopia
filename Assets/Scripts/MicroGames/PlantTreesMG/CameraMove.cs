using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnList = new List<GameObject>();
    private int listIterator = 0;
    private float moveTime = 1f;

    void Start()
    {
        // Find and sort the objects with the "Spawn" tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Spawn");
        objectsWithTag = objectsWithTag.OrderBy(obj => obj.name).ToArray();

        // Add them to the list in the sorted order
        foreach (GameObject obj in objectsWithTag)
        {
            spawnList.Add(obj);
        }

        // Start with the first position
         ChangeCameraPosition();
    }

    void ChangeCameraPosition()
    {
        // Get the target's position
        Vector3 targetPosition = spawnList[listIterator].transform.position;

        // Calculate the new camera position, maintaining the current y-axis position of the camera
        Vector3 newPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

        // Move the camera using LeanTween
        LeanTween.move(gameObject, newPosition, moveTime).setEase(LeanTweenType.easeInOutQuad);
        listIterator++;
    }

    public void CheckSoilObjects()
    {
        // Get the objects in the current spawn point's PlantTreesMG component
        List<GameObject> list = spawnList[listIterator - 1].GetComponent<SpawnObjects>().spawnedObjects;

        // Check if all objects have the "Planted" tag
        bool allPlanted = list.All(obj => obj.CompareTag("Planted"));

        // If all objects are planted, change the camera position
        if (allPlanted)
        {
            if (listIterator < spawnList.Count) ChangeCameraPosition();
            else GameObject.Find("GameManager").GetComponent<PlantTreesMG>().gameWon = true;
        }
    }
}