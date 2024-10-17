using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MicroGameVariables.GetDifficulty());
    }

    public void LoadLevelSelect()
    {

        SceneManager.LoadSceneAsync("LevelSelect", LoadSceneMode.Additive);
        Scene targetScene = SceneManager.GetSceneByName("Main Menu");
        SceneManager.UnloadSceneAsync(targetScene);
        GameVariables.EnableAllTexts();
    }
    public void BackToLevelSelect()
    {
        SceneManager.LoadSceneAsync("LevelSelect", LoadSceneMode.Additive);
        Scene targetScene = SceneManager.GetSceneByName("UITransition");
        SceneManager.UnloadSceneAsync(targetScene);
        GameVariables.EnableAllTexts();
    }

}
