using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset; 
    public float smoothTime = 0.3f; 

    private Vector3 velocity = Vector3.zero;
    void LateUpdate()
    {
        if (target != null)
        {
            
            Vector3 desiredPosition = target.position + offset;

            
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            
            transform.position = smoothedPosition;
        }
    }
}   