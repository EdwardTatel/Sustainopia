using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using System.Runtime.CompilerServices;

public class MicroGameManager : MonoBehaviour
{
    private GameObject bar;
    public GameObject timerbar,heart1, heart2, heart3;
    private int tweenId;
    // Start is called before the first frame update

    private void Awake()
    {
        bar = GameObject.Find("TimerBarUI");
        timerbar = GameObject.Find("TimerBar");
        heart1 = GameObject.Find("Heart1");
        heart2 = GameObject.Find("Heart2");
        heart3 = GameObject.Find("Heart3");
        MicroGameVariables.OnShowUIChange += showHideUI;
        showHideUI(MicroGameVariables.showUI);
        timerbar.gameObject.SetActive(false);
        heart1.gameObject.SetActive(false);
        heart2.gameObject.SetActive(false);
        heart3.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        LivesUI();

    }
    public void AnimateBar()
    {
        tweenId = LeanTween.scaleX(bar, 0, 9.7f).setOnComplete(CheckWinCondition).id;
    }

    public void CancelTimerBarAnimate()
    {
        LeanTween.cancel(tweenId);
        ResetTimer();
    }
    public void CheckWinCondition()
    {
        MicroGameVariables.gameFailed = true;
    }


    public void ResetTimer()
    {
        LeanTween.scaleX(bar, 0.98f, .1f);
    }
    private void showHideUI(bool showUI)
    {
        if (!showUI)
        {
            CancelTimerBarAnimate();
            if(timerbar != null) timerbar.gameObject.SetActive(false);
            if (heart1 != null) heart1.gameObject.SetActive(false);
            if (heart2 != null) heart2.gameObject.SetActive(false);
            if (heart3 != null) heart3.gameObject.SetActive(false);
        }
        else
        {
            if (timerbar != null) timerbar.gameObject.SetActive(true);
        }
    }
    public void LivesUI()
    {
        if (MicroGameVariables.showUI == true)
        {
            switch (MicroGameVariables.GetLives())
            {

                case 3:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(true);
                    break;
                case 2:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(false);
                    break;
                default:
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(false);
                    heart3.gameObject.SetActive(false);
                    break;
            }
        }
        
    }
}
