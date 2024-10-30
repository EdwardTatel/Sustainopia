using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReleaseFishMGManager : MonoBehaviour
{
    public List<GameObject> fishList = new List<GameObject>();
    private int bigFishCount = 0;
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    private int fishCountTarget;
    public Material newSkybox;

    void Start()
    {
       if(GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().timerbar == null) GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().timerbar = GameObject.Find("TimerBar");
        MicroGameVariables.ShowUI();
        Cursor.visible = true;
       MicroGameVariables.gameFailed = false;
       SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
       SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
       SetDifficulty();
       GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
       MicroGameVariables.ShowUI();
       SetLighting();
    }

    void Update()
    {
        RemoveNullReferences();
        CollectFishObjects();
        WinCondition();
    }

    void CollectFishObjects()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Fish");

        foreach (GameObject obj in taggedObjects)
        {
            if (!fishList.Contains(obj))
            {
                fishList.Add(obj);
            }
        }

    }
    private void RemoveNullReferences()
    {
        fishList.RemoveAll(obj => obj == null);
    }

    private void SetDifficulty()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.hard:
                fishCountTarget = 6;
                break;
            case MicroGameVariables.levels.medium:
                fishCountTarget = 5;
                break;
            default:
                fishCountTarget = 3;
                break;
        }
    }
    private void WinCondition()
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
                if (fishList.Count <= fishCountTarget)
                {
                    foreach (GameObject fish in fishList)
                    {
                        if (fish.name == "BigFish(Clone)") bigFishCount++;
                    }

                    if (bigFishCount >= fishList.Count)
                    {
                        GameWon();
                    }
                    else
                    {
                        GameFailed();
                    }
                   
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
        RenderSettings.ambientIntensity = 0;
        // Optionally, if you need to update lighting
        DynamicGI.UpdateEnvironment();
    }
}
