using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Splash : MonoBehaviour
{
    // Time in seconds before the object is destroyed
    public float lifetime = 0.2f;

    void Start()
    {
        // Expanding with easeOutElastic for a more natural feel
        LeanTween.moveLocal(gameObject, gameObject.transform.position += new Vector3(0, .5f, 0), 0.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(gameObject, new Vector3(15, 15, 15), 0.1f)
                 .setEase(LeanTweenType.easeOutElastic)
                 .setOnComplete(DissapearSplash);
        
    }

    void DissapearSplash()
    {
        // Shrinking with easeInQuad for a smoother end
        LeanTween.moveLocal(gameObject, gameObject.transform.position += new Vector3(0, -.1f, 0), 0.1f).setEase(LeanTweenType.easeInQuad);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.3f)
                 .setEase(LeanTweenType.easeInQuad)
                 .setOnComplete(DestroyObject);
    }

    void DestroyObject()
    {
        // Destroy the object after the shrinking animation
        Destroy(gameObject, lifetime);
    }
}
