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
    private bool hasTriggeredSection3 = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Section2" && !hasTriggeredSection2)
        {
            hasTriggeredSection2 = true;  // Set to true so it doesn't trigger again
            LeanTween.rotateY(camera.gameObject, 71.7f, 1f).setEase(LeanTweenType.easeInOutQuad);
            hasTriggeredSection1 = false;
            hasTriggeredSection3 = false;
        }
        else if (other.name == "Section1" && !hasTriggeredSection1)
        {
            hasTriggeredSection1 = true;  // Set to true so it doesn't trigger again
            LeanTween.rotateY(camera.gameObject, 93.6f, 1f).setEase(LeanTweenType.easeInOutQuad);
            hasTriggeredSection3 = false;
            hasTriggeredSection2 = false;
        }
        else if (other.name == "Section3" && !hasTriggeredSection3)
        {
            hasTriggeredSection3 = true;  // Set to true so it doesn't trigger again
            LeanTween.rotateY(camera.gameObject, 146.92f, 1f).setEase(LeanTweenType.easeInOutQuad);
            hasTriggeredSection1 = false;
            hasTriggeredSection2 = false;
        }
    }

}
