using System.Collections.Generic;
using UnityEngine;

public class GenerateMaterials : MonoBehaviour
{
    public enum MaterialType
    {
        Wood,
        Concrete
    }

    public GameObject materialPrefab; // Assign the material prefab in the Inspector
    public Sprite woodSprite;  // Assign the Wood sprite in the Inspector
    public Sprite concreteSprite;  // Assign the Concrete sprite in the Inspector

    private Queue<MaterialType> materialQueue = new Queue<MaterialType>();
    private List<MaterialType> materialList = new List<MaterialType>();
    private GameObject currentMaterialInstance;

    void Start()
    {
        GenerateMaterialList();
        EnqueueMaterials();
        InstantiateAndAssignMaterial();
    }

    void GenerateMaterialList()
    {
        int totalItems = 7;
        int woodCount = (int)(totalItems * 0.7f);
        int concreteCount = totalItems - woodCount;

        for (int i = 0; i < woodCount; i++)
        {
            materialList.Add(MaterialType.Wood);
        }

        for (int i = 0; i < concreteCount; i++)
        {
            materialList.Add(MaterialType.Concrete);
        }

        // Shuffle the list to randomize the order
        for (int i = 0; i < materialList.Count; i++)
        {
            MaterialType temp = materialList[i];
            int randomIndex = Random.Range(i, materialList.Count);
            materialList[i] = materialList[randomIndex];
            materialList[randomIndex] = temp;
        }
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
            }

            if (spriteToAssign != null)
            {
                // Destroy existing material instance if it exists
                if (currentMaterialInstance != null)
                {
                    Destroy(currentMaterialInstance);
                }

                // Instantiate new material prefab
                currentMaterialInstance = Instantiate(materialPrefab);
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
        // Check if the current instance has been destroyed and instantiate the next one
        if (currentMaterialInstance == null)
        {
            InstantiateAndAssignMaterial();
        }
    }
}
