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
        SDGText = GameObject.Find("LifeOnLandDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
        CollectSpeciesObjects();
    }

    private void Update()
    {
        RemoveNullReferences();
        WinCondition();
    }

    void CollectSpeciesObjects()
    {
        GameObject[] speciesArray = GameObject.FindGameObjectsWithTag("NonInvasive");
        speciesList.AddRange(speciesArray);

        GameObject[] invasivespeciesArray = GameObject.FindGameObjectsWithTag("Invasive");
        invasiveSpeciesList.AddRange(invasivespeciesArray);
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
            if (invasiveSpeciesList.Count <= 0)
            {
                SDGText.text = "Biodiversity Protected!";
                gameDone = true;
                SDGImageAnimator.Play("RemoveInvasiveSpeciesMGDone");
            }
            else if (speciesList.Count < 6)
            {
                SDGText.text = "Biodiversity Loss!";
                gameDone = true;
                SDGImageAnimator.Play("RemoveInvasiveSpeciesMGDone");

            }

            
        }
        
    }
}
