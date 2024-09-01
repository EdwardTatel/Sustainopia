using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCheckers : MonoBehaviour
{
    public GameObject panelChecker;
    public 
    // Start is called before the first frame update
    void Start()
    {
        InstantiatePanelCheckers();
    }

    private void InstantiatePanelCheckers()
    {
        for(int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 10; j++)
            {   
                Instantiate(panelChecker, transform.position + new Vector3(j * 0.12f, 0, i * -0.12f), Quaternion.Euler(90,0,0), transform);
            }
        }
    }
}
