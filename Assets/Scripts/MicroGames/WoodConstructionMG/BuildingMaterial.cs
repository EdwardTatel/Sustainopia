using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingMaterial : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private GameObject gameManager;
    public float tpSpeed = 5f;

    private void Start()    
    {
        gameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameManager.GetComponent<BuildTower>().LinecastEnable = false;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                    offset = transform.position - GetMouseWorldPos();
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            gameManager.GetComponent<BuildTower>().LinecastEnable = false;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = new Vector3(curPosition.x, curPosition.y, 171.9f);
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gameManager.GetComponent<BuildTower>().LinecastEnable = true;
            StartCoroutine(WaitAndProceed());
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPoint.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "WasteBinDeleter")
        {
            Destroy(gameObject);
        }else if(other.name == "Floor")
        {

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(27.3799992f, 2.97474289f, 171.899994f), tpSpeed);
        }

    }

    IEnumerator WaitAndProceed()
    {
        yield return new WaitForSeconds(.1f);
        gameManager.GetComponent<BuildTower>().LinecastEnable = false;
    }
}
