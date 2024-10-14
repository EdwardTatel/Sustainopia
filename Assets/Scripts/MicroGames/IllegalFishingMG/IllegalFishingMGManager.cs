using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using static TMPro.Examples.TMP_ExampleScript_01;

public class IllegalFishingMGManager : MonoBehaviour
{
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    public List<GameObject> objectsList = new List<GameObject>();
    private bool list = true;
    private bool gameFailed = false;
    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
        
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
        MicroGameVariables.ShowUI();
    }

    // Update is called once per frame
    void Update()
    {
        removeInactiveObjects();
        if (list)
        {
            // Find all objects with the "Fisher" tag
            GameObject[] fisherTaggedObjects = GameObject.FindGameObjectsWithTag("Fisher");

            // Add them to the list
            objectsList.AddRange(fisherTaggedObjects);
            list = false;
        }
        
        if (MicroGameVariables.gameFailed && !gameDone)
        {
            GameFailed();
            gameDone = true;
        }
        if (objectsList.Count <= 0)
        {
            Debug.Log(MicroGameVariables.game1Stats.ToString());
            Debug.Log(MicroGameVariables.game2Stats.ToString());
            Debug.Log(MicroGameVariables.game3Stats.ToString());
            WinCondition();
        }
    }

    private void removeInactiveObjects()
    {
        for (int i = objectsList.Count - 1; i >= 0; i--)
        {
            if (!objectsList[i].activeInHierarchy)
            {
                objectsList.RemoveAt(i);
            }
        }
    }
    public void GameFailed()
    {

        MicroGameVariables.setGameStats(3, false);
        SDGText.text = "Population at Risk!";
        SDGImageAnimator.Play("LifeBelowWaterDone");
        MicroGameVariables.HideUI();
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {

        MicroGameVariables.setGameStats(3, true);
        SDGText.text = "Population Preserved!";
        SDGImageAnimator.Play("LifeBelowWaterDone");
        MicroGameVariables.HideUI();
    }

    void WinCondition()
    {
        if (!gameDone)
        {
            GameWon();
            gameDone = true;
        }
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
}
