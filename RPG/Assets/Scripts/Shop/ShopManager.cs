using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Enums;

public class ShopManager : Interactable
{
    [Header("UI")]
    public GameObject shopUI;
    public GameObject hudUI;

    [Header("Settings")]
    public bool isOpen = false;
    
    public List<Item> shopItems;
    public List<Item> filteredItems;

    [Header("Refs")]
    public TMP_Text coinText;
    public Transform shopContainer;
    public Transform sellContainer;
    public GameObject shopBuySlotPrefab;
    public GameObject shopSellSlotPrefab;
    PlayerInput playerInput;
    PlayerStats playerStats;


    [Header("Item Infor Refs")]
    public GameObject itemInfo;
    public ShopBuySlot currentBuyItem;
    public ShopSellSlot currentSellItem;
    public Image itemInforIcon;
    public TMP_Text itemInforName;
    public TMP_Text itemInforDescription;
    public TMP_Text itemInforStats;
    public Slider itemInforStack;
    public TMP_Text itemInforStackText;
    public TMP_Text itemInforTotalPrice;
    public TMP_Text itemInforButtonText;
    
    [Header("BUY Button")]
    public Image buttonBackground;
    public Sprite canBuy;
    public Sprite cantBuy;

    [Header("SHOP type")]
    public string shopType;
    public GameObject sellPanel;
    public GameObject buyPanel;
    public Image buttonSell;
    public Image buttonBuy;
    public Sprite active;
    public Sprite deactivate;

    private void Awake() {
        GetComponent<Animator>().SetBool("isSitting", true);
        sellPanel.SetActive(false);
        buyPanel.SetActive(false);
    }

    public override void Interact(PlayerInteract playerInteract){
        base.Interact(playerInteract);

        playerInput = playerInteract.GetComponent<PlayerInput>();
        playerStats = playerInteract.GetComponent<PlayerStats>();
        
        if(isOpen) CloseShop();
        else OpenShop();
    }

    public void OpenShop(){
        coinText.text = playerStats.coin.ToString();
        filteredItems = shopItems;

        // Mặc định mới vào là BUY
        ChangeShopType("BUY");

        GenerateShopBUY();

        playerInput.cameraHandler.ChangeCursor(false, true);
        playerInput.shopFlag = true;

        shopUI.SetActive(true);
        hudUI.SetActive(false);

        isOpen = true;
    }

    public void CloseShop(){
        currentBuyItem = null;
        currentSellItem = null;

        playerInput.cameraHandler.ChangeCursor(true, true);
        playerInput.shopFlag = false;

        itemInfo.SetActive(false);
        shopUI.SetActive(false);
        hudUI.SetActive(true);

        isOpen = false;

        shopType = "BUY";
        sellPanel.SetActive(false);
        buyPanel.SetActive(true);
    }

    public void ChangeShopType(string type){
        if(shopType == type) return;

        switch (type)
        {
            case "BUY":
                shopType = "BUY";
                buyPanel.SetActive(true);
                sellPanel.SetActive(false);
                itemInfo.SetActive(false);
                buttonBuy.sprite = active;
                buttonSell.sprite = deactivate;
                break;
            case "SELL":
                shopType = "SELL";
                buttonBackground.sprite = canBuy;
                GenerateShopSELL();
                buyPanel.SetActive(false);
                sellPanel.SetActive(true);
                itemInfo.SetActive(false);
                buttonSell.sprite = active;
                buttonBuy.sprite = deactivate;
                break;
            default:
                Debug.Log("Type is not valid");
                break;
        }
    } 

