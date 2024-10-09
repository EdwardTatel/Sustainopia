using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveOcean : MonoBehaviour
{
    private int speed = 16;


    private void Start()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.medium:
                speed = 14;
                break;
            case MicroGameVariables.levels.hard:
                speed = 16;
                break;
            default:
                speed = 12;
                break;
        }
    }
    void Update()
    {
        // Move the object along the -z axis
        transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);

    }
}
