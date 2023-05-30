using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueUI;
    public TMP_Text npcNameText;
    public TMP_Text dialogueText;
    public TMP_Text buttonText;

    [Header("Quest Info")]
    public string npcName;
    public string[] dialogues;
    int currentDialogueIndex = 0;

    PlayerInput playerInput;
    Action action;

    private void Awake() {
        dialogueUI.SetActive(false);
    }

    public void InitializeDialogue(PlayerInput _playerInput, string[] _dialogues, string _name, Action _action = null){
        playerInput = _playerInput;
        action = _action;
        dialogues = _dialogues;
        npcName = _name;
        currentDialogueIndex = 0;

        playerInput.cameraHandler.ChangeCursor(false);
        playerInput.dialogueFlag = true;

        PlayDialogue();
        dialogueUI.SetActive(true);
    }

    public void PlayDialogue(){
        if(currentDialogueIndex == dialogues.Length - 2){
            buttonText.text = "OK";
        }else{
            buttonText.text = "Next";
        }

        npcNameText.text = npcName; 
        dialogueText.text = dialogues[currentDialogueIndex++];
    }

    public void NextDialogue(){
        if(currentDialogueIndex < dialogues.Length - 1){
            PlayDialogue();
        }else{
            playerInput.cameraHandler.ChangeCursor(true, false);
            dialogueUI.SetActive(false);
            playerInput.dialogueFlag = false;

            if(action != null){
                action();
            }
        }
    }

    public void SkipDialogues(){
        currentDialogueIndex = dialogues.Length - 1;
        NextDialogue();
    }
}
