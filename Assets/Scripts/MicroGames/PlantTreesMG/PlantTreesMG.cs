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
    public Material newSkybox;
    void Start()
    {
        SetLighting();
        if (GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().timerbar == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().timerbar = GameObject.Find("TimerBar");
        if (GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart1 == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart1 = GameObject.Find("Heart1");
        if (GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart2 == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart2 = GameObject.Find("Heart2");
        if (GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart3 == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().heart3 = GameObject.Find("Heart3");
        MicroGameVariables.ShowUI();
        Cursor.visible = true;
        gameWon = false;
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
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