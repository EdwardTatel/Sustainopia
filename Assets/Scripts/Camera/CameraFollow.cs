using System.Collections;
using UnityEngine;
using UnityEngine.Accessibility;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; 
    public float smoothTime = 0.3f;
    private bool instaChagePosition = true;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (GameVariables.city1Finished || GameVariables.city2Finished || GameVariables.city3Finished)
        {
            if (instaChagePosition)
            {
                Vector3 desiredPosition = target.position + offset;
                transform.position = desiredPosition;
                instaChagePosition = false;
            }
        }
        
        if (target != null)
        {
            
            Vector3 desiredPosition = target.position + offset;

            
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            
            transform.position = smoothedPosition;
        }
    }
}   