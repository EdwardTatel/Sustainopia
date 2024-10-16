using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class MedalScript : MonoBehaviour
{
    private Animator animator;

    public int CityNumber;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CityNumber + " " + GameVariables.city1Finished);
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CityNumber == 1)
        {
            if (GameVariables.city1Finished)
            {
                animator.SetBool("Solid", true);
            }
            else
            {
                animator.SetBool("Solid", false);
            }

        }
    }
}
