using UnityEngine;

public class CheckLight : MonoBehaviour
{
    private Camera mainCamera;        // Reference to the main camera
    public float checkDuration = 2f;  // Time in seconds before the object disappears
    private float timer = 0f;

    void Start()
    {
        mainCamera = GameObject.Find("GameCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // Perform the linecast
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        RaycastHit hit;

        // Draw the linecast (in Scene view) using Debug.DrawLine
        Debug.DrawLine(transform.position, mainCamera.transform.position, Color.red);

        if (Physics.Linecast(transform.position, mainCamera.transform.position, out hit))
        {
            // If there's something between the object and the camera, reset the timer
            if (hit.collider.gameObject != mainCamera.gameObject)  // Ignore the camera itself
            {
                timer = 0f;  // Reset timer
            }
        }
        else
        {
            // If nothing is between the object and the camera, start the timer
            timer += Time.deltaTime;

            // If the line is clear for the duration, make the object disappear
            if (timer >= checkDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }

    



}
