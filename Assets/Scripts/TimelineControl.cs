using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI; // Make sure to include this if you are using a UI button

public class TimelineControl : MonoBehaviour
{
    public PlayableDirector playableDirector; // Assign your Playable Director here


    public void StartTimeline()
    {
        // Start the Timeline when this method is called
        playableDirector.Play();
    }
}