using TMPro;
using UnityEngine;

// Các tương tác của người dùng
public class PlayerInteract : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask interactableLayers;
    public float range = 1f;
    public float radius = 0.2f;
    
    [Header("UI")]
    public TMP_Text interactText;
    public GameObject interactObj;
    public GameObject keyInteract;
    public GameObject keyNeed;

    PlayerInput playerInput;
    InventoryManager inventoryManager;

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        inventoryManager = GetComponent<InventoryManager>();
    }

    private void Update() {
        CheckInteract();
    }

    public void CheckInteract(){
        RaycastHit hit;

        // Kiểm tra trong bán kinh radius tại vị trí của người dùng có tiếp xúc với đối tượng tương tác nào không?
        if(Physics.SphereCast(transform.position, radius, transform.forward, out hit, range, interactableLayers)){
            if(hit.collider.tag == "Pick Up"){
                PickUp pickUp = hit.transform.GetComponent<PickUp>();

                if(pickUp != null){
                    ShowInteractText("Pick Up");

                    if(playerInput.interact_Input){
                        pickUp.Interact(this);
                    }
                }
            }
            else if(hit.collider.tag == "Chest"){
                Chest chest = hit.transform.GetComponent<Chest>();

                if(chest != null){
                    ShowInteractText("Open Chest");

                    if(playerInput.interact_Input || (playerInput.pause_Input && playerInput.chestFlag)){
                        chest.Interact(this);
                        HideInteractText();
                    }
                }
            }
            else if(hit.collider.tag == "Shop"){
                ShopManager shopManager = hit.transform.GetComponent<ShopManager>();

                if(shopManager != null){
                    ShowInteractText("Open Shop");

                    if(playerInput.interact_Input || (playerInput.pause_Input && playerInput.shopFlag)){
                        shopManager.Interact(this);
                        HideInteractText();
                    }
                }
            }
            else if(hit.collider.tag == "Door"){
                Door door = hit.transform.GetComponent<Door>();

                if(door != null && !door.isLocked){
                    bool hasKey = true;

                    // Cửa cần chìa khóa
                    if(door.keyID != ""){
                        hasKey = inventoryManager.CheckPrisonKey(door.keyID);

                        if(!hasKey) ShowInteractText("Need '" + door.keyID + "'", false);
                        else if(door.isOpen) ShowInteractText(door.openOnStart ? "Close Door" : "Open Door");
                        else ShowInteractText(door.openOnStart ? "Open Door" : "Close Door");
                    }
                    // Cửa không cần chìa khóa
                    else{
                        if(door.isOpen) ShowInteractText(door.openOnStart ? "Open Door" : "Close Door");
                        else ShowInteractText(door.openOnStart ? "Close Door" : "Open Door");
                    }
                    
                    if(hasKey && playerInput.interact_Input){
                        door.Interact(this);
                        HideInteractText();
                    }
                }
            }
            else if(hit.collider.tag == "NPC"){
                NPC npc = hit.transform.GetComponent<NPC>();

                if(npc != null && !npc.isEscorting){
                    npc.playerManager = playerInput.playerManager;

                    if(!playerInput.dialogueFlag){
                        ShowInteractText($"Talk to {npc.npcName}");
                    }

                    if(playerInput.interact_Input){
                        npc.Interact(this);
                        HideInteractText();
                    }
                }
            }
            // Teleport
            else if(hit.collider.tag == "Portal"){
                Portal portal = hit.transform.GetComponent<Portal>();

                if(portal != null){
                    ShowInteractText($"Teleport to {portal.destination.ToString()}");

                    if(playerInput.interact_Input){
                        portal.Interact(this);
                        HideInteractText();
                    }
                }
            }
        }else{
            HideInteractText();
        }
    }

    void HideInteractText(){
        interactObj.SetActive(false);
    }

    void ShowInteractText(string text, bool showButtonInteract = true){
        interactObj.SetActive(true);
        
        if(showButtonInteract){
            keyNeed.SetActive(false);
            keyInteract.SetActive(true);
        }else{
            keyNeed.SetActive(true);
            keyInteract.SetActive(false); 
        }

        interactText.text = text;
    }

}
