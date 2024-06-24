using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera camecamera;
    public Animator animator;
    public enum UISelect
    {
        ReleaseFishInst,
        ReleaseFishDone
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("SDGImagePop");
    }

    void Update()
    {
        if(Camera.main != null) camecamera = Camera.main;
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
        SceneManager.LoadSceneAsync("ReleaseFishMG",LoadSceneMode.Additive);
    }
    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync("UITransition");
    }

    public void UnloadGameScenes()
    {
        Scene targetScene = SceneManager.GetSceneByName("ReleaseFishMG");

        if (targetScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(targetScene);
        }
    }

}
 