using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpecies : MonoBehaviour
{
    private int numberOfSpawns;
    [SerializeField] List<GameObject> SpeciesSpawnPoints = new List<GameObject>();
    [SerializeField] List<GameObject> SpeciesArray = new List<GameObject>();
    [SerializeField] List<GameObject> selectedSpawnObjects = new List<GameObject>();

    public GameObject species1Prefab;  // Assign the prefab for the 1st species in the Inspector
    public GameObject species2Prefab;  // Assign the prefab for the 2nd species in the Inspector
    public GameObject species3Prefab;
    // Start is called before the first frame update
    private void SetDifficulty()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.hard:
                numberOfSpawns = 15;
                break;
            case MicroGameVariables.levels.medium:
                numberOfSpawns = 10;
                break;
            default:
                numberOfSpawns = 40;
                break;
        }
    }
    private void Awake()
    {
        SetDifficulty();
        GameObject[] spawnArray = GameObject.FindGameObjectsWithTag("SpeciesSpawn");
        SpeciesSpawnPoints.AddRange(spawnArray);


        // Randomly select objects and add them to the selected list
        for (int i = 0; i <= numberOfSpawns; i++)
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

            if (i > (selectedSpawnObjects.Count - 5)) //this means only 4 invasives spawn regardless of difficulty?
            {
                GameObject prefab = Instantiate(species1Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
                GetComponent<RemoveInvasiveSpeciesMGManager>().invasiveSpeciesList.Add(prefab);
                GetComponent<RemoveInvasiveSpeciesMGManager>().speciesList.Add(prefab);
            }
            else if (i > (selectedSpawnObjects.Count - 5) / 2)
            {
                GameObject prefab = Instantiate(species2Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
                GetComponent<RemoveInvasiveSpeciesMGManager>().speciesList.Add(prefab);
            }
            else {
                GameObject prefab = Instantiate(species3Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
                GetComponent<RemoveInvasiveSpeciesMGManager>().speciesList.Add(prefab);
            }
        }
    }
}
