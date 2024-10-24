using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public void StartConversation()
    {
        NPCConversation Convo = GetComponent<NPCConversation>();
        ConversationManager.Instance.StartConversation(Convo);
    }

}
