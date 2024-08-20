using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpecies : MonoBehaviour
{
    [SerializeField] List<GameObject> SpeciesSpawnPoints = new List<GameObject>();
    [SerializeField] List<GameObject> SpeciesArray = new List<GameObject>();
    [SerializeField] List<GameObject> selectedSpawnObjects = new List<GameObject>();

    public GameObject species1Prefab;  // Assign the prefab for the 1st species in the Inspector
    public GameObject species2Prefab;  // Assign the prefab for the 2nd species in the Inspector
    public GameObject species3Prefab;
    // Start is called before the first frame update

    private void Awake()
    {
        GameObject[] spawnArray = GameObject.FindGameObjectsWithTag("SpeciesSpawn");
        SpeciesSpawnPoints.AddRange(spawnArray);


        // Determine the number of objects to select (50% of the found objects)
        int numToSelect = Mathf.CeilToInt(SpeciesSpawnPoints.Count * 0.2f);

        // Create a list to store the selected objects
        ;

        // Randomly select objects and add them to the selected list
        while (selectedSpawnObjects.Count < numToSelect)
        {
            int randomIndex = Random.Range(0, SpeciesSpawnPoints.Count);
            GameObject selectedObject = SpeciesSpawnPoints[randomIndex];

            if (!selectedSpawnObjects.Contains(selectedObject))
            {
                selectedSpawnObjects.Add(selectedObject);
            }
        }

        for (int i = selectedSpawnObjects.Count - 1; i > 0 ; i--)
        {

            if (i > (selectedSpawnObjects.Count - 5))
            {
                Debug.Log("1st");
                GameObject prefab = Instantiate(species1Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
                GetComponent<RemoveInvasiveSpeciesMGManager>().speciesList.Add(prefab);
            }
            else if (i > (selectedSpawnObjects.Count - 5) / 2)
            {
                Debug.Log("2nd");
                GameObject prefab = Instantiate(species2Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
                GetComponent<RemoveInvasiveSpeciesMGManager>().speciesList.Add(prefab);
            }
            else {
                Debug.Log("3rd");
                GameObject prefab = Instantiate(species3Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
                GetComponent<RemoveInvasiveSpeciesMGManager>().speciesList.Add(prefab);
            }
        }
    }
}
