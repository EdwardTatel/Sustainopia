using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilObject : MonoBehaviour
{
    public Sprite newSprite;
    private GameObject gameCamera;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameCamera = GameObject.Find("GameCamera");
        
    }
    void OnMouseDown()
    {
        gameObject.tag = "Planted";
        spriteRenderer.sprite = newSprite;
        gameCamera.GetComponent<CameraMove>().CheckSoilObjects();

    }
}
