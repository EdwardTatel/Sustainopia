using UnityEngine;

public class ChangeImageOrder : MonoBehaviour
{
    public GameObject imageToMoveBehind; // The image you want behind
    public GameObject imageToMoveFront;  // The image you want on top

    // Call this function to move 'imageToMoveBehind' behind 'imageToMoveFront'
    public void MoveBehind()
    {
        imageToMoveBehind.transform.SetSiblingIndex(0); // Move the object to the back
        imageToMoveFront.transform.SetSiblingIndex(1);  // Move the other object on top
    }

    // Call this function to move 'imageToMoveFront' to the back
    public void MoveInFront()
    {
        imageToMoveFront.transform.SetAsLastSibling(); // Make it the last rendered (on top)
    }
}