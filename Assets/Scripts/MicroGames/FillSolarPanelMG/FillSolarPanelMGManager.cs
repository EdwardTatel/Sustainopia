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
        Cursor.visible = true;
        enlistPiecesAreas();
        GameObject[] array = GameObject.FindGameObjectsWithTag("Pentomino");
        pentominoPieces.AddRange(array);
        ColorizePentominos();

        for (int i = 0; i <= numberOfPieces + 1; i++)
        {
            RemoveRandomObjectFromList();
        }
        

    }
    private void Start()
    {
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("ClimateActionDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
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
            GameObject checker = Instantiate(panelChecker, obj.transform.position, panelChecker.transform.rotation, transform);
            panelCheckers.Add(checker);
            obj.gameObject.AddComponent<CheckPlace>();
            obj.gameObject.AddComponent<SolarPanelScript>();
            
        }

        // Move the object to the new position
        objectToMove.transform.position = piecesArea[counter].transform.position;
        objectToMove.transform.rotation = piecesArea[counter].transform.rotation;

        // Remove the object from the list to prevent it from being selected again
        pentominoPieces.RemoveAt(randomIndex);
        counter++;
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
        SDGText.text = "Failed!";
        SDGImageAnimator.Play("WoodConstructionMGDone");
        MicroGameVariables.HideUI();
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        SDGText.text = "Clean Energy!";
        SDGImageAnimator.Play("WoodConstructionMGDone");
        MicroGameVariables.HideUI();
    }
}
