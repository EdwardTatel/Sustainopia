using UnityEngine;

public class NetFish : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float jumpForce = 13f; 
    private Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ocean")
        {
            Destroy(this.gameObject);
        }
        else if (collision.contacts[0].normal.y > 0.5f)
        {
            Vector3 jumpDirection = Vector3.up + collision.contacts[0].normal; 
            if(rb != null) rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse); 
        }
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(curPosition.x, 4, curPosition.z);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPoint.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
