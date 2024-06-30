using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public GameObject houseBlockPrefab;
    private Queue<GameObject> houseBlocks = new Queue<GameObject>();
    private GameObject currentHouseBlock;
    private int houseBlockQuantity;
    private bool linecastEnable;
    public Transform houseBlockParent;
    public bool LinecastEnable {  get { return linecastEnable; } set { linecastEnable = value; } }
    public Queue<GameObject> HouseBlocks { get {  return houseBlocks; } set { houseBlocks = value; } }


    private void Start()
    {
        currentHouseBlock = GameObject.Find("HouseBlock");
        currentHouseBlock.GetComponent<Animator>().SetTrigger("Current");
        houseBlockQuantity = 5;
        InitializeStack();
        NextQueueItem();
    }

    private void Update()
    {
        ChangeCurrentHouseBlock();
    }
    void InitializeStack()
    {
        houseBlocks.Enqueue(GameObject.Find("HouseBlock"));
        for (int i = 0; i < houseBlockQuantity; i++)
        {
            currentHouseBlock = AddHouseBlock(currentHouseBlock);
            houseBlocks.Enqueue(currentHouseBlock);
        }
    }
    private GameObject AddHouseBlock(GameObject houseBlock)
    {
        GameObject newHouseBlock;
        newHouseBlock = Instantiate(houseBlockPrefab, houseBlock.transform.position + new Vector3(0,14,0), houseBlock.transform.rotation, houseBlockParent);

        return newHouseBlock;
    }

    void ChangeCurrentHouseBlock()
    {
        if(houseBlocks.Count > 0 ){
            if(currentHouseBlock.GetComponent<HouseBlock>().hitObject != null)
            {
                GameObject hitObject = currentHouseBlock.GetComponent<HouseBlock>().hitObject;
                if (currentHouseBlock.GetComponent<HouseBlock>().hitObject.name == "Material(Clone)")
                {
                    Destroy(hitObject);
                    houseBlocks.Dequeue();
                    currentHouseBlock.GetComponent<HouseBlock>().Changeable = false;
                    StartCoroutine(WaitAndProceed());

                }
            }
        }
    }
    

    void NextQueueItem()
    {
        currentHouseBlock = houseBlocks.Peek();
        currentHouseBlock.GetComponent<HouseBlock>().Changeable = true;
        currentHouseBlock.GetComponent<Animator>().SetTrigger("Current");
    }


    IEnumerator WaitAndProceed()
    {
        yield return new WaitForSeconds(.01f);
        if (houseBlocks.Count > 0) NextQueueItem();
    }

}
