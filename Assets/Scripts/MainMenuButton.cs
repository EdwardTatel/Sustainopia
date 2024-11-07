using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    private Button myButton;  // Reference to the Button component
    private Button myButton2;
    private Button myButton3; 
    private Button backButton;
    public CanvasGroup canvasGroup;
    void Start()
    {

        canvasGroup.alpha = 1;
        myButton = GetComponent<Button>();
        // Ensure the button is enabled at the start
        myButton.interactable = true;

        myButton2 = GameObject.Find("Button (2)").GetComponent<Button>();
        // Ensure the button is enabled at the start
        myButton2.interactable = true;

        myButton3 = GameObject.Find("Button (1)").GetComponent<Button>();

        // Ensure the button is enabled at the start
        myButton3.interactable = true;

        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        backButton.interactable = true;
        // Add listener to the button to disable it permanently after click
        myButton.onClick.AddListener(OnButtonClick);
        myButton2.onClick.AddListener(OnButtonClick2);
        myButton3.onClick.AddListener(OnButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    // This method will be called when the button is clicked
    void OnButtonClick()
    {
        // Disable the button permanently
        myButton.interactable = false;
        myButton2.interactable = false;
        myButton3.interactable = false;
    }
    void OnButtonClick2()
    {

        Debug.Log("OnButtonClick2");
        OnButtonClick();
        FadeOut();
    }

    void enableButtons()
    {
        // Disable the button permanently
        myButton.interactable = true;
        myButton2.interactable = true;
        myButton3.interactable = true;
    }

    void OnBackButtonClick()
    {
        enableButtons();
        DisableOptions();
    }


    public void FadeIn()
    {
        // Set the starting alpha value
        canvasGroup.alpha = 0;

        // Fade in to alpha 1
        LeanTween.alphaCanvas(canvasGroup, 1, 1f);
    }

    public void FadeOut()
    {
        Debug.Log("FadeOut");
        // Fade out to alpha 0 over 1 second
        LeanTween.alphaCanvas(canvasGroup, 0, 1f).setOnComplete(enableOptions);
    }
    public void enableOptions()
    {
        Debug.Log("EnableOptions");
        // Fade out to alpha 0 over 1 second
        LeanTween.move(GameObject.Find("Volume Slider").GetComponent<RectTransform>(), new Vector3(0, 0f, 0), 1f).setEase(LeanTweenType.easeInOutQuad);
    }
    public void DisableOptions()
    {
        // Fade out to alpha 0 over 1 second
        LeanTween.move(GameObject.Find("Volume Slider").GetComponent<RectTransform>(), new Vector3(0, -451f, 0), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(FadeIn);
    }
}