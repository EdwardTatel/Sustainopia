using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteBinCatcher : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Material(Clone)")
        {
            other.transform.position = Vector3.Lerp(other.transform.position, transform.position, 4.0f * Time.deltaTime);
        }
    }
}
