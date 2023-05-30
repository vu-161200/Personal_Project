using System;
using UnityEngine;


public class NPC : Interactable
{
    [Header("NPC Info")]
    public Quaternion baseRotation; 
    public string[] baseDialogues = new string[]{
        "Hello adventurer, nice to meet you!",
        "Now, I still have no commission for you. If so, I will contact you immediately!"
    };

    [Header("Refs")]
    public PlayerInteract playerInteract;
    public PlayerInput playerInput;
    public DialogueManager dialogueManager;

    [Header("Target")]
    public bool isEscorting = false; 
    public PlayerManager playerManager; 

    protected virtual void Awake() {
        baseRotation = transform.rotation;
    }
    
    void LateUpdate() {
        if(isEscorting) return;
        
        if(playerManager != null) {
            float distance = Vector3.Distance(playerManager.transform.position, transform.position);
 
            if(distance <= 3f){
                Vector3 rotation = playerManager.transform.position - transform.position;
                rotation.y = 0;
                rotation.Normalize(); // 

                Quaternion tr = Quaternion.LookRotation(rotation); 
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, 300 * Time.deltaTime);

                transform.rotation = targetRotation;
            }
            else{
                
                transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation, 300 * Time.deltaTime);
                playerManager = null;
            }
            
        }
    }

    public override void Interact(PlayerInteract playerInteract){
        // Refs
        this.playerInteract = playerInteract;
        playerInput = playerInteract.GetComponent<PlayerInput>();
        dialogueManager = playerInteract.GetComponentInChildren<DialogueManager>();
   
        base.Interact(playerInteract);

        InitializeDialogue(baseDialogues);
    }

    public void InitializeDialogue(string[] _dialogues, Action _action = null){
        dialogueManager.InitializeDialogue(playerInput, _dialogues, npcName, _action);
    }

}
