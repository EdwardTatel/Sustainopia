using UnityEngine;

public class EnableUIOnProximity : MonoBehaviour
{
    public Transform car; 
    public float activationDistance = 10f;
    public Animator animator;
    private bool keepHidden;
    private void Start()
    {
        animator = GameObject.Find("DialogueBox").GetComponent<Animator>(); 
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, car.position) <= activationDistance)
        {
            SetChildrenActive(true);
        }
        else
        {
            SetChildrenActive(false);
        }
    }

    void SetChildrenActive(bool isActive)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}