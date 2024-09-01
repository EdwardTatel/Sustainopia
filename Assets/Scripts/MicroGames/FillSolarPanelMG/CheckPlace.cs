using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CheckPlace : MonoBehaviour
{
    public bool placeable;
    public GameObject collidedObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PanelChecker" && other.GetComponent<PanelChecker>().enablePlace)
        {
            collidedObject = other.gameObject;
            placeable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PanelChecker" && other.GetComponent<PanelChecker>().enablePlace)
        {
            collidedObject = other.gameObject;
            placeable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        placeable = false;
        collidedObject = null;
        
    }
}
