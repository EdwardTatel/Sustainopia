using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class FilterTrashMGManager : MonoBehaviour
{
    public GameObject trashObject; // The prefab to instantiate
    public GameObject fishObject;
    public Transform spawnPoint; // The point where the object will be instantiated
    private int trashQuantity = 5;

    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;

    private List<objectType> objectList = new List<objectType>();
    enum objectType { trash, fish }
    void Start()
    {
        MicroGameVariables.gameFailed = false;
        /*SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();*/
        GenerateObjects();
        StartCoroutine(SpawnTrash());
        /*GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();*/
        MicroGameVariables.ShowUI();
    }

    void GenerateObjects()
    {
        int objectQuantity = 7;
        int trashCount = (int)(objectQuantity * 0.66f);
        int fishCount = objectQuantity - trashCount;


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
        float timer;
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.medium:
                timer = Random.Range(1, 2);
                break;
            case MicroGameVariables.levels.hard:
                timer = Random.Range(.5f, 1);
                break;
            default:
                timer = Random.Range(1,2);
                break;
        }

        foreach(objectType type in objectList)
        {
            Instantiate((type == objectType.trash) ? trashObject : fishObject, spawnPoint.position + new Vector3(Random.Range(-30, 30),0,0), Quaternion.Euler(90, 0, 0));
            yield return new WaitForSeconds(timer);
        }
    }
    public void GameFailed()
    {
        SDGText.text = "Clean Waters!";
        SDGImageAnimator.Play("ReleaseFishMGDone");
        MicroGameVariables.HideUI();
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        SDGText.text = "Dirty Waters!";
        SDGImageAnimator.Play("ReleaseFishMGDone");
        MicroGameVariables.HideUI();
    }

}