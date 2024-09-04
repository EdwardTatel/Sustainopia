using UnityEngine;

public class CheckLight : MonoBehaviour
{
    private Camera mainCamera;        // Reference to the main camera
    public float checkDuration = 4f; // Time in seconds before the object disappears
    private float timer = 0f;
    private bool isClear = false;

    void Start()
    {
        mainCamera = GameObject.Find("GameCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // Perform the linecast
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Linecast(transform.position, mainCamera.transform.position, out hit))
        {
            // If there's something between the object and the camera, reset the timer
            if (hit.collider != mainCamera.GetComponent<Collider>())
            {
                isClear = false;
                timer = 0f;
            }
        }
        else
        {
            // If nothing is between the object and the camera, start the timer
            isClear = true;
            timer += Time.deltaTime;

            // If the line is clear for the duration, make the object disappear
            if (timer >= checkDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
