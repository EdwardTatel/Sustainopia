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


    void Start()
    {
        Cursor.visible = true;
       MicroGameVariables.gameFailed = false;
       SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
       SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
       SetDifficulty();
       GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
       MicroGameVariables.ShowUI();
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
        SDGImageAnimator.Play("LifeBelowWaterDone");
        MicroGameVariables.HideUI();
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        MicroGameVariables.setGameStats(1, true);
        SDGText.text = "Success!";
        SDGImageAnimator.Play("LifeBelowWaterDone");
        MicroGameVariables.HideUI();
        
    }
}
