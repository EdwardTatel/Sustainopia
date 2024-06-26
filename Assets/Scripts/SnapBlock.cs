using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBlock : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WoodBlock (1)")   
        {
            Debug.Log(gameObject.transform.parent.gameObject);
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            otherRigidbody.isKinematic = true;  
            other.transform.Translate(this.gameObject.transform.localPosition);
            Destroy(this.gameObject);
        }
    }

}
