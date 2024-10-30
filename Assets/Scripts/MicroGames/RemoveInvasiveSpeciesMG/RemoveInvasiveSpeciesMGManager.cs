using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class RemoveInvasiveSpeciesMGManager : MonoBehaviour
{
    public List<GameObject> speciesList = new List<GameObject>();
    public List<GameObject> invasiveSpeciesList = new List<GameObject>();
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    // Start is called before the first frame update
    void Start()
    {
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeOnLandDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
        MicroGameVariables.ShowUI();
    }
    private void Update()
    {
        RemoveNullReferences();
        WinCondition();
    }


    void RemoveNullReferences()
    {
        for (int i = invasiveSpeciesList.Count - 1; i >= 0; i--)
        {
            if (invasiveSpeciesList[i] == null)
            {
                invasiveSpeciesList.RemoveAt(i);
            }
        }
        for (int i = speciesList.Count - 1; i >= 0; i--)
        {
            if (speciesList[i] == null)
            {
                speciesList.RemoveAt(i);
            }
        }
    }


    void WinCondition()
    {
        if (!gameDone)
        {
            if (MicroGameVariables.gameFailed == true)
            {
                GameFailed();
                gameDone = true;
            }
            else
            {
                if (invasiveSpeciesList.Count <= 0)
                {
                    GameWon();
                    gameDone = true;
                }
                else if (speciesList.Count < 13)
                {
                    GameFailed();
                    gameDone = true;
                }
            }
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

