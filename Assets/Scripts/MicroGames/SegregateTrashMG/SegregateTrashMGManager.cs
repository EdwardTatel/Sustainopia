using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class SegregateTrashMGManager : MonoBehaviour
{

    public List<GameObject> trashList = new List<GameObject>();
    public GameObject[] bins; // Assign the three bin GameObjects in the Inspector
    public GameObject[] binsLabel;
    public Material ewasteMaterial; // Material for EwasteBin
    public Material organicMaterial; // Material for OrganicBin
    public Material recycleMaterial; // Material for RecycleBin
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    private bool trashCollected = false;
    private string[] availableTags = { "Ewaste", "Organic", "Recyclable" };
    public Material newSkybox;

    void Start()
    {
        SetLighting();
        MicroGameVariables.resetTries();
        if (bins.Length != 3)
        {
            return;
        }

        if (GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().timerbar == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().timerbar = GameObject.Find("TimerBar");
        if (GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart1 == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart1 = GameObject.Find("Heart1");
        if (GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart2 == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart2 = GameObject.Find("Heart2");
        if (GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart3 == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart3 = GameObject.Find("Heart3");
        MicroGameVariables.ShowUI();

        // Randomize the tag assignments
        AssignRandomTags();
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
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
                binsLabel[i].GetComponent<TextMeshPro>().text = "EWASTE";
            }
            else if (bins[i].tag == "Organic")
            {
                ChangeMaterial(bins[i], organicMaterial);
                binsLabel[i].GetComponent<TextMeshPro>().text = "BIO";
            }
            else if (bins[i].tag == "Recyclable")
            {
                ChangeMaterial(bins[i], recycleMaterial);
                binsLabel[i].GetComponent<TextMeshPro>().text = "NON BIO";
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
                    GameFailed();
                    gameDone = true;
                }
                else if (trashList.Count <= 0)
                {
                    GameWon();
                    gameDone = true;
                }
            }
        }
    }
    public void GameFailed()
    {
        MicroGameVariables.setGameStats(1, false);
        SDGText.text = "Fail!";
        SDGImageAnimator.Play("MGDone");
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        MicroGameVariables.setGameStats(1, true);
        SDGText.text = "Success!";
        SDGImageAnimator.Play("MGDone");

    }
    void SetLighting()
    {
        RenderSettings.skybox = newSkybox;
        RenderSettings.ambientIntensity = 1;
        // Optionally, if you need to update lighting
        DynamicGI.UpdateEnvironment();
    }



}