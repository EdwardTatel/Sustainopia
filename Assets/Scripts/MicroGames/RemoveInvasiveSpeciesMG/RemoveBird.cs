using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemoveBird : MonoBehaviour
{
    public GameObject cloud;
    public 
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // Instantiate the Cloud prefab at the position of this object
        GameObject Cloud = Instantiate(cloud, transform.position, Quaternion.identity);

        Cloud.transform.Find("Text").GetComponent<TextMeshPro>().text = "REMOVED";
        // Destroy this game object
        Destroy(gameObject);
    }

}
