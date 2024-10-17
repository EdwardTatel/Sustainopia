using DialogueEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Make sure to include this if you are using a UI button

public class SequenceManagement : MonoBehaviour
{
    public PlayableDirector playableDirector; // Assign your Playable Director here

    private void Start()
    {

        
    }
    public void StartTimeline()
    {
        Debug.Log("Start Dialog");
        // Start the Timeline when this method is called
        playableDirector.Play();
        SceneManager.LoadSceneAsync("Dialogue", LoadSceneMode.Additive);
    }

    public void StartSequence()
    {
        GameObject.Find("SceneManagement").GetComponent<SceneManagement>().LoadLevelSelect();
    }

    public void ChangeDialogueManager()
    {
        Destroy(GameObject.Find("EventSystem"));
        Destroy(GameObject.Find("ConversationManager"));
        SceneManager.LoadSceneAsync("Dialogue", LoadSceneMode.Additive);
    }
    public void MovetoLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

}