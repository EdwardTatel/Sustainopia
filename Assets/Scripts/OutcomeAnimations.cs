using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutcomeAnimations : MonoBehaviour
{

    public RectTransform outcomeTransform;
    public RectTransform parentOutcomeTransform;
    public GameObject GoodImage;
    public GameObject BadImage;
    private Animator SDGImageAnimator;
    private Vector3 savedAnchoredPosition;
    private Vector3 savedAnchoredPosition2;
    private Vector3 savedLocalScale;
    private Vector3 savedLocalScale2;
    public Sprite GoodReleaseFishImage;
    public Sprite BadReleaseFishImage;
    public Sprite GoodFilterTrashImage;
    public Sprite BadFilterTrashImage;
    public Sprite GoodIllegalFishingImage;
    public Sprite BadIllegalFishingImage;

    public Sprite GoodRemoveInvasiveSpeciesImage;
    public Sprite BadRemoveInvasiveSpeciesImage;
    public Sprite GoodPlantTreesImage;
    public Sprite BadPlantTreesImage;
    public Sprite GoodCatchPoachersImage;
    public Sprite BadCatchPoachersImage;

    public Sprite GoodSegregateTrashImage;
    public Sprite BadSegregateTrashImage;
    public Sprite GoodFillSolarPanelImage;
    public Sprite BadFillSolarPanelImage;
    public Sprite GoodWoodConstructionImage;
    public Sprite BadWoodConstructionImage;

    public Sprite ReleaseFishTutorialImage;
    public Sprite FilterTrashTutorialImage;
    public Sprite IllegalFishingTutorialImage;

    public Sprite PlantTreesTutorialImage;
    public Sprite RemoveInvasiveSpeciesTutorialImage;
    public Sprite CatchPoachersTutorialImage;

    public Sprite SegregateTrashTutorialImage;
    public Sprite FillSolarPanelTutorialImage;
    public Sprite WoodConstructionTutorialImage;

    public TextMeshProUGUI percentageText;
    private int Difficulty = 1;
    [SerializeField] private TextMeshProUGUI LifeBelowWaterText;
    [SerializeField] private TextMeshProUGUI ClimateActionText;
    [SerializeField] private TextMeshProUGUI LifeOnLandText;
    [SerializeField] private TextMeshProUGUI LifeBelowWaterDoneText;

    [SerializeField] private Sprite LifeBelowWaterSprite;
    [SerializeField] private Sprite ClimateActionSprite;
    [SerializeField] private Sprite LifeOnLandSprite;
    [SerializeField] private Sprite LifeBelowWaterSpriteBlank;
    [SerializeField] private Sprite ClimateActionSpriteBlank;
    [SerializeField] private Sprite LifeOnLandSpriteBlank;

    private DialogueManager ReleaseFishTutorial;
    private DialogueManager FilterTrashTutorial;
    private DialogueManager IllegalFishingTutorial;

    private DialogueManager ReleaseFishSuccessDialog;
    private DialogueManager FilterTrashSuccessDialog;
    private DialogueManager IllegalFishingSuccessDialog;

    private DialogueManager ReleaseFishFailDialog;
    private DialogueManager FilterTrashFailDialog;
    private DialogueManager IllegalFishingFailDialog;

    public GameObject tutorialImage;

    private Color initialColor1;  // Store the initial color of the first image
    private Color initialColor2;  // Store the initial color of the second image

    private GameObject SDGImage;
    // Duration of the fade animation
    public float fadeDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        MicroGameVariables.tutorial = true;
        initialColor1 = GoodImage.GetComponent<Image>().color;
        initialColor2 = BadImage.GetComponent<Image>().color;
        SDGImage = GameObject.Find("SDGImages");
        SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
        SDGImageSet();

        FadeInText();
        ReleaseFishSuccessDialog = GameObject.Find("ReleaseFishSuccessDialog").GetComponent<DialogueManager>();
        FilterTrashSuccessDialog = GameObject.Find("FilterTrashSuccessDialog").GetComponent<DialogueManager>();
        IllegalFishingSuccessDialog = GameObject.Find("IllegalFishingSuccessDialog").GetComponent<DialogueManager>();

        ReleaseFishFailDialog = GameObject.Find("ReleaseFishFailDialog").GetComponent<DialogueManager>();
        FilterTrashFailDialog = GameObject.Find("FilterTrashFailDialog").GetComponent<DialogueManager>();
        IllegalFishingFailDialog = GameObject.Find("IllegalFishingFailDialog").GetComponent<DialogueManager>();


        ReleaseFishTutorial = GameObject.Find("ReleaseFishTutorial").GetComponent<DialogueManager>();
        FilterTrashTutorial = GameObject.Find("FilterTrashTutorial").GetComponent<DialogueManager>();
        IllegalFishingTutorial = GameObject.Find("IllegalFishingTutorial").GetComponent<DialogueManager>();

        StartMicroGame();
    }
    void SDGImageSet()
    {
        switch (MicroGameVariables.SDGNum)
        {
            default:
                SDGImage.GetComponent<Image>().overrideSprite = LifeBelowWaterSprite;
                LifeBelowWaterDoneText.outlineColor = Color.blue;
                break;
            case 2:
                SDGImage.GetComponent<Image>().overrideSprite = LifeOnLandSprite;
                LifeBelowWaterDoneText.outlineColor = Color.green;
                break;
            case 3:
                SDGImage.GetComponent<Image>().overrideSprite = ClimateActionSprite;
                LifeBelowWaterDoneText.outlineColor = Color.green;
                break;
        }
    }

    void StartMicroGame()
    {
        if (MicroGameVariables.tutorial)
        {
            SDGImageAnimator.Play("Fein");
        }
        else if (!MicroGameVariables.tutorial)
        {
            SDGImageAnimator.Play("StartMG");
        }
    }

    public void ChangeNormalBG()
    {
        switch (MicroGameVariables.SDGNum)
        {
            default:
                SDGImage.GetComponent<Image>().overrideSprite = LifeBelowWaterSpriteBlank;
                break;
            case 2:
                SDGImage.GetComponent<Image>().overrideSprite = LifeOnLandSpriteBlank;
                break;
            case 3:
                SDGImage.GetComponent<Image>().overrideSprite = ClimateActionSpriteBlank;
                break;
        }
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
        outcomeTransform.localPosition = new Vector3(prevGameStats * 12024, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
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
        outcomeTransform.localPosition = new Vector3(prevGameStats * 12024, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);

        if (MicroGameVariables.MGNum == 2 && Difficulty == 3)
        {
            
            LeanTween.move(outcomeTransform, new Vector2(gameStats * 12024, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlayEndAnimation);
            AnimatePercentage(prevPercentage, percentage);
        }
        else
        {
            LeanTween.move(outcomeTransform, new Vector2(gameStats * 12024, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlaySDGAnimation);
            AnimatePercentage(prevPercentage, percentage);
        }
        nextMG();
    }

    
    public void changeOutcomeImages()
    {
        switch (MicroGameVariables.SDGNum)
        {
            
            case 2:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodPlantTreesImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadPlantTreesImage;
                        break;
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodRemoveInvasiveSpeciesImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadRemoveInvasiveSpeciesImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodCatchPoachersImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadCatchPoachersImage;
                        break;
                }
                break;
            case 3:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodSegregateTrashImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadSegregateTrashImage;
                        break;
                    case 1:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodFillSolarPanelImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadFillSolarPanelImage;
                        break;
                    case 2:
                        GoodImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodWoodConstructionImage;
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = BadWoodConstructionImage;
                        break;
                }
                break;
            default:
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
        }
    }
    public void SetFinalOutcomes()
    {
        EndFadeInText();
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
        outcomeTransform.localPosition = new Vector3(gameStats * 12024, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
        float percentage = gameStats + 3;
        percentage = percentage / 6;
        percentage = percentage * 100;
        percentageText.text = percentage.ToString("F1") + "%";

        GoodImage.GetComponent<Image>().color = initialColor1;
        BadImage.GetComponent<Image>().color = initialColor2;
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

            LeanTween.move(outcomeTransform, new Vector2(55187, outcomeTransform.localPosition.y), 2f).setEase(LeanTweenType.easeInOutQuad);
            EndAnimatePercentage(percentage, 100);
            StartCoroutine(RunEndDialogue());


        }
        else
        {

            LeanTween.move(outcomeTransform, new Vector2(-55187, outcomeTransform.localPosition.y), 2f).setEase(LeanTweenType.easeInOutQuad);
            EndAnimatePercentage(percentage, 0);
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
            
            case 2:
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
                        GameVariables.city2Finished = true;
                        GameVariables.dialogStarted = 2;

                        MicroGameVariables.restartGameStats();
                        break;

                }
                break;
            case 3:
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
                        GameVariables.city3Finished = true;
                        GameVariables.dialogStarted = 3;

                        MicroGameVariables.restartGameStats();
                        break;

                }
                break;
            default:

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
                        MicroGameVariables.restartGameStats();
                        break;

                }
                break;
        }

    }
    public void PlayBackToLevelSelectAnim()
    {
        SDGImageAnimator.Play("BackToLevelSelect");
    }
    public void BackToLevelSelect()
    {
        SceneManager.LoadSceneAsync("LevelSelect", LoadSceneMode.Additive);
    }

    public void RemoveUITransition()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("UITransition"));
    }
    public void PlayEndAnimation()
    {
        StartCoroutine(RunEndAnimationAfterDelay());
    }

    IEnumerator RunEndAnimationAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(.5f);
        switch (MicroGameVariables.MGNum)
        { 
            case 0:
                SDGImageAnimator.Play("LevelClear");
                break;
            case 1:
                SDGImageAnimator.Play("Empty");
                break;
            default:
                SDGImageAnimator.Play("Empty");
                break;
        }
    }

    public void PlaySDGAnimation()
    {
        StartCoroutine(RunAnimationAfterDelay());
    }

    IEnumerator RunAnimationAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(.5f);

        // Call the method after the delay
        StartMicroGame();
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

    public void EndAnimatePercentage(float initialValue, float finalValue)
    {
        // Start the coroutine to animate the change
        StartCoroutine(EndAnimateValue(initialValue, finalValue));
    }

    private IEnumerator EndAnimateValue(float initialValue, float finalValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;

            // Calculate interpolated values
            float currentPercentage = Mathf.Lerp(initialValue, finalValue, elapsedTime / 1);
            Color currentColor1 = Color.Lerp(initialColor1, Color.white, elapsedTime / 1);
            Color currentColor2 = Color.Lerp(initialColor2, Color.white, elapsedTime / 1);

            // Update the UI elements
            percentageText.text = currentPercentage.ToString("F1") + "%";
            if (GoodImage.GetComponent<Image>() != null)
            {
                GoodImage.GetComponent<Image>().color = currentColor1;
            }
            if (BadImage.GetComponent<Image>() != null)
            {
                BadImage.GetComponent<Image>().color = currentColor2;
            }

            yield return null;
        }

        // Set final values
        percentageText.text = finalValue.ToString("F1") + "%";
        if (GoodImage.GetComponent<Image>() != null)
        {
            GoodImage.GetComponent<Image>().color = Color.white;
        }
        if (BadImage.GetComponent<Image>() != null)
        {
            BadImage.GetComponent<Image>().color = Color.white;
        }
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
            if (!MicroGameVariables.tutorial)
            {
                MicroGameVariables.DifficultyChange();
                Difficulty++;
            }
                MicroGameVariables.MGNum = 0;
            MicroGameVariables.tutorial = false;
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
    }

    public void HideMGUI()
    {

        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().ResetTimer();
        MicroGameVariables.HideUI();
    }


    public void OutComesUp()
    {
        OutComesDown();
        LeanTween.move(parentOutcomeTransform, new Vector3(0, 0, 0), 1f).setEase(LeanTweenType.easeInOutQuad);
    }
    public void OutComesUpNormal()
    {
        parentOutcomeTransform.anchoredPosition = new Vector2(0, 0);
    }
    public void OutComesDown()
    {
        parentOutcomeTransform.anchoredPosition = new Vector2(0, -468);
    }

    public void ChangePercentagePosition()
    {
        percentageText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void ChangeTutorialImage()
    {
        switch (MicroGameVariables.SDGNum)
        {

            case 2:

                switch (MicroGameVariables.MGNum)
                {
                    case 0:

                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = ReleaseFishTutorialImage;
                        break;
                    case 1:
                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = FilterTrashTutorialImage;
                        break;
                    case 2:
                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = IllegalFishingTutorialImage;
                        break;

                }
                break;
            case 3:

                switch (MicroGameVariables.MGNum)
                {
                    case 0:

                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = ReleaseFishTutorialImage;
                        break;
                    case 1:
                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = FilterTrashTutorialImage;
                        break;
                    case 2:
                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = IllegalFishingTutorialImage;
                        break;

                }
                break;
            default:

                switch (MicroGameVariables.MGNum)
                {
                    case 0:

                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = ReleaseFishTutorialImage;
                        break;
                    case 1:
                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = FilterTrashTutorialImage;
                        break;
                    case 2:
                        tutorialImage.GetComponent<UnityEngine.UI.Image>().sprite = IllegalFishingTutorialImage;
                        break;

                }
                break;
        }
    }

    public void TutorialImageDown()
    {
        tutorialImage.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -465f);
    }
    public void TutorialImageUp()
    {
        tutorialImage.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
    public void RunTutorialDialog()
    {
        switch (MicroGameVariables.SDGNum)
        {
            case 2:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:

                        ReleaseFishTutorial.StartConversation();
                        nextMG();
                        break;
                    case 1:
                        FilterTrashTutorial.StartConversation();
                        nextMG();
                        break;
                    case 2:
                        IllegalFishingTutorial.StartConversation();
                        nextMG();

                        break;

                }
                break;
            case 3:
                switch (MicroGameVariables.MGNum)
                {
                    case 0:

                        ReleaseFishTutorial.StartConversation();
                        nextMG();
                        break;
                    case 1:
                        FilterTrashTutorial.StartConversation();
                        nextMG();
                        break;
                    case 2:
                        IllegalFishingTutorial.StartConversation();
                        nextMG();

                        break;

                }
                break;
            default:

                switch (MicroGameVariables.MGNum)
                {
                    case 0:

                        ReleaseFishTutorial.StartConversation();
                        nextMG();
                        break;
                    case 1:
                        FilterTrashTutorial.StartConversation();
                        nextMG();
                        break;
                    case 2:
                        IllegalFishingTutorial.StartConversation();
                        nextMG();

                        break;

                }
                break;
        }
    }
}
