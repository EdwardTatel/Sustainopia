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
    private int numberOfInvasive;
    // Start is called before the first frame update
    private void SetDifficulty()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.hard:
                numberOfSpawns = 40;
                numberOfInvasive = 4;
                break;
            case MicroGameVariables.levels.medium:
                numberOfSpawns = 35;
                numberOfInvasive = 3;
                break;
            default:
                numberOfSpawns = 30;
                numberOfInvasive = 2;
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
            GameObject prefab;
            if (i > (selectedSpawnObjects.Count - numberOfInvasive)) //this means only 4 invasives spawn regardless of difficulty?
            {
                prefab = Instantiate(species1Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
                GetComponent<RemoveInvasiveSpeciesMGManager>().invasiveSpeciesList.Add(prefab);
            }
            else if (i > (selectedSpawnObjects.Count - 5) / 2)
            {
                prefab = Instantiate(species2Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
            }
            else {
                prefab = Instantiate(species3Prefab, selectedSpawnObjects[i].transform.position, selectedSpawnObjects[i].transform.rotation);
            }

            SpriteRenderer spriteRenderer = prefab.GetComponentInChildren<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                // Generate a random number (0 or 1)
                int randomValue = Random.Range(0, 2);  // This will return either 0 or 1

                // If the random value is 1, flip the sprite by setting flipX to true
                spriteRenderer.flipX = randomValue == 1;
            }
            GetComponent<RemoveInvasiveSpeciesMGManager>().speciesList.Add(prefab);
        }
    }
}
