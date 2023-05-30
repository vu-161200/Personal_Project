using System.Collections.Generic;
using UnityEngine;
using static Enums;

// Thông tin vật phẩm trong rương
[System.Serializable]
public class ChestItem
{
    public Item item;
    public int stackSize = 1;

    public ChestItem(Item item, int stackSize){
        this.item = item;
        this.stackSize = stackSize;
    }
}

public class Chest : Interactable
{
    [Header("Settings")]
    public bool isOpen = false;
    public int chestSize = 24;

    [Header("UI")]
    public GameObject decorationsUI;
    public GameObject chestUI;

    [Header("Items")]
    public Transform chestSlotParent;
    public GameObject chestSlotPrefab;
    public ChestItem[] items;

    [Header("Refs")]
    Animator anim;
    InventoryManager inventoryManager;
    PlayerInput playerInput;

    private void OnValidate() {
        foreach(ChestItem item in items) {
            if(item.item != null) {
                if(item.stackSize > item.item.maxStack){
                    item.stackSize = item.item.maxStack;
                }

                if(item.stackSize < 1){
                    item.stackSize = 1;
                }
            }
        }
    }

    void Awake() {
        anim = GetComponent<Animator>();
    }

    public override void Interact(PlayerInteract playerInteract) {
        base.Interact(playerInteract);

        this.playerInput = playerInteract.GetComponent<PlayerInput>();
        this.inventoryManager = playerInteract.GetComponent<InventoryManager>();
        
        if(isOpen) CloseChest();
        else OpenChest();
    }

    // Cập nhật lại tham chiếu khi load scene
    //! Load scene sẽ bị mất tham chiếu do rương đó ở map khác, trong khi tham chiếu này ở map trước đó
    void AssignRefs(){
        if(decorationsUI == null) decorationsUI = playerInput.playerManager.decorationsUI;
        if(inventoryManager.equipmentUI == null) inventoryManager.equipmentUI = playerInput.playerManager.equipmentUI;
        if(chestUI == null) chestUI = playerInput.playerManager.chestUI;
        if(chestSlotParent == null) chestSlotParent = playerInput.playerManager.chestSlotParent;
    }

    public void OpenChest(){
        AssignRefs();

        GenerateSlots();
        isOpen = true;

        anim.SetBool("isOpen", isOpen);

        playerInput.cameraHandler.ChangeCursor(false);
        playerInput.chestFlag = true;

        inventoryManager.inventoryUI.SetActive(true);
        inventoryManager.hudUI.SetActive(false);
        
        decorationsUI.SetActive(false);
        inventoryManager.equipmentUI.SetActive(false);
        chestUI.SetActive(true);
    }

    public void CloseChest(){
        UpdateItemAndDestroySlot();
        isOpen = false;

        anim.SetBool("isOpen", isOpen);

        playerInput.cameraHandler.ChangeCursor(true);
        playerInput.chestFlag = false;

        inventoryManager.inventoryUI.SetActive(false);
        inventoryManager.hudUI.SetActive(true);

        decorationsUI.SetActive(true);
        inventoryManager.equipmentUI.SetActive(true);
        chestUI.SetActive(false);

        inventoryManager.DestroyItemInfo();
    }

    void GenerateSlots(){
        for (int i = 0; i < chestSize; i++){
            InventorySlot slot = Instantiate(chestSlotPrefab.gameObject, chestSlotParent).GetComponent<InventorySlot>();

            if(i < items.Length){

                slot.AddItem(items[i].item, items[i].stackSize);
                // Gán là slot trong Chest
                slot.itemType = ItemType.Chest;
            }
        }
    }

    // Cập nhật lại vật phẩm khi đóng rương
    //! Do có thể lấy vật phẩm hoặc để vào trong đó nên ds vật phẩm bị thay đổi so với mặc định
    //! Nên cần cập nhật lại mỗi khi đóng rương
    void UpdateItemAndDestroySlot(){
        List<ChestItem> _items = new List<ChestItem>();

        foreach (Transform child in chestSlotParent) {
            InventorySlot slot = child.gameObject.GetComponent<InventorySlot>();
            if(slot.item != null){
                _items.Add(new ChestItem(slot.item, slot.stackSize));
            }else{
                _items.Add(new ChestItem(null, 1));
            }

            GameObject.Destroy(child.gameObject);
        }

        items = _items.ToArray();
    }

}
