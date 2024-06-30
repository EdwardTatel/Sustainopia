using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class GenerateMaterials : MonoBehaviour
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

    void Start()
    {
        SDGText = GameObject.Find("ClimateActionDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
        GenerateMaterialList();
        EnqueueMaterials();
        InstantiateAndAssignMaterial();
        PutHouseBlocksToList();
        StartCoroutine(PutHouseBlocksToList());
    }

    void GenerateMaterialList()
    {
        int totalItems = 10;
        int woodCount = (int)(totalItems * 0.66f);
        int concreteCount = totalItems - woodCount;

        woodCount--;

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

                currentMaterialInstance = Instantiate(materialPrefab,materialsParent);
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
        unbuiltHouseBlocks = GetComponent<BuildTower>().HouseBlocks.Count;
        if (currentMaterialInstance == null)
        {
            InstantiateAndAssignMaterial();
        }
        if ((materialQueue.Count <= 0 || unbuiltHouseBlocks <= 0) && !gameDone)
        {
            StartCoroutine(WinCondition());
        }

    }


    IEnumerator WinCondition()
    {
        yield return new WaitForSeconds(.1f);
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
        Debug.Log(houseBlockCheckList.Count);
        Debug.Log(winConditionCounter);
        if (buildingNotFinished)
        {
            SDGText.text = "Building Unfinished!";
            SDGImageAnimator.Play("WoodConstructionMGDone");
        }
        else
        {
            if (winConditionCounter >= 6)
            {
                SDGText.text = "Neutral Carbon Construction!";
                SDGImageAnimator.Play("WoodConstructionMGDone");
            }
            else
            {
                SDGText.text = "Unsustainable Construction!";
                SDGImageAnimator.Play("WoodConstructionMGDone");
            }
        }
        gameDone = true;
    }



}
