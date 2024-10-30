using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class WoodConstructionMGManager : MonoBehaviour
{
    public enum MaterialType
    {
        Wood,
        Concrete,
        None
    }

    public GameObject materialPrefab;
    public Sprite woodSprite; 
    public Sprite concreteSprite; 

    private Queue<MaterialType> materialQueue = new Queue<MaterialType>();
    private List<MaterialType> materialList = new List<MaterialType>();
    private bool gameDone = false;
    private GameObject currentMaterialInstance;

    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;

    public Transform materialsParent;
    private List<GameObject> houseBlockCheckList = new List<GameObject>();
    private int unbuiltHouseBlocks;
    private int buildingCounter;
    private int woodCount = 0;
    private int concreteCount = 0;
    void Start()
    {
        SetDifficulty();
        Cursor.visible = true;
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
        GenerateMaterialList();
        EnqueueMaterials();
        InstantiateAndAssignMaterial();
        PutHouseBlocksToList();
        StartCoroutine(PutHouseBlocksToList());
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
        MicroGameVariables.ShowUI();
    }

    private void SetDifficulty()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.hard:
                woodCount = 4;
                concreteCount = 2;
                buildingCounter = 4;

                break;
            case MicroGameVariables.levels.medium:
                woodCount = 3;
                concreteCount = 2;
                buildingCounter = 3;
                break;
            default:
                woodCount = 2;
                concreteCount = 1;
                buildingCounter = 2;
                break;
        }
    }
    void GenerateMaterialList()
    {

        for (int i = 0; i < woodCount; i++)
        {
            materialList.Add(MaterialType.Wood);
        }

        for (int i = 0; i < concreteCount; i++)
        {
            materialList.Add(MaterialType.Concrete);
        }

        for (int i = 0; i < materialList.Count; i++)
        {
            MaterialType temp = materialList[i];
            int randomIndex = Random.Range(i, materialList.Count);
            materialList[i] = materialList[randomIndex];
            materialList[randomIndex] = temp;
        }

        materialList.Add(MaterialType.Wood);
        materialList.Add(MaterialType.None);
    }

    IEnumerator PutHouseBlocksToList()
    {
        yield return new WaitForSeconds(.1f);
        houseBlockCheckList = FindObjectsOfType<GameObject>().Where(obj => (obj.name == "HouseBlock(Clone)")).ToList();
        houseBlockCheckList.Add(GameObject.Find("HouseBlock"));
    }

    void EnqueueMaterials()
    {
        foreach (var material in materialList)
        {
            materialQueue.Enqueue(material);
        }
    }

    void InstantiateAndAssignMaterial()
    {
        if (materialQueue.Count > 0)
        {
            MaterialType materialType = materialQueue.Dequeue();
            Sprite spriteToAssign = null;

            switch (materialType)
            {
                case MaterialType.Wood:
                    spriteToAssign = woodSprite;
                    break;
                case MaterialType.Concrete:
                    spriteToAssign = concreteSprite;
                    break;
                case MaterialType.None:
                    spriteToAssign = null;
                    break;
            }

            if (spriteToAssign != null)
            {
                if (currentMaterialInstance != null)
                {
                    Destroy(currentMaterialInstance);
                }

                currentMaterialInstance = Instantiate(materialPrefab);
                currentMaterialInstance.transform.SetParent(materialsParent.transform, true);
                Transform materialSpriteTransform = currentMaterialInstance.transform.Find("MaterialSprite");
                if (materialSpriteTransform != null)
                {
                    SpriteRenderer spriteRenderer = materialSpriteTransform.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sprite = spriteToAssign;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (!gameDone)
        {
            unbuiltHouseBlocks = GetComponent<BuildTower>().HouseBlocks.Count;
            if (MicroGameVariables.gameFailed == true)
            {
                GameFailed();
                gameDone = true;
            }
            else
            {
                if (currentMaterialInstance == null)
                {
                    InstantiateAndAssignMaterial();
                }
                if ((materialQueue.Count <= 0 || unbuiltHouseBlocks <= 0))
                {
                    StartCoroutine(WinCondition());

                    gameDone = true;
                }
            }
        }
    }

    IEnumerator WinCondition()
    {
        yield return new WaitForSeconds(.2f);
        int winConditionCounter = 0;
        bool buildingNotFinished = false;

        foreach (GameObject houseBlock in houseBlockCheckList)
        {
            string houseMaterial = houseBlock.transform.Find("HouseBlockModel").GetComponent<MeshRenderer>().material.name;

            Debug.Log(houseMaterial);
            if (houseMaterial == "WoodMaterial (Instance)")
            {
                winConditionCounter++;
            }
            else if (houseMaterial == "TransparentHouseBlock (Instance)" || houseMaterial == "TransparentHouseBlockDarker (Instance)")
            {
                buildingNotFinished = true;
            }
        }
        if (buildingNotFinished)
        {
            GameFailed();

        }
        else
        {

            Debug.Log(winConditionCounter+ "" + buildingCounter);
            if (winConditionCounter == buildingCounter)
            {
                GameWon();
            }
            else
            {
                GameFailed();
            }
        }
        gameDone = true;
    }
    public void GameFailed()
    {
        MicroGameVariables.setGameStats(3, false);
        SDGText.text = "Fail!";
        SDGImageAnimator.Play("MGDone");
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        MicroGameVariables.setGameStats(3, true);
        SDGText.text = "Success!";
        SDGImageAnimator.Play("MGDone");

    }


}
