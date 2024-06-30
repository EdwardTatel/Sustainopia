using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBlock : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Boolean changeable = false;
    public GameObject hitObject;
    public GameObject gameManager;

    public Boolean Changeable
    {
        get { return changeable; }
        set { changeable = value; }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        if (changeable && gameManager.GetComponent<BuildTower>().LinecastEnable)
        {
            PerformLinecast(transform.Find("HouseBlockModel").transform.position + new Vector3(0, 0, -6), Camera.main.transform.position);
        }
    }

    void PerformLinecast(Vector3 startPoint, Vector3 endPoint)
    {
        RaycastHit hitInfo;

        if (Physics.Linecast(startPoint, endPoint, out hitInfo))
        {
            hitObject = hitInfo.collider.gameObject;

            if (hitObject.transform.Find("MaterialSprite") != null)
            {
                if (hitObject.transform.Find("MaterialSprite").GetComponent<SpriteRenderer>().sprite.name == "WoodBundle")
                {
                    animator.SetTrigger("Wood");
                }
                else
                {
                    animator.SetTrigger("Concrete");
                }
            }

            Renderer renderer = hitObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }
        }

        Debug.DrawLine(startPoint, endPoint, Color.green);
    }
    

}
