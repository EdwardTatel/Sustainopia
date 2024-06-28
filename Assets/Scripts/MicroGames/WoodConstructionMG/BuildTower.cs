using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public GameObject houseBlockPrefab;
    private Stack<GameObject> houseBlockStacks;
    private int houseBlockQuantity;

    void InitializeStack()
    {
        
    }
    void AddHouseBlock()
    {

        Vector3 newPositionToSpawn = houseBlockStacks.Peek().transform.position + new Vector3(0, 14, 0);
        Instantiate(houseBlockPrefab, newPositionToSpawn, houseBlockStacks.Peek().transform.rotation);
    }
}
