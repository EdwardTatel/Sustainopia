using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class OutcomeAnimations : MonoBehaviour
{

    public RectTransform outcomeTransform;
    public GameObject GoodImage;
    public GameObject BadImage;
    private Animator SDGImageAnimator;
    public Sprite GoodReleaseFishImage;
    public Sprite BadReleaseFishImage;
    public Sprite GoodFilterTrashImage;
    public Sprite BadFilterTrashImage;
    public Sprite GoodIllegalFishingImage;
    public Sprite BadIllegalFishingImage;
    public TextMeshProUGUI percentageText;
    private int Difficulty = 1;
    [SerializeField] private TextMeshProUGUI LifeBelowWaterText;
    [SerializeField] private TextMeshProUGUI ClimateActionText;
    [SerializeField] private TextMeshProUGUI LifeOnLandText;
    [SerializeField] private TextMeshProUGUI LifeBelowWaterDoneText;


    private DialogueManager ReleaseFishSuccessDialog;
    private DialogueManager FilterTrashSuccessDialog;
    private DialogueManager IllegalFishingSuccessDialog;

    private DialogueManager ReleaseFishFailDialog;
    private DialogueManager FilterTrashFailDialog;
    private DialogueManager IllegalFishingFailDialog;


    // Duration of the fade animation
    public float fadeDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        MicroGameVariables.SDGNum = 1;
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
        FadeInText();
        ReleaseFishSuccessDialog = GameObject.Find("ReleaseFishSuccessDialog").GetComponent<DialogueManager>();
        FilterTrashSuccessDialog = GameObject.Find("FilterTrashSuccessDialog").GetComponent<DialogueManager>();
        IllegalFishingSuccessDialog = GameObject.Find("IllegalFishingSuccessDialog").GetComponent<DialogueManager>();

        ReleaseFishFailDialog = GameObject.Find("ReleaseFishFailDialog").GetComponent<DialogueManager>();
        FilterTrashFailDialog = GameObject.Find("FilterTrashFailDialog").GetComponent<DialogueManager>();
        IllegalFishingFailDialog = GameObject.Find("IllegalFishingFailDialog").GetComponent<DialogueManager>();


    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetOutcomes()
    {

        changeOutcomeImages();

        int gameStats = 0;
        int prevGameStats = 0;
        switch (MicroGameVariables.MGNum)
        {
            case 0:
                gameStats = MicroGameVariables.game1Stats;
                prevGameStats = MicroGameVariables.prevGame1Stats;
                break;
            case 1:
                gameStats = MicroGameVariables.game2Stats;
                prevGameStats = MicroGameVariables.prevGame2Stats;
                break;
            case 2:
                gameStats = MicroGameVariables.game3Stats;
                prevGameStats = MicroGameVariables.prevGame3Stats;
                break;
        }
        outcomeTransform.localPosition = new Vector3(prevGameStats * 90f, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
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
        switch (MicroGameVariables.MGNum)
        {
            case 0:
                gameStats = MicroGameVariables.game1Stats;
                prevGameStats = MicroGameVariables.prevGame1Stats;
                break;
            case 1:
                gameStats = MicroGameVariables.game2Stats;
                prevGameStats = MicroGameVariables.prevGame2Stats;
                break;
            case 2:
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
        outcomeTransform.localPosition = new Vector3(prevGameStats * 90f, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
        if (MicroGameVariables.MGNum == 2 && Difficulty == 3)
        {
            LeanTween.move(outcomeTransform, new Vector2(gameStats * 90f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlayEndAnimation);
            AnimatePercentage(prevPercentage, percentage);
        }
        else
        {
            LeanTween.move(outcomeTransform, new Vector2(gameStats * 90f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlaySDGAnimation);
            AnimatePercentage(prevPercentage, percentage);
        }
        nextMG();
    }

    public void SetFinalOutcomes()
    {
        changeOutcomeImages();

        int gameStats = 0;
        switch (MicroGameVariables.MGNum)
        {
            case 0:
                gameStats = MicroGameVariables.game1Stats;
                break;
            case 1:
                gameStats = MicroGameVariables.game2Stats;
                break;
            case 2:
                gameStats = MicroGameVariables.game3Stats;
                break;
        }
        outcomeTransform.localPosition = new Vector3(gameStats * 90f, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
        float percentage = gameStats + 3;
        percentage = percentage / 6;
        percentage = percentage * 100;
        percentageText.text = percentage.ToString("F1") + "%";
    }

    public void changeOutcomeImages()
    {
        switch (MicroGameVariables.SDGNum)
        {
            case 1:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadReleaseFishImage;
                        break;
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodFilterTrashImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadFilterTrashImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadReleaseFishImage;
                        break;
                }
                break;
            case 2:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                }
                break;
            case 3:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                }
                break;
        }
    }
    private void LevelFinished()
    {
        int gameStats = 0;
        switch (MicroGameVariables.MGNum)
        {
            case 0:
                gameStats = MicroGameVariables.game1Stats;
                break;
            case 1:
                gameStats = MicroGameVariables.game2Stats;
                break;
            case 2:
                gameStats = MicroGameVariables.game3Stats;
                break;
        }

        float percentage = gameStats + 3;
        percentage = percentage / 6;
        percentage = percentage * 100;
        EndFadeOutText();
        if (percentage > 50)
        {
            LeanTween.move(outcomeTransform, new Vector2(405f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad);
            AnimatePercentage(percentage, 100);
            StartCoroutine(RunEndDialogue());

        }
        else
        {
            LeanTween.move(outcomeTransform, new Vector2(-405f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad);
            AnimatePercentage(percentage, 0);
            StartCoroutine(RunEndDialogue());
        }

    }


    IEnumerator RunEndDialogue()
    {
        yield return new WaitForSeconds(1f);
        int gameStats = 0;
        Cursor.visible = true;
        switch (MicroGameVariables.SDGNum)
        {
            case 1:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:

                        gameStats = MicroGameVariables.game1Stats;
                        if (gameStats > 0) ReleaseFishSuccessDialog.StartConversation();
                        else ReleaseFishFailDialog.StartConversation();
                        nextMG();
                        break;
                    case 1:
                        gameStats = MicroGameVariables.game2Stats;
                        if (gameStats > 0) FilterTrashSuccessDialog.StartConversation();
                        else FilterTrashFailDialog.StartConversation();
                        nextMG();
                        break;
                    case 2:
                        gameStats = MicroGameVariables.game3Stats;
                        if (gameStats > 0) IllegalFishingSuccessDialog.StartConversation();
                        else IllegalFishingFailDialog.StartConversation();
                        GameVariables.city1Finished = true;
                        GameVariables.dialogStarted = 1;
                        break;

                }
                break;
        }

    }

    public void BackToLevelSelect()
    {
        GameObject.Find("SceneManagement").GetComponent<SceneManagement>().BackToLevelSelect();
    }
    public void PlayEndAnimation()
    {
        StartCoroutine(RunEndAnimationAfterDelay());
    }

    IEnumerator RunEndAnimationAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(.5f);
        Debug.Log(MicroGameVariables.SDGNum);
        // Call the method after the delay
        switch (MicroGameVariables.SDGNum)
        {
            case 1:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:
                        SDGImageAnimator.Play("LifeBelowWaterLevelClear");
                        break;
                    case 1:
                        SDGImageAnimator.Play("Empty");
                        break;
                    case 2:
                        SDGImageAnimator.Play("Empty");
                        break;
                }
                break;
            case 2:
                break;
            case 3:
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
        StartCoroutine(AnimateValue(initialValue, finalValue));
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


    public void EndFadeOutText()
    {
        LifeBelowWaterText.color = new Color(0, 0, 0, 0);
        // Store the current color of the text
        Color originalColor = percentageText.color;
        // Animate the alpha from 1 to 0 (fade out)
        LeanTween.value(gameObject, 1f, 0f, fadeDuration)
                 .setOnUpdate((float alpha) =>
                 {
                     // Set the color with updated alpha
                     percentageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                 });


    }

    public void EndFadeInText()
    {
        LifeBelowWaterText.color = new Color(0, 0, 0, 0);
        // Store the current color of the text
        Color originalColor = percentageText.color;
        // Animate the alpha from 0 to 1 (fade in)
        LeanTween.value(gameObject, 0f, 1f, .5f)
                 .setOnUpdate((float alpha) =>
                 {
                     // Set the color with updated alpha
                     percentageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                 });

    }

    public void FadeOutText()
    {
        // Store the current color of the text
        Color originalColor = percentageText.color;
        // Animate the alpha from 1 to 0 (fade out)
        LeanTween.value(gameObject, 1f, 0f, fadeDuration)
                 .setOnUpdate((float alpha) =>
                 {
                     // Set the color with updated alpha
                     percentageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                 });
        Color originalColor2 = LifeBelowWaterDoneText.color;
        LeanTween.value(gameObject, 1f, 0f, fadeDuration)
                 .setOnUpdate((float alpha) =>
                 {
                     // Set the color with updated alpha
                     LifeBelowWaterDoneText.color = new Color(originalColor2.r, originalColor2.g, originalColor2.b, alpha);
                 });

    }

    public void FadeInText()
    {
        // Store the current color of the text
        Color originalColor = percentageText.color;
        // Animate the alpha from 0 to 1 (fade in)
        LeanTween.value(gameObject, 0f, 1f, fadeDuration)
                 .setOnUpdate((float alpha) =>
                 {
                     // Set the color with updated alpha
                     percentageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                 });
        Color originalColor2 = LifeBelowWaterDoneText.color;
        LeanTween.value(gameObject, 0f, 1f, fadeDuration)
                 .setOnUpdate((float alpha) =>
                 {
                     // Set the color with updated alpha
                     LifeBelowWaterDoneText.color = new Color(originalColor2.r, originalColor2.g, originalColor2.b, alpha);
                 });
    }

    public void nextMG()
    {
        if (MicroGameVariables.MGNum == 2)
        {
            MicroGameVariables.DifficultyChange();
            Difficulty++;
            MicroGameVariables.MGNum = 0;
        }
        else
        {
            MicroGameVariables.MGNum++;
        }
    }

    public void SetWinDialog(int dialog)
    {
        GameVariables.dialogStarted = dialog;
    }

    public void ChangeTextOnFinish()
    {
        LifeBelowWaterText.text = "Level Clear";
        ClimateActionText.text = "Level Clear";
        LifeOnLandText.text = "Level Clear";
    }
}
