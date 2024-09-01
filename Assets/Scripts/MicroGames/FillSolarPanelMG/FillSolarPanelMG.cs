using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FillSolarPanelMG : MonoBehaviour
{
    [SerializeField] private List<GameObject> pentominoPieces = new List<GameObject>();
    public GameObject piecesArea;
    public GameObject pentominoParent;
    public GameObject panelChecker;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] array =GameObject.FindGameObjectsWithTag("Pentomino");
        pentominoPieces.AddRange(array);
        ColorizePentominos();
        RemoveRandomObjectFromList();
        RemoveRandomObjectFromList();
        RemoveRandomObjectFromList();
        RemoveRandomObjectFromList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RemoveRandomObjectFromList()
    {
        // Check if there are any pieces left in the list
        if (pentominoPieces.Count == 0)
        {
            Debug.LogWarning("No more pieces to remove!");
            return;
        }

        // Get a random index
        int randomIndex = Random.Range(0, pentominoPieces.Count);

        // Get the GameObject to remove
        GameObject objectToMove = pentominoPieces[randomIndex];
        objectToMove.gameObject.AddComponent<SnapPiece>();
        objectToMove.gameObject.GetComponent<SnapPiece>().initialPosition = piecesArea;
        // Process the object
        foreach (Transform obj in objectToMove.transform)
        {
            Instantiate(panelChecker, obj.transform.position, panelChecker.transform.rotation, transform);
            obj.gameObject.AddComponent<CheckPlace>();
            obj.gameObject.AddComponent<SolarPanelScript>();
            
        }

        // Move the object to the new position
        objectToMove.transform.position = piecesArea.transform.position;
        objectToMove.transform.rotation = piecesArea.transform.rotation;

        // Remove the object from the list to prevent it from being selected again
        pentominoPieces.RemoveAt(randomIndex);
    }
    void ColorizePentominos()
    {
        foreach (Transform obj in pentominoParent.transform)            
        {
            Color newColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
            foreach (Transform obj1 in obj.transform)
            {
                SpriteRenderer spriteRenderer = obj1.GetComponent<SpriteRenderer>();
                spriteRenderer.color = newColor;
            }   
        }
    }
}
