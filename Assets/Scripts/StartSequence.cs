using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSequence : MonoBehaviour
{
    [SerializeField] private GameObject car;
    void Start()
    {
        Debug.Log("Aint no rush");
        SceneManager.LoadSceneAsync("Dialogue", LoadSceneMode.Additive);
    }

    void Update()
    {
        
    }

    
    public void LoadUIScene()
    {
        SceneManager.LoadSceneAsync("UITransition", LoadSceneMode.Additive);
    }
}
