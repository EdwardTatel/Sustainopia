using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameVariables {
    // Start is called before the first frame update
    public static bool disableText = false;
    public static bool stopControls = false;

    public static bool city1Finished = false;
    public static bool city2Finished = false;
    public static bool city3Finished = false;
    public static void DisableAllTexts()
    {
        disableText = true;
    }
    public static void EnableAllTexts()
    {
        disableText = false;
    }
    public static void StopControls()
    {
        stopControls = true;
    }
    public static void EnableControls()
    {
        stopControls = true;
    }
}
