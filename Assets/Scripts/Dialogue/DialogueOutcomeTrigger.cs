using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOutcomeTrigger : MonoBehaviour
{
    public Dialogue dialogue;


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueOutcomeManager>().StartDialogue(dialogue);
    }
    public void SetSDGGames(int SDGNum)
    {
        MicroGameVariables.SDGNum = SDGNum;
    }

    public void HideAllTextsOnClick()
    {
        GameVariables.DisableAllTexts();
        GameVariables.StopControls();
    }
}
