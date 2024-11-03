using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    private Button myButton;  // Reference to the Button component
    private Button myButton2;
    private Button myButton3;
    void Start()
    {
        myButton = GetComponent<Button>();
        // Ensure the button is enabled at the start
        myButton.interactable = true;

        myButton2 = GameObject.Find("Button (1)").GetComponent<Button>();
        // Ensure the button is enabled at the start
        myButton2.interactable = true;

        myButton3 = GameObject.Find("Button (2)").GetComponent<Button>();
        // Ensure the button is enabled at the start
        myButton3.interactable = true;
        // Add listener to the button to disable it permanently after click
        myButton.onClick.AddListener(OnButtonClick);
        myButton2.onClick.AddListener(OnButtonClick);
        myButton3.onClick.AddListener(OnButtonClick);
    }

    // This method will be called when the button is clicked
    void OnButtonClick()
    {
        // Disable the button permanently
        myButton.interactable = false;
        myButton2.interactable = false;
        myButton3.interactable = false;
    }
}