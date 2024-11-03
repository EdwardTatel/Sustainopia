using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    private float speed = 13f; // Speed of movement along the -z axis
    public float swayAmplitude = 1f; // How much it sways on the x axis
    public float swayFrequency = 1f; // How fast it sways

    private Vector3 initialPosition;
    private float swayOffset;


    public GameObject cloud;

    void Start()
    {
        switch (MicroGameVariables.GetDifficulty())
        {
            case MicroGameVariables.levels.medium:
                speed = 14;
                break;
            case MicroGameVariables.levels.hard:
                speed = 16;
                break;
            default:
                speed = 12;
                break;
        }
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
        if (gameObject.name == "TrashFish(Clone)") GameObject.Find("GameManager").GetComponent<FilterTrashMGManager>().gameFailed = true;
        GameObject Cloud = Instantiate(cloud, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
        Cloud.transform.Find("Text").GetComponent<TextMeshPro>().text = "CAUGHT";
        Destroy(gameObject);
    }
}
