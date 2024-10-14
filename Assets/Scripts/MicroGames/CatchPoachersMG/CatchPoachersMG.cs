using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class CatchPoachersMG : MonoBehaviour
{
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    private bool list = true;
    private List<GameObject> poachers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeOnLandDoneText").GetComponent<TextMeshProUGUI>();
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
            GameObject[] poacherTaggedObjects = GameObject.FindGameObjectsWithTag("Poacher");

            // Add them to the list
            poachers.AddRange(poacherTaggedObjects);
            list = false;
        }

        if (poachers.Count <= 0) WinCondition();
        if (MicroGameVariables.gameFailed && !gameDone)
        {
            GameFailed();
            gameDone = true;
        }
    }

    private void removeInactiveObjects()
    {
        for (int i = poachers.Count - 1; i >= 0; i--)
        {
            if (!poachers[i].activeInHierarchy)
            {
                poachers.RemoveAt(i);
            }
        }
    }

    void WinCondition()
    {

        if (!gameDone)
        {

            if (poachers.Count <= 0)
            {
                GameWon();
                gameDone = true;
            }
            else
            {
                GameFailed();
                gameDone = true;
            }
        }
    }
    void GameFailed()
    {
        SDGText.text = "Biodiversity Loss!";
        SDGImageAnimator.Play("LifeOnLandDone");
        MicroGameVariables.HideUI();
        MicroGameVariables.DeductLife();
    }
    void GameWon()
    {
        SDGText.text = "Biodiversity Protected!";
        SDGImageAnimator.Play("LifeOnLandDone");
        MicroGameVariables.HideUI();
    }
}
