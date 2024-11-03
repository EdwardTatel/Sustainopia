using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSequence : MonoBehaviour
{
    [SerializeField] private GameObject car;
    void Start()
    {
    }

    void Update()
    {
        
    }
    public void LoadUIScene()
    {
        GameVariables.Save(car.transform);
        SceneManager.LoadSceneAsync("UITransition", LoadSceneMode.Additive);
    }

}
