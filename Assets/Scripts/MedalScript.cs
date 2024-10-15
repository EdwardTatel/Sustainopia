using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MedalScript : MonoBehaviour
{
    private Color color;
    public int CityNumber;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(CityNumber == 1)
        {
            if (GameVariables.city1Finished)
            {
                color.a = 1;
            }
            else
            {
                color.a = .5f;
            }

        }
    }
}
