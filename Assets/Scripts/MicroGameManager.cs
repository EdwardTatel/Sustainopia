using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MicroGameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadUIScene()
    {
        SceneManager.LoadSceneAsync("UITransition", LoadSceneMode.Additive);
    }
}
