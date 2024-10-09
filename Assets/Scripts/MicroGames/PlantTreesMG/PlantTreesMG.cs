using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantTreesMG : MonoBehaviour
{
    // Minimum distance between prefabs
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    public Sprite soilTree;
    private bool list = true;
    public bool gameWon;

    void Start()
    {
        gameWon = false;
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("ClimateActionDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
        MicroGameVariables.ShowUI();
    }

    
    private void Update()
    {
        if (gameWon) WinCondition();
        if (MicroGameVariables.gameFailed && !gameDone)
        {
            GameFailed();
            gameDone = true;
        }
    }


    void WinCondition()
    {

        if (!gameDone)
        {
                GameWon();
                gameDone = true;
        }
    }
    void GameFailed()
    {
        SDGText.text = "Biodiversity Loss!";
        SDGImageAnimator.Play("RemoveInvasiveSpeciesMGDone");
        MicroGameVariables.HideUI();
        MicroGameVariables.DeductLife();
    }
    void GameWon()
    {
        SDGText.text = "Biodiversity Protected!";
        SDGImageAnimator.Play("RemoveInvasiveSpeciesMGDone");
        MicroGameVariables.HideUI();
    }
    
}