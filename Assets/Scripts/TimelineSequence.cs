using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineSequence : MonoBehaviour
{

    public Material newSkybox;
    private PlayableDirector playableDirector;
    private PlayableDirector endTimeLine;
    private PlayableDirector endTimeLine2;
    // Start is called before the first frame update
    private void Awake()
    {
        GameVariables.DisableAllTexts();
        GameVariables.StopControls();
    }
    void Start()
    {
        SetLighting();

        Scene dialogueScene = SceneManager.GetSceneByName("Dialogue");

        if (!dialogueScene.isLoaded)
        {
            SceneManager.LoadSceneAsync("Dialogue", LoadSceneMode.Additive);
        }
        playableDirector = GetComponent<PlayableDirector>();
        endTimeLine = GameObject.Find("EndTimeLine").GetComponent<PlayableDirector>();
        endTimeLine2 = GameObject.Find("EndTimeLine2").GetComponent<PlayableDirector>();
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
        if (GameVariables.city1Finished && GameVariables.city2Finished && GameVariables.city3Finished)
        {
            ResumeEnd();
        }
        else
        {
            StartCoroutine(EnableTexts());
        }
        
    }

    IEnumerator EnableTexts()
    {
        yield return new WaitForSeconds(1f);

        GameVariables.EnableAllTexts();
    }
    
    public void ResumeEnd()
    {
        endTimeLine.Play();
    }
    public void ResumeEnd2()
    {
        endTimeLine2.Play();
    }
    public void PauseEnd()
    {

        endTimeLine.Pause();
    }
    public void StartIntroDialog()
    {
            if (GameVariables.dialogStarted == 0)
            {
                ResumeTimeline();
        }
            else if (GameVariables.dialogStarted == 1)
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
            else if (GameVariables.dialogStarted == 4)
            {
                GameObject.Find("levelsuccessdialog3").GetComponent<DialogueManager>().StartConversation();
            }

    }
    public void SetSDGGames(int SDGNum)
    {
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
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;

        // Convert hex color codes to Color
        RenderSettings.ambientSkyColor = HexToColor("#363A42");
        RenderSettings.ambientEquatorColor = HexToColor("#565E63");
        RenderSettings.ambientGroundColor = HexToColor("#A6A6A6");

        RenderSettings.ambientIntensity = 0.5f;  // Adjust to control brightness
    }

    private Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        return Color.black; // Default if hex parsing fails
    }
}
