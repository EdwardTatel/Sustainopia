using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineSequence : MonoBehaviour
{

    public Material newSkybox;
    private PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        Scene dialogueScene = SceneManager.GetSceneByName("Dialogue");

        if (!dialogueScene.isLoaded)
        {
            SceneManager.LoadSceneAsync("Dialogue", LoadSceneMode.Additive);
        }
        SetLighting();
        GameVariables.StopControls();
        GameVariables.DisableAllTexts();
        playableDirector = GetComponent<PlayableDirector>();
        StartIntroDialog();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    // Call this method when you want to resume the timeline (e.g., after pressing a dialogue button)
    public void ResumeTimeline()
    {
        playableDirector.Play();
    }

    public void EnableCameraFollow()
    {
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled = true;
        GameVariables.EnableControls();
        GameVariables.EnableAllTexts();
    }

    public void StartIntroDialog()
    {
            if (GameVariables.dialogStarted == 0)
            {
                /*ResumeTimeline();*/

                // Easy Debug
                EnableCameraFollow();
            }
            if (GameVariables.dialogStarted == 1)
            {
                GameObject.Find("levelsuccessdialog1").GetComponent<DialogueManager>().StartConversation();
            }
            else if (GameVariables.dialogStarted == 2)
            {
                GameObject.Find("levelsuccessdialog2").GetComponent<DialogueManager>().StartConversation();
            }
            else if(GameVariables.dialogStarted == 3)
            {
                GameObject.Find("levelsuccessdialog3").GetComponent<DialogueManager>().StartConversation();
            }
    }
    public void SetSDGGames(int SDGNum)
    {
        Debug.Log("SETTING AT SDG" + SDGNum);
        MicroGameVariables.SDGNum = SDGNum;
    }

    public void HideAllTextsOnClick()
    {
        GameVariables.DisableAllTexts();
        GameVariables.StopControls();
    }

    void SetLighting()
    {
        RenderSettings.skybox = newSkybox;

        RenderSettings.ambientIntensity = .59F; 
        DynamicGI.UpdateEnvironment();
    }
}
