using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOcean : MonoBehaviour
{
    private int speed = 10;
    void Update()
    {
        // Move the object along the -z axis
        transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);

    }
}
