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
    public bool gameWon;

    void Start()
    {
        Cursor.visible = true;
        gameWon = false;
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("ClimateActionDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
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
    public void GameFailed()
    {
        MicroGameVariables.setGameStats(1, false);
        SDGText.text = "Fail!";
        SDGImageAnimator.Play("MGDone");
        MicroGameVariables.HideUI();
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        MicroGameVariables.setGameStats(1, true);
        SDGText.text = "Success!";
        SDGImageAnimator.Play("MGDone");
        MicroGameVariables.HideUI();

    }


}