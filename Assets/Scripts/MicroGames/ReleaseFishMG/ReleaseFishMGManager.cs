using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReleaseFishMGManager : MonoBehaviour
{
    public List<GameObject> fishList = new List<GameObject>();
    [SerializeField] private int bigFishCount = 0;
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;


    void Start()
    {
       SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
       SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
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

    private void WinCondition()
    {
        if (fishList.Count <= 6 && !gameDone)
        {
            foreach(GameObject fish in fishList)
            {
                if (fish.name == "BigFish(Clone)") bigFishCount++;
            }

            if (bigFishCount >= 5)
            {
                SDGText.text = "Population Preserved!";
                SDGImageAnimator.Play("ReleaseFishMGDone");
            }


            else
            {
                SDGText.text = "Population at Risk!";
                SDGImageAnimator.Play("ReleaseFishMGDone");
            }
            gameDone = true;
        }

        
    }
}
