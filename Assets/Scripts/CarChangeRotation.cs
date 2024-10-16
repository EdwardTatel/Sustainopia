using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChangeRotation : MonoBehaviour
{
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private bool hasTriggeredSection1 = false;
    private bool hasTriggeredSection2 = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Section2" && !hasTriggeredSection2)
        {
            hasTriggeredSection2 = true;  // Set to true so it doesn't trigger again
            LeanTween.rotateY(camera.gameObject, 146.92f, 1f).setEase(LeanTweenType.easeInOutQuad);
            hasTriggeredSection1 = false;
        }
        else if (other.name == "Section1" && !hasTriggeredSection1)
        {
            hasTriggeredSection1 = true;  // Set to true so it doesn't trigger again
            LeanTween.rotateY(camera.gameObject, 57.33f, 1f).setEase(LeanTweenType.easeInOutQuad);
            hasTriggeredSection2 = false;
        }
    }

}
