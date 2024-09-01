using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChecker : MonoBehaviour
{
    public bool enablePlace;

    private void Start()
    {
        enablePlace = true;
    }

    private void OnTriggerStay(Collider other)
    {
        enablePlace = false;
    }

    private void OnTriggerExit(Collider other)
    {
        enablePlace = true;
    }
}
