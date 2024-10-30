using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FillSolarPanelMGManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pentominoPieces = new List<GameObject>();
    [SerializeField] private List<GameObject> piecesArea = new List<GameObject>();
    [SerializeField] private List<GameObject> panelCheckers = new List<GameObject>();
    public GameObject pentominoParent;
    public GameObject panelChecker;
    private int counter = 0;
    private bool gameWon = false;
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    private int numberOfPieces;
    // Start is called before the first frame update
    void Awake()
    {
         
        SetDifficulty();
        Cursor.visible = true;
        enlistPiecesAreas();
        GameObject[] array = GameObject.FindGameObjectsWithTag("Pentomino");
        pentominoPieces.AddRange(array);
        /*ColorizePentominos();*/

        for (int i = 0; i < numberOfPieces; i++)
        {
            RemoveRandomObjectFromList();
        }
        

    }
    private void Start()
    {
        Debug.Log(MicroGameVariables.GetDifficulty());
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
        MicroGameVariables.ShowUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameDone)
        {
            if (MicroGameVariables.gameFailed)
            {
                GameFailed();
                gameDone = true; 
            }

            if (Input.GetMouseButtonUp(0)) // 0 = left mouse button, 1 = right, 2 = middle
            {
                gameWon = true;
                foreach (GameObject panelChecker in panelCheckers)
                {
                    if (panelChecker.GetComponent<PanelChecker>().enablePlace)
                    {
                        gameWon = false;
                    }
                }
                if (gameWon)
                {
                    GameWon();
                    gameDone = true;
                }
            }
        }
    }

    void SetDifficulty()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.medium:
                numberOfPieces = 3;
                break;
            case MicroGameVariables.levels.hard:
                numberOfPieces = 4;
                break;
            default:
                numberOfPieces = 2;
                break;
        }
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
        objectToMove.gameObject.GetComponent<SnapPiece>().initialPosition = piecesArea[counter];
        // Process the object
        foreach (Transform obj in objectToMove.transform)
        {
            obj.GetChild(0).gameObject.SetActive(true);
            GameObject checker = Instantiate(panelChecker, obj.transform.position, panelChecker.transform.rotation, transform);
            panelCheckers.Add(checker);
            obj.gameObject.AddComponent<CheckPlace>();
            obj.gameObject.AddComponent<SolarPanelScript>();
            
        }

        Vector3 currentCenter = GetCenterOfChildren(objectToMove);

        // Calculate the offset from the current center to the pivot
        Vector3 centerOffset = objectToMove.transform.position - currentCenter;
        float yPosition = objectToMove.transform.position.y;
        // Move the parent object to the new position based on the center
        objectToMove.transform.position = piecesArea[counter].transform.position + centerOffset;
        objectToMove.transform.rotation = piecesArea[counter].transform.rotation;
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, yPosition, objectToMove.transform.position.z);
        // Remove the object from the list to prevent it from being selected again
        pentominoPieces.RemoveAt(randomIndex);
        counter++;
    }
    Vector3 GetCenterOfChildren(GameObject parentObject)
    {
        // Initialize an empty bounds structure
        Bounds combinedBounds = new Bounds(parentObject.transform.GetChild(0).position, Vector3.zero);

        // Loop through all child objects and encapsulate their bounds
        foreach (Transform child in parentObject.transform)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                combinedBounds.Encapsulate(childRenderer.bounds);
            }
        }

        // Return the center of the combined bounds
        return combinedBounds.center;
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

    void enlistPiecesAreas()
    {
        GameObject[] foundPieces = GameObject.FindGameObjectsWithTag("PiecesArea");

        // Add the found objects to the list
        piecesArea.AddRange(foundPieces);

    }

    public void GameFailed()
    {
        MicroGameVariables.setGameStats(2, false);
        SDGText.text = "Fail!";
        SDGImageAnimator.Play("MGDone");
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        MicroGameVariables.setGameStats(2, true);
        SDGText.text = "Success!";
        SDGImageAnimator.Play("MGDone");

    }
}
