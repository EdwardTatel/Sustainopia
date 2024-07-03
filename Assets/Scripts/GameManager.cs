using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera camecamera;
    public Animator animator;
    private int microGameNumber = 1;

    public enum UISelect
    {
        ReleaseFishInst,
        ReleaseFishDone
    }

    void Start()
    {
        animator = GetComponent<Animator>();
       
    }

    void Update()
    {
        animator.SetInteger("microGame", microGameNumber);
        if (Camera.main != null) camecamera = Camera.main;
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
    public void LoadMicroGame()
    {
        switch(microGameNumber) {
            case 1:
                SceneManager.LoadSceneAsync("ReleaseFishMG", LoadSceneMode.Additive);
                microGameNumber++;
                break;
            case 2:
                SceneManager.LoadSceneAsync("WoodConstructionMG", LoadSceneMode.Additive);
                microGameNumber++;
                break;
            case 3:
                SceneManager.LoadSceneAsync("RemoveInvasiveSpeciesMG", LoadSceneMode.Additive);
                microGameNumber = 1;
                break;
            default:
                break;
        }
            

    }
    public void LoadSecondMicroGame()
    {
        SceneManager.LoadSceneAsync("WoodConstructionMG", LoadSceneMode.Additive);
    }
    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync("UITransition");

    }

    public void UnloadGameScenes()
    {
        Scene targetScene = SceneManager.GetSceneByName("ReleaseFishMG");
        Scene targetScene2 = SceneManager.GetSceneByName("WoodConstructionMG");
        Scene targetScene3 = SceneManager.GetSceneByName("RemoveInvasiveSpeciesMG");
        Scene targetScene4 = SceneManager.GetSceneByName( "LevelSelect");


        if (targetScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene);
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
    }

}
 