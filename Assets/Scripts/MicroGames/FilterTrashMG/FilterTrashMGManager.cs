using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class FilterTrashMGManager : MonoBehaviour
{
    public GameObject trashObject; // The prefab to instantiate
    public GameObject fishObject;
    public GameObject endObject;
    public Transform spawnPoint; // The point where the object will be instantiated
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    public GameObject endChecker = null;
    private GameObject lastObject = null;
    private bool gameDone = false;
    private bool endCheck = false;

    public bool gameFailed = false;
    public List<GameObject> objectsList = new List<GameObject>();
    private List<objectType> objectList = new List<objectType>();


    public Material newSkybox;
    enum objectType { trash, fish }
    void Start()
    {
        Cursor.visible = false;
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
        GenerateObjects();
        StartCoroutine(SpawnTrash());
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
        MicroGameVariables.ShowUI();
        SetLighting();
}


    private void Update()
    {
        RemoveNullReferences();
        if (endChecker != null)
        {
            if (endChecker.gameObject.transform.position.z <= -48.54f)
            {
                WinCondition();
            }
        }
        if (endCheck)
        {
            if (lastObject == null)
            {
                WinCondition();
                endCheck = false;
            }
                
        }
    }

    void GenerateObjects()
    {
        int trashCount = 2;
        int fishCount = 1;
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.medium:
                trashCount = 2;
                fishCount = 2;
                break;
            case MicroGameVariables.levels.hard:
                trashCount = 3;
                fishCount = 2;
                break;
            default:
                trashCount = 2;
                fishCount = 1;
                break;
        }
        
        


        for (int i = 0; i < trashCount; i++)
        {
            objectList.Add(objectType.trash);
        }

        for (int i = 0; i < fishCount; i++)
        {
            objectList.Add(objectType.fish);
        }
        ShuffleObjects();
    }

    void ShuffleObjects()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            objectType temp = objectList[i];
            int randomIndex = Random.Range(i, objectList.Count);
            objectList[i] = objectList[randomIndex];
            objectList[randomIndex] = temp;
        }
    }

    IEnumerator SpawnTrash()
    {
        float timer = 1;

        int index = 0;
        int lastIndex = objectList.Count - 1;
        foreach (objectType type in objectList)
        {
            GameObject listedObject = Instantiate ((type == objectType.trash) ? trashObject : fishObject, spawnPoint.position + new Vector3(Random.Range(-59, 44),0,0), Quaternion.Euler(90, 0, 0), transform);
            objectsList.Add(listedObject);
            if (index == lastIndex)
            {
                lastObject = listedObject;
                endChecker = Instantiate(endObject, spawnPoint.position + new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0),transform);
                endCheck = true;
            }
            index++;
            yield return new WaitForSeconds(timer);
        }
    }

    void SetLighting()
    {
        RenderSettings.skybox = newSkybox;

        RenderSettings.ambientIntensity = .59f; 
        // Optionally, if you need to update lighting
        DynamicGI.UpdateEnvironment();

    }

    void RemoveNullReferences()
    {
        for (int i = objectsList.Count - 1; i >= 0; i--)
        {
            if (objectsList[i] == null)
            {
                objectsList.RemoveAt(i);
            }
        }
    }

    void WinCondition()
    {
        if (!gameDone)
        {
            foreach (GameObject listedObject in objectsList)
            {
                if (listedObject.name == "Trash(Clone)") gameFailed = true;
            }
            if (gameFailed) GameFailed();
            else GameWon(); 
            gameDone = true;
        }

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