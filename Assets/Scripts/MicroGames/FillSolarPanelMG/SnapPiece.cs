using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class SnapPiece : MonoBehaviour
{
    private SolarPanelScript solarPanelScript;
    private CheckPlace childObjectScript;
    public GameObject initialPosition;

    private void Start()
    {
        childObjectScript = transform.Find("SolarPanel").GetComponent<CheckPlace>();
    }

    public bool IsPlaceable()
    {
        
        foreach (Transform obj in transform)
        {
            
            if (!obj.GetComponent<CheckPlace>().placeable) {
                return false;
            }
        }
        return true;
    }

    public void SnapToPlace()
    {
        if (IsPlaceable())
        {
            transform.position = childObjectScript.collidedObject.transform.position;
            foreach(Transform obj in transform)
            {
                obj.GetComponent<CheckPlace>().collidedObject.GetComponent<PanelChecker>().enablePlace = false;
            }
        }
        else
        {
            LeanTween.move(gameObject, initialPosition.transform.position, 0.5f);
        }
    }
}
    