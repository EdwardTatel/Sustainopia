using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void ExitGame()
    {
        // Quit the application
        Application.Quit();

        // Stop play mode if in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}