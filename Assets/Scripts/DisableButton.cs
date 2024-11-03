using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    private Button myButton;  // Reference to the Button component
    void Start()
    {
        myButton = GetComponent<Button>();
        // Ensure the button is enabled at the start
        myButton.interactable = true;

        // Add listener to the button to disable it permanently after click
        myButton.onClick.AddListener(OnButtonClick);
    }   

    // This method will be called when the button is clicked
    void OnButtonClick()
    {
        // Disable the button permanently
        myButton.interactable = false;
    }
}
