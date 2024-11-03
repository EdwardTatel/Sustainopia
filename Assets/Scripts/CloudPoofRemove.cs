using UnityEngine;

public class CloudPoofRemove : MonoBehaviour
{
    public float lifetime = 0.2f;
    public string text;
    private Transform cameraTransform;

    void Start()
    {
        // Find the main camera in the scene
        cameraTransform = Camera.main.transform;

        // Set the text if applicable
        Transform cloudSprite = transform.Find("CloudSprite");
        if (cloudSprite != null)
        {
            float randomZRotation = Random.Range(0f, 360f);
            cloudSprite.localRotation = Quaternion.Euler(0, 0, randomZRotation);
        }
        // Initial animation with LeanTween
        LeanTween.moveLocal(gameObject, gameObject.transform.position + new Vector3(0, .5f, 0), 0.1f)
            .setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(gameObject, new Vector3(15, 15, 15), 0.1f)
            .setEase(LeanTweenType.easeOutElastic)
            .setOnComplete(DissapearSplash);
    }

    void Update()
    {
        // Face the center of the camera each frame
        if (cameraTransform != null)
        {
            transform.LookAt(transform.position + cameraTransform.forward);
        }
    }

    void DissapearSplash()
    {
        // Shrink with LeanTween before destroying
        LeanTween.moveLocal(gameObject, gameObject.transform.position + new Vector3(0, -.1f, 0), 0.1f)
            .setEase(LeanTweenType.easeInQuad);
        LeanTween.scale(gameObject, Vector3.zero, 0.3f)
            .setEase(LeanTweenType.easeInQuad)
            .setOnComplete(DestroyObject);
    }

    void DestroyObject()
    {
        Destroy(gameObject, lifetime);
    }
}