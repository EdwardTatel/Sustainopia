using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class OutcomeAnimations : MonoBehaviour
{

    public RectTransform outcomeTransform;
    public GameObject GoodImage;
    public GameObject BadImage;
    private Animator SDGImageAnimator;
    public Sprite GoodReleaseFishImage;
    public Sprite BadReleaseFishImage;
    public TextMeshProUGUI percentageText;
    private int MGNum = 1;
    private int Difficulty = 1;
    [SerializeField] private TextMeshProUGUI LifeBelowWaterText;
    [SerializeField] private TextMeshProUGUI ClimateActionText;
    [SerializeField] private TextMeshProUGUI LifeOnLandText;

    // Duration of the fade animation
    public float fadeDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
        FadeInText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetOutcomes()
    {
        switch (MicroGameVariables.SDGNum)
        {
            case 1:
                switch (MGNum)
                {
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadReleaseFishImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = BadReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 3:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadReleaseFishImage;
                        break;
                }
                break;
            case 2:
                switch (MGNum)
                {
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 3:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                }
                break;
            case 3:
                switch (MGNum)
                {
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 3:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                }
                break;
        }

        int gameStats = 0;
        int prevGameStats = 0;
        switch (MGNum)
        {
            case 1:
                gameStats = MicroGameVariables.game1Stats;
                prevGameStats = MicroGameVariables.prevGame1Stats;
                break;
            case 2:
                gameStats = MicroGameVariables.game2Stats;
                prevGameStats = MicroGameVariables.prevGame2Stats;
                break;
            case 3:
                gameStats = MicroGameVariables.game3Stats;
                prevGameStats = MicroGameVariables.prevGame3Stats;
                break;
        }
        outcomeTransform.localPosition = new Vector3(prevGameStats * 6708.6f, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
        // Calculate the percentage
        float percentage = prevGameStats + 3;
        percentage = percentage / 6;
        percentage = percentage * 100;
        percentageText.text = percentage.ToString("F1") + "%";

    }
    public void AnimateOutcomes()
    {
        int gameStats = 0;
        int prevGameStats = 0;
        switch (MGNum)
        {
            case 1:
                gameStats = MicroGameVariables.game1Stats;
                prevGameStats = MicroGameVariables.prevGame1Stats;
                break;
            case 2:
                gameStats = MicroGameVariables.game2Stats;
                prevGameStats = MicroGameVariables.prevGame2Stats;
                break;
            case 3:
                gameStats = MicroGameVariables.game3Stats;
                prevGameStats = MicroGameVariables.prevGame3Stats;
                break;
        }
        float percentage = gameStats + 3;
        percentage = percentage / 6;
        percentage = percentage * 100;

        float prevPercentage = prevGameStats + 3;
        prevPercentage = prevPercentage / 6;
        prevPercentage = prevPercentage * 100;
        outcomeTransform.localPosition = new Vector3(prevGameStats * 6708.6f, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
        if (MGNum == 1 && Difficulty == 1)
        {
            LeanTween.move(outcomeTransform, new Vector2(gameStats * 6708.6f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlayEndAnimation);
            AnimatePercentage(prevPercentage, percentage);
        }
        else
        {
            LeanTween.move(outcomeTransform, new Vector2(gameStats * 6708.6f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlaySDGAnimation);
            AnimatePercentage(prevPercentage, percentage);
        }

        if (MGNum == 3)
        {
            Difficulty++;
            MGNum = 1;
        } /*else MGNum++;*/

    }

    public void SetFinalOutcomes()
    {
        FadeInText();
        switch (MicroGameVariables.SDGNum)
        {
            case 1:
                switch (MGNum)
                {
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadReleaseFishImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = BadReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 3:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadReleaseFishImage;
                        break;
                }
                break;
            case 2:
                switch (MGNum)
                {
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 3:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                }
                break;
            case 3:
                switch (MGNum)
                {
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 3:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                }
                break;
        }

        int gameStats = 0;
        int prevGameStats = 0;
        switch (MGNum)
        {
            case 1:
                gameStats = MicroGameVariables.game1Stats;
                break;
            case 2:
                gameStats = MicroGameVariables.game2Stats;
                break;
            case 3:
                gameStats = MicroGameVariables.game3Stats;
                break;
        }
        Debug.Log(MGNum + "MGNum");
        outcomeTransform.localPosition = new Vector3(gameStats * 3.4f, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
        float percentage = gameStats + 3;
        percentage = percentage / 6;
        percentage = percentage * 100;
        percentageText.text = percentage.ToString("F1") + "%";
        Debug.Log(percentage + "percentage");
        Debug.Log(gameStats + "gameStats");
    }
    private void LevelFinished()
    {
        int gameStats = 0;
        switch (MGNum)
        {
            case 1:
                gameStats = MicroGameVariables.game1Stats;
                break;
            case 2:
                gameStats = MicroGameVariables.game2Stats;
                break;
            case 3:
                gameStats = MicroGameVariables.game3Stats;
                break;
        }

        float percentage = gameStats + 3;
        Debug.Log(percentage);
        percentage = percentage / 6;
        percentage = percentage * 100;
        FadeOutText();
        if (percentage > 50)
        {
            LeanTween.move(outcomeTransform, new Vector2(20.3f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad);
            AnimatePercentage(percentage, 100);
            //Play ASSISTANT DIALOGUE HERE
        }
        else{
            LeanTween.move(outcomeTransform, new Vector2(-20.3f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad);
            AnimatePercentage(percentage, 0);
        }

    }

    private void PlayEndAnimation()
    {
        StartCoroutine(RunEndAnimationAfterDelay());
    }

    IEnumerator RunEndAnimationAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(.5f);

        // Call the method after the delay

        switch (MicroGameVariables.SDGNum)
        {
            case 1:
                LifeBelowWaterText.text = "Level Clear!";
                SDGImageAnimator.Play("LifeBelowWaterLevelClear");
                break;
            case 2:
                LifeBelowWaterText.text = "Level Clear!";
                SDGImageAnimator.Play("LifeBelowWaterLevelClear");
                break;
            case 3:
                LifeBelowWaterText.text = "Level Clear!";
                SDGImageAnimator.Play("LifeBelowWaterLevelClear");
                break;
        }
    }

    private void PlaySDGAnimation()
    {
        StartCoroutine(RunAnimationAfterDelay());
    }

    IEnumerator RunAnimationAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(.5f);

        // Call the method after the delay
        SDGImageAnimator.Play("LifeBelowWater");
    }

    public void AnimatePercentage(float initialValue, float finalValue)
    {
        // Start the coroutine to animate the change
        StartCoroutine(AnimateValue(initialValue,finalValue));
    }

    private IEnumerator AnimateValue(float initialValue, float finalValue)
    {
        float elapsedTime = 0f;

        // While the animation duration hasn't been reached, update the percentage
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the interpolated value between the start and target percentages
            float currentPercentage = Mathf.Lerp(initialValue, finalValue, elapsedTime / 1);

            // Update the UI element to display the current percentage
            percentageText.text = currentPercentage.ToString("F1") + "%";

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final value is set
        percentageText.text = finalValue.ToString("F1") + "%";
    }


    public void FadeOutText()
    {
        // Store the current color of the text
        Color originalColor = percentageText.color;
        // Animate the alpha from 1 to 0 (fade out)
        LeanTween.value(gameObject, 1f, 0f, fadeDuration)
                 .setOnUpdate((float alpha) => {
                     // Set the color with updated alpha
                     percentageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                 });
    }

    public void FadeInText()
    {
        // Store the current color of the text
        Color originalColor = percentageText.color;
        // Animate the alpha from 0 to 1 (fade in)
        LeanTween.value(gameObject, 0f, 1f, fadeDuration)
                 .setOnUpdate((float alpha) => {
                     // Set the color with updated alpha
                     percentageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                 });
    }
}