    public void FilteredItems(string type){
        switch (type)
        {
            case "All":
                filteredItems = shopItems;
                break;
            case "Weapon":
                filteredItems = shopItems.FindAll(item => item.itemType == ItemType.Weapon || item.itemType == ItemType.Ammo);
                break;
            case "Armor":
                filteredItems = shopItems.FindAll(item => item.itemType == ItemType.Equipment && item.equipType == EquipType.Armor);
                break;
            case "Boots":
                filteredItems = shopItems.FindAll(item => item.itemType == ItemType.Equipment && item.equipType == EquipType.Boots);
                break;
            case "Cape":
                filteredItems = shopItems.FindAll(item => item.itemType == ItemType.Equipment && item.equipType == EquipType.Cape);
                break;
            case "Helmet":
                filteredItems = shopItems.FindAll(item => item.itemType == ItemType.Equipment && item.equipType == EquipType.Helmet);
                break;
            case "Gloves":
                filteredItems = shopItems.FindAll(item => item.itemType == ItemType.Equipment && item.equipType == EquipType.Gloves);
                break;
            case "Legs":
                filteredItems = shopItems.FindAll(item => item.itemType == ItemType.Equipment && item.equipType == EquipType.Legs);
                break;
            case "Consumable":
                filteredItems = shopItems.FindAll(item => item.itemType == ItemType.Consumable);
                break;
                
            default:
                Debug.Log("'" + type + "' is not a valid");
                return;
        }

        GenerateShopBUY();
    }

    void GenerateShopBUY(){
        DeleteAllShopSlots(shopContainer);

        for (int i = 0; i < filteredItems.Count; i++){
            ShopBuySlot slot = Instantiate(shopBuySlotPrefab.gameObject, shopContainer).GetComponent<ShopBuySlot>();
            slot.InitialSlot(filteredItems[i], this);
        }
    }

    void GenerateShopSELL(){
        DeleteAllShopSlots(sellContainer);

        InventorySlot[] inventories = playerInput.inventoryManager.inventorySlots;
        // Lấy danh sách vật phẩm trong kho đồ
        for (int i = 0; i < inventories.Length; i++){
            if(inventories[i].item != null){
                ShopSellSlot slot = Instantiate(shopSellSlotPrefab.gameObject, sellContainer).GetComponent<ShopSellSlot>();
                slot.InitialSlot(inventories[i].item, inventories[i].stackSize, i, this);
            }
        }
    }

    void DeleteAllShopSlots(Transform parent){
        foreach (Transform child in parent) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public bool CheckCanBuyItem(int price){
        if(price <= playerStats.coin){
            buttonBackground.sprite = canBuy;
            return true;
        }else{
            buttonBackground.sprite = cantBuy;
            return false;
        }
    }

    public void BuyItem(){
        // BUY
        if(shopType == "BUY"){
            int stack = Mathf.RoundToInt(itemInforStack.value);
            int totalPrice = currentBuyItem.item.price * stack;

            if(CheckCanBuyItem(totalPrice)){
                playerStats.coin -= totalPrice;
                coinText.text = playerStats.coin.ToString();

                playerInput.inventoryManager.AddItem(currentBuyItem.item, stack);
                playerInput.playerManager.characterSound.PlaySound("buySell");

                CheckCanBuyItem(totalPrice);
            }else{
                Debug.Log("Not enough coins: " + totalPrice);
            }
        }
        // SELL
        else if(shopType == "SELL"){
            int stack = Mathf.RoundToInt(itemInforStack.value);
            int totalPrice = currentSellItem.item.price * stack;

            playerStats.coin += totalPrice;
            coinText.text = playerStats.coin.ToString();

            int remainStack = playerInput.inventoryManager.inventorySlots[currentSellItem.index].stackSize - stack;
            // Bán tất cả
            if(remainStack == 0){
                playerInput.inventoryManager.inventorySlots[currentSellItem.index].DeleteItem();
                GenerateShopSELL();
                itemInfo.SetActive(false);
            }else{
                playerInput.inventoryManager.inventorySlots[currentSellItem.index].AddStackAmount(-stack);
                playerInput.inventoryManager.inventorySlots[currentSellItem.index].UpdateSlot();
                currentSellItem.UpdateStackSize(remainStack);
            }
            playerInput.playerManager.characterSound.PlaySound("buySell");
        }
        else{
            Debug.Log("shopType is not valid");
        }
    }
}
