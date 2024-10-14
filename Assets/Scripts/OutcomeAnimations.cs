using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OutcomeAnimations : MonoBehaviour
{
    public GameObject Outcomes;
    public GameObject GoodImage;
    public GameObject BadImage;
    private Animator SDGImageAnimator;
    public Sprite GoodReleaseFishImage;
    public Sprite BadReleaseFishImage;

    // Start is called before the first frame update
    void Start()
    {
        SDGImageAnimator = GameObject.Find("SDGImage").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSplashArt()
    {
        

    }
    public void SetOutcomes()
    {
        switch (MicroGameVariables.SDGNum)
        {
            case 1:
                switch (MicroGameVariables.MGNum)
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
                        BadImage.GetComponent<UnityEngine.UI.Image>().sprite = GoodReleaseFishImage;
                        break;
                }
                break;
            case 2:
                switch (MicroGameVariables.MGNum)
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
                switch (MicroGameVariables.MGNum)
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
        switch (MicroGameVariables.MGNum)
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
        Debug.Log(prevGameStats);
        RectTransform outcomeTransform = Outcomes.GetComponent<RectTransform>();
        outcomeTransform.localPosition = new Vector3(prevGameStats * 6708.6f, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);

    }
    public void AnimateOutcomes()
    {
        int gameStats = 0;
        int prevGameStats = 0;
        switch (MicroGameVariables.MGNum)
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
        RectTransform outcomeTransform = Outcomes.GetComponent<RectTransform>();
        outcomeTransform.localPosition = new Vector3(prevGameStats * 6708.6f, outcomeTransform.localPosition.y, outcomeTransform.localPosition.z);
        Debug.Log("MGNUM = " + MicroGameVariables.MGNum + " Difficulty = " + MicroGameVariables.GetDifficulty());
        if (MicroGameVariables.MGNum == 3 && MicroGameVariables.GetDifficulty() == MicroGameVariables.levels.hard)
        {
            LeanTween.move(outcomeTransform, new Vector2(40434, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlaySDGAnimation);
        }
        else LeanTween.move(outcomeTransform, new Vector2(gameStats * 6708.6f, outcomeTransform.localPosition.y), 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(PlaySDGAnimation);
    }
    private void PlaySDGAnimation()
    {
        SDGImageAnimator.Play("LifeBelowWater");
    }
}
