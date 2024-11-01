using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class CatchPoachersMG : MonoBehaviour
{
    private TextMeshProUGUI SDGText;
    private Animator SDGImageAnimator;
    private bool gameDone = false;
    private bool list = true;
    private List<GameObject> poachers = new List<GameObject>();
    public Material newSkybox;

    // Start is called before the first frame update
    void Start()
    {
        SetLighting();
        Cursor.visible = false;
        MicroGameVariables.gameFailed = false;
        SDGText = GameObject.Find("LifeBelowWaterDoneText").GetComponent<TextMeshProUGUI>();
        SDGImageAnimator = GameObject.Find("UICanvas").GetComponent<Animator>();
        GameObject.Find("MicroGameManager").GetComponent<MicroGameManager>().AnimateBar();
        MicroGameVariables.ShowUI();
    }

    // Update is called once per frame
    void Update()
    {
        removeInactiveObjects();
        if (list)
        {
            // Find all objects with the "Fisher" tag
            GameObject[] poacherTaggedObjects = GameObject.FindGameObjectsWithTag("Poacher");

            // Add them to the list
            poachers.AddRange(poacherTaggedObjects);
            list = false;
        }

        if (poachers.Count <= 0) WinCondition();
        if (MicroGameVariables.gameFailed && !gameDone)
        {
            GameFailed();
            gameDone = true;
        }
    }

    private void removeInactiveObjects()
    {
        for (int i = poachers.Count - 1; i >= 0; i--)
        {
            if (!poachers[i].activeInHierarchy)
            {
                poachers.RemoveAt(i);
            }
        }
    }

    void WinCondition()
    {

        if (!gameDone)
        {

            if (poachers.Count <= 0)
            {
                GameWon();
                gameDone = true;
            }
            else
            {
                GameFailed();
                gameDone = true;
            }
        }
    }
    public void GameFailed()
    {
        MicroGameVariables.setGameStats(3, false);
        SDGText.text = "Fail!";
        SDGImageAnimator.Play("MGDone");
        MicroGameVariables.DeductLife();
    }
    public void GameWon()
    {
        MicroGameVariables.setGameStats(3, true);
        SDGText.text = "Success!";
        SDGImageAnimator.Play("MGDone");

    }
    void SetLighting()
    {
        RenderSettings.skybox = newSkybox;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;

        // Convert hex color codes to Color
        RenderSettings.ambientSkyColor = HexToColor("#363A42");
        RenderSettings.ambientEquatorColor = HexToColor("#1D2022");
        RenderSettings.ambientGroundColor = HexToColor("#0C0B09");

        RenderSettings.ambientIntensity = 0.5f;  // Adjust to control brightness
    }

    private Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        return Color.black; // Default if hex parsing fails
    }
}
