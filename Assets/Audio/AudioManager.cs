using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip GameLoopEscalate;
    public AudioClip GameLoopSample;
    public AudioClip GameLoop1;
    public AudioClip GameLoop2;
    public AudioClip MainMenu;

    public void PlayAudio(string audioName)
    {
        switch (audioName.ToLower()) // Lowercase to make matching easier
        {
            case "gameloopescalate":
                musicSource.clip = GameLoopEscalate;
                break;
            case "gameloopsample":
                musicSource.clip = GameLoopSample;
                break;
            case "gameloop1":
                musicSource.clip = GameLoop1;
                break;
            case "gameloop2":
                musicSource.clip = GameLoop2;
                break;
            case "mainmenu": // Added as a case
                musicSource.clip = MainMenu;
                break;
            default:
                musicSource.clip = MainMenu;
                break;
        }
        musicSource.Play();
    }

    private void Start()
    {
        PlayAudio("mainmenu"); // This should match the switch case now
    }
}