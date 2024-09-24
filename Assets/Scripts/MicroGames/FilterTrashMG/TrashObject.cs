using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    private float speed = 10f; // Speed of movement along the -z axis
    public float swayAmplitude = 1f; // How much it sways on the x axis
    public float swayFrequency = 1f; // How fast it sways

    private Vector3 initialPosition;
    private float swayOffset;

    void Start()
    {
        initialPosition = transform.position;
        swayOffset = Random.Range(-50f, 50f * Mathf.PI); // Random offset for each object
    }

    void Update()
    {
        // Move the object along the -z axis
        transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
        // Sway the object on the x axis with an offset
        float sway = Mathf.Sin(Time.time * swayFrequency + swayOffset) * swayAmplitude;
        transform.position = new Vector3(initialPosition.x + sway, transform.position.y, transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        Destroy(gameObject);
    }
}
