using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class SegregateTrashMGManager : MonoBehaviour
{

    public List<GameObject> trashList = new List<GameObject>();
    public GameObject[] bins; // Assign the three bin GameObjects in the Inspector
    public Material ewasteMaterial; // Material for EwasteBin
    public Material organicMaterial; // Material for OrganicBin
    public Material recycleMaterial; // Material for RecycleBin
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    private bool trashCollected = false;
    private string[] availableTags = { "Ewaste", "Organic", "Recyclable" };
    
    void Start()
    {
        if (bins.Length != 3)
        {
            return;
        }
        
        // Randomize the tag assignments
        AssignRandomTags();
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("ClimateActionDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
        MicroGameVariables.ShowUI();
        StartCoroutine(CollectTrashAfterStart());
    }

    IEnumerator CollectTrashAfterStart()
    {
        // Optionally wait for a frame or any delay you need
        yield return null; // This waits for 1 frame

        // Now collect objects by tags
        CollectObjectsByTagAndName("Recyclable", "Trash(Clone)");
        CollectObjectsByTagAndName("Organic", "Trash(Clone)");
        CollectObjectsByTagAndName("Ewaste", "Trash(Clone)");
        trashCollected = true;
    }

    private void Update()
    {
        RemoveNullReferences();
        WinCondition();
    }


    void AssignRandomTags()
    {
        // Create a list to shuffle the available tags
        List<string> tagsList = new List<string>(availableTags);

        // Shuffle the list of tags
        for (int i = 0; i < tagsList.Count; i++)
        {
            string temp = tagsList[i];
            int randomIndex = Random.Range(i, tagsList.Count);
            tagsList[i] = tagsList[randomIndex];
            tagsList[randomIndex] = temp;
        }

        // Assign a tag from the shuffled list to each bin and change materials
        for (int i = 0; i < bins.Length; i++)
        {
            bins[i].tag = tagsList[i];

            // Change materials based on the assigned tag
            if (bins[i].tag == "Ewaste")
            {
                ChangeMaterial(bins[i], ewasteMaterial);
            }
            else if (bins[i].tag == "Organic")
            {
                ChangeMaterial(bins[i], organicMaterial);
            }
            else if (bins[i].tag == "Recyclable")
            {
                ChangeMaterial(bins[i], recycleMaterial);
            }
        }
    }

    void ChangeMaterial(GameObject bin, Material newMaterial)
    {
        // Get all children of the bin and change their material
        Renderer[] childRenderers = bin.GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in childRenderers)
        {
            rend.material = newMaterial;
        }
    }

    void CollectObjectsByTagAndName(string tag, string name)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // Filter objects by name and add only those that match
        foreach (GameObject obj in objectsWithTag)
        {
            if (obj.name == name)
            {
                trashList.Add(obj);
            }
        }
    }
    private void RemoveNullReferences()
    {
        trashList.RemoveAll(obj => obj == null);
    }

    
    private void WinCondition()
    {
        if (!gameDone)
        {
            if (MicroGameVariables.gameFailed)
            {
                GameFailed();
                gameDone = true;
            }
            if (trashCollected)
            {
                if (MicroGameVariables.getTries() <= 0)
                {
                    Debug.Log("FAILER!");
                    GameFailed();
                    gameDone = true;
                }
                else if (trashList.Count <= 0)
                {
                    Debug.Log("Wonners!");
                    GameWon();
                    gameDone = true;
                }
            }
        }
    }
    public void GameFailed()
    {
        SDGText.text = "Dirty Environment!";
        SDGImageAnimator.Play("WoodConstructionMGDone");
        MicroGameVariables.HideUI();
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        SDGText.text = "Clean Environment!";
        SDGImageAnimator.Play("WoodConstructionMGDone");
        MicroGameVariables.HideUI();
    }



}