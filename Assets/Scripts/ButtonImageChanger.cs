using UnityEngine;
using UnityEngine.UI;

public class ButtonImageChanger : MonoBehaviour
{
    public Image targetImage;  // The image component to change
    public Sprite newSprite;   // The new sprite to set when the button is clicked

    // This method will be called when the button is clicked
    public void ChangeImage()
    {
        if (targetImage != null && newSprite != null)
        {
            targetImage.sprite = newSprite;  // Change the image sprite
        }
    }
}