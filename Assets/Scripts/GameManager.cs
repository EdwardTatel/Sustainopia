using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera gamecamera;
    public Animator animator;
    [SerializeField] private TextMeshProUGUI SDGText;
    public enum UISelect
    {
        ReleaseFishInst,
        ReleaseFishDone
    }

    void Start()
    {
        UnityEngine.SceneManagement.Scene targetScene1 = SceneManager.GetSceneByName("Dialogue");
        if (!targetScene1.IsValid())
        {
            SceneManager.LoadSceneAsync("Dialogue", LoadSceneMode.Additive);
        }
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Camera.main != null) gamecamera = Camera.main;
    }
    public void DisableCanvas()
    {
        transform.parent.gameObject.SetActive(false);
    }
    public void ChangeCamera()
    {
        if (Camera.main != null && Camera.main.transform.name != "GameCamera")
        {
            Camera currentMainCamera = Camera.main;
            Camera newMainCamera = GameObject.Find("GameCamera").GetComponent<Camera>(); 

            if (newMainCamera != null)
            {
                currentMainCamera.enabled = false;
                currentMainCamera.tag = "Untagged";
                newMainCamera.enabled = true;
                newMainCamera.tag = "MainCamera";
            }
        }

    }
    public void LoadGame()
    {

        switch (MicroGameVariables.SDGNum) {
            
            case 3:
                string[] scenes1 = new string[] { "SegregateTrashMG", "FillSolarPanelMG", "WoodConstructionMG" };
                SceneManager.LoadSceneAsync(scenes1[MicroGameVariables.MGNum], LoadSceneMode.Additive);
                break;
            case 2:
                string[] scenes2 = new string[] { "PlantTreesMG", "RemoveInvasiveSpeciesMG", "CatchPoachersMG" };
                SceneManager.LoadSceneAsync(scenes2[MicroGameVariables.MGNum], LoadSceneMode.Additive);
                break;
            default:
                string[] scenes3 = new string[] { "ReleaseFishMG", "FilterTrashMG", "IllegalFishingMG" };
                SceneManager.LoadSceneAsync(scenes3[MicroGameVariables.MGNum], LoadSceneMode.Additive);
                break;
        }
    }

    public void ChangeGame()
    {
        switch (MicroGameVariables.SDGNum)
        {

            case 3:
                string[] scenes1 = new string[] { "SegregateTrashMG", "FillSolarPanelMG", "WoodConstructionMG" };
                switch (MicroGameVariables.MGNum)
                {
                    case 1:
                        SDGText.text = "Fill Solar Panels!";
                        break;
                    case 2:
                        SDGText.text = "Construct Without Concrete!";
                        break;
                    default:
                        SDGText.text = "Segregate Trash!";
                        break;
                }
                break;
            case 2:
                string[] scenes2 = new string[] { "PlantTreesMG", "RemoveInvasiveSpeciesMG", "CatchPoachersMG" };
                switch (MicroGameVariables.MGNum)
                {
                    case 1:
                        SDGText.text = "Remove Invasive Species!";
                        break;
                    case 2:
                        SDGText.text = "Catch Poachers!";
                        break;
                    default:
                        SDGText.text = "Plant Trees!";
                        break;

                }
                break;
            default:
                string[] scenes3 = new string[] { "ReleaseFishMG", "FilterTrashMG", "IllegalFishingMG" };
                switch (MicroGameVariables.MGNum)
                {
                    case 1:
                        SDGText.text = "Filter Trash!";
                        break;
                    case 2:
                        SDGText.text = "Find Illegal Fishers";
                        break;
                    default:
                        SDGText.text = "Release Small Fish!";
                        break;

                }
                break;
        }

    }
    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync("UITransition");

    }
    public static void LevelFinish()
    {
        
        SceneManager.LoadScene("LevelSelect");

    }
    public void RunUnloadGameScenes()
    {
        UnloadGameScenes();
    }
    public static void UnloadGameScenes()
    {
        UnityEngine.SceneManagement.Scene targetScene1 = SceneManager.GetSceneByName("ReleaseFishMG");
        UnityEngine.SceneManagement.Scene targetScene2 = SceneManager.GetSceneByName("FilterTrashMG");
        UnityEngine.SceneManagement.Scene targetScene3 = SceneManager.GetSceneByName("IllegalFishingMG");

        UnityEngine.SceneManagement.Scene targetScene4 = SceneManager.GetSceneByName("WoodConstructionMG");
        UnityEngine.SceneManagement.Scene targetScene5 = SceneManager.GetSceneByName("SegregateTrashMG");
        UnityEngine.SceneManagement.Scene targetScene6 = SceneManager.GetSceneByName("FillSolarPanelMG");

        UnityEngine.SceneManagement.Scene targetScene7 = SceneManager.GetSceneByName("RemoveInvasiveSpeciesMG");
        UnityEngine.SceneManagement.Scene targetScene8 = SceneManager.GetSceneByName("CatchPoachersMG");
        UnityEngine.SceneManagement.Scene targetScene9 = SceneManager.GetSceneByName("PlantTreesMG");


        UnityEngine.SceneManagement.Scene targetScene10 = SceneManager.GetSceneByName("LevelSelect");


        if (targetScene1.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene1);
        }
        if (targetScene2.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene2);
        }
        if (targetScene3.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene3);
        }
        if (targetScene4.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene4);
        }
        if (targetScene5.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene5);
        }
        if (targetScene6.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene6);
        }
        if (targetScene7.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene7);
        }
        if (targetScene8.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene8);
        }
        if (targetScene9.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene9);
        }
        if (targetScene10.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene10);
        }
    }

}
 