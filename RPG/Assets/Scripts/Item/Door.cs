using UnityEngine;

// Cửa
public class Door : Interactable
{
    [Header("Small Boss")]
    public bool isLocked = false;

    [Header("Door Information")]
    public bool openOnStart;
    public bool isReversed;
    public bool isOpen;

    [Header("Refs")]
    Animator anim;
    PlayerInteract playerInteract;

    public string keyID = "";

    private void Awake() {
        anim = GetComponent<Animator>();

        if(openOnStart){
            anim.SetLayerWeight(1, 1);
        }else if(isReversed){
            anim.SetLayerWeight(2, 1);
        }else{
            anim.SetLayerWeight(3, 1);
        }
    }

    public override void Interact(PlayerInteract playerInteract){
        this.playerInteract = playerInteract;

        base.Interact(playerInteract);
        
        if(isOpen) CloseDoor();
        else OpenDoor();
    }

    public void OpenDoor(){
        isOpen = true;

        anim.SetBool("isOpen", isOpen);
    }

    public void CloseDoor(){
        isOpen = false;

        anim.SetBool("isOpen", isOpen);
    }

    public void LockDoor(){
        isLocked = true;
        isOpen = false;

        anim.SetBool("isOpen", isOpen);
    }

    public void UnlockDoor(){
        isLocked = false;
        isOpen = true;

        anim.SetBool("isOpen", isOpen);
    }
}
