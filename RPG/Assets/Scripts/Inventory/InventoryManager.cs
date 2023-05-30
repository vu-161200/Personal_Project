using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Enums;

public class InventoryManager : MonoBehaviour
{
    [Header("Settings")]
    public int inventorySize = 48;

    [Header("UI")]
    public GameObject inventoryUI;
    public GameObject hudUI;
    public GameObject attributePointsUI;
    public GameObject addAttributePointsButton;
    public GameObject equipmentUI;

    [Header("Drop Item")]
    public GameObject dropModel;
    public Transform dropPos;

    [Header("Item Info")]
    public Transform canvas;
    public GameObject itemInfoPrefab;
    private GameObject currentItemInfo = null;

    [Header("Inventory Slot")]
    public InventorySlot[] inventorySlots;
    public Transform inventorySlotParent;
    public GameObject inventorySlotPrefab;
    
    [Header("Equipment Slot")]
    public TMP_Text playerInfo;
    public TMP_Text attributePointText;
    public TMP_Text coinsText;
    public InventorySlot[] equipmentSlots;

    [Header("Ref")]
    PlayerEquipment playerEquipment;
    PlayerStats playerStats;

    void Start(){
        playerEquipment = GetComponent<PlayerEquipment>();
        playerStats = GetComponent<PlayerStats>();

        GenerateInventory();
    }

    // Khởi tạo danh sách trang bị hiện tại của player
    void GenerateEquipment(){
        // Helmet: index 0
        if(playerEquipment.helmet != null){
            equipmentSlots[0].AddItem(playerEquipment.helmet);
            equipmentSlots[0].UpdateSlot();
        }else{
            equipmentSlots[0].DeleteItem();
        }

        // Cape: index 1
        if(playerEquipment.cape != null){
            equipmentSlots[1].AddItem(playerEquipment.cape);
            equipmentSlots[1].UpdateSlot();
        }else{
            equipmentSlots[1].DeleteItem();
        }

        // Armor: index 2
        if(playerEquipment.armor != null){
            equipmentSlots[2].AddItem(playerEquipment.armor);
            equipmentSlots[2].UpdateSlot();
        }else{
            equipmentSlots[2].DeleteItem();
        }

        // Armor: index 3
        if(playerEquipment.legs != null){
            equipmentSlots[3].AddItem(playerEquipment.legs);
            equipmentSlots[3].UpdateSlot();
        }else{
            equipmentSlots[3].DeleteItem();
        }

        // Boots: index 4
        if(playerEquipment.boots != null){
            equipmentSlots[4].AddItem(playerEquipment.boots);
            equipmentSlots[4].UpdateSlot();
        }else{
            equipmentSlots[4].DeleteItem();
        }

        // Boots: index 5
        if(playerEquipment.gloves != null){
            equipmentSlots[5].AddItem(playerEquipment.gloves);
            equipmentSlots[5].UpdateSlot();
        }else{
            equipmentSlots[5].DeleteItem();
        }

        // Left Weapon: index 8
        if(playerEquipment.leftWeapon.weaponType != WeaponType.Unarmed){
            equipmentSlots[8].AddItem(playerEquipment.leftWeapon);
            equipmentSlots[8].UpdateSlot();
        }else{
            equipmentSlots[8].DeleteItem();
        }

        // Right Weapon: index 7
        if(playerEquipment.rightWeapon.weaponType != WeaponType.Unarmed){
            equipmentSlots[7].AddItem(playerEquipment.rightWeapon);
            equipmentSlots[7].UpdateSlot();
        }else{
            equipmentSlots[7].DeleteItem();
        }

        // Hotbar: index 6 
        if(playerEquipment.consumable != null && playerEquipment.consumableStack > 0){
            equipmentSlots[6].AddItem(playerEquipment.consumable, playerEquipment.consumableStack);
            equipmentSlots[6].UpdateSlot();
        }else{
            equipmentSlots[6].DeleteItem();
        }

        // Arrow: index 9
        if(playerEquipment.arrow != null && playerEquipment.arrowStack > 0){
            equipmentSlots[9].AddItem(playerEquipment.arrow, playerEquipment.arrowStack);
            equipmentSlots[9].UpdateSlot();
        }else{
            equipmentSlots[9].DeleteItem();
        }

        // Player Information
        GeneratePlayerInfo();
    }

    void GeneratePlayerInfo(){
        if(playerStats.attributePoints > 0) addAttributePointsButton.SetActive(true);
        else addAttributePointsButton.SetActive(false);

        attributePointText.text = "Points: " + playerStats.attributePoints.ToString();
        coinsText.text = "Coins: " + playerStats.coin.ToString();

        playerInfo.text = 
        $@"Level: {playerStats.level} ({playerStats.currentEXP} / {playerStats.levelEXP})
        
HP: {playerStats.currentHealth} / {playerStats.maxHealth}
Stamina: {playerStats.currentStamina} / {playerStats.maxStamina}
Base ATK: {playerStats.baseATK}
Base DEF: {playerStats.baseDEF}
        ";
    }

    // Khởi tạo giao diện kho đồ (các ô)
    void GenerateInventory(){
        List<InventorySlot> _inventorySlots = new List<InventorySlot>();

        for (int i = 0; i < inventorySize; i++){
            InventorySlot slot = Instantiate(inventorySlotPrefab.gameObject, inventorySlotParent).GetComponent<InventorySlot>();
        
            _inventorySlots.Add(slot);
        }

        inventorySlots = _inventorySlots.ToArray();
    }

    // Thêm vật phẩm vào kho đồ khi nhặt vật phẩm
    public void AddItem(PickUp pickUp){
        // Vật phẩm có thể stack (số lượng > 1)
        if(pickUp.item.maxStack > 1){
            // Vị trí của vật phẩm đó
            InventorySlot stackableSlot = null;

            // Tìm kiểm xem vật phẩm đó có trong kho đồ hay chưa
            for (int i = 0; i < inventorySlots.Length; i++){
                if(inventorySlots[i].item != null){
                    if(inventorySlots[i].item == pickUp.item && inventorySlots[i].stackSize < pickUp.item.maxStack){
                        stackableSlot = inventorySlots[i];
                        break;
                    }
                }
            }

            // Vật phẩm đã có trong kho đồ
            if(stackableSlot != null){
                // Vượt quá maxStack
                // Gán stack của vật phẩm hiện tại = max
                // Tạo một vị trí mới = stack còn lại
                if(stackableSlot.stackSize + pickUp.stackSize > pickUp.item.maxStack){
                    int amountLeft = (stackableSlot.stackSize + pickUp.stackSize) - pickUp.item.maxStack;

                    stackableSlot.AddItem(pickUp.item, pickUp.item.maxStack);

                    for (int i = 0; i < inventorySlots.Length; i++){
                        if (inventorySlots[i].item == null){
                            inventorySlots[i].AddItem(pickUp.item, amountLeft);
                            inventorySlots[i].UpdateSlot();

                            // Event khi nhận được 1 vật phẩm (Với nhiệm vụ nhặt/thu thập vật phẩm)
                            EventManager.ItemFetched(pickUp.item, pickUp.stackSize);

                            break;
                        }
                    }

                    // Hủy item đã nhặt
                    Destroy(pickUp.gameObject);
                }
                // tổng stack <= maxStack ==> Cập nhật stack
                else{
                    stackableSlot.AddStackAmount(pickUp.stackSize);
                    
                    // Event khi nhận được 1 vật phẩm (Với nhiệm vụ nhặt/thu thập vật phẩm)
                    EventManager.ItemFetched(pickUp.item, pickUp.stackSize);
                    
                    // Hủy item đã nhặt
                    Destroy(pickUp.gameObject);
                }

                // Cập nhật lại thông tin của slot đó
                stackableSlot.UpdateSlot();
            }
            // Vật phẩm chưa có trong kho đồ
            else{
                // Vị trí trống để đặt vật phẩm đó vào
                InventorySlot emptySlot = null;

                // Tìm vị trí trống trong danh sách
                for (int i = 0; i < inventorySlots.Length; i++){
                    if (inventorySlots[i].item == null){
                        emptySlot = inventorySlots[i];
                        break;
                    }
                }

                // Nếu tìm thấy ==> Thêm vào
                if (emptySlot != null){
                    emptySlot.AddItem(pickUp.item, pickUp.stackSize);
                    emptySlot.UpdateSlot();
                    // Event
                    EventManager.ItemFetched(pickUp.item, pickUp.stackSize);

                    Destroy(pickUp.gameObject);
                }
                // Trường hợp không còn vị trí trống ==> vứt ra map
                else{
                    pickUp.transform.position = dropPos.position;
                }
            }
        }
        // Vật phẩm chỉ có 1 stack (Kiếm, Trang bị ...)
        else{
            // Vị trí trống để thêm vật phẩm đó
            InventorySlot emptySlot = null;

            // Tìm vị trí trống
            for (int i = 0; i < inventorySlots.Length; i++){
                if (inventorySlots[i].item == null){
                    emptySlot = inventorySlots[i];
                    break;
                }
            }

            // Nếu còn ==> thêm vào
            if (emptySlot != null){
                emptySlot.AddItem(pickUp.item, pickUp.stackSize);
                emptySlot.UpdateSlot();
                // Event
                EventManager.ItemFetched(pickUp.item, pickUp.stackSize);

                Destroy(pickUp.gameObject);
            }
            // Không còn ==> vứt ra map
            else{
                pickUp.transform.position = dropPos.position;
            }

        }
    }

    // Tương thêm vật phẩm vào kho đồ khi hoàn thành nhiệm vụ
    // Tương tự khi nhặt
    // Khác ở chỗ không phải Destroy
    public void AddItem(Item item, int stackSize){
        // Stackable
        if(item.maxStack > 1){
            InventorySlot stackableSlot = null;

            for (int i = 0; i < inventorySlots.Length; i++){
                if(inventorySlots[i].item != null){
                    if(inventorySlots[i].item == item && inventorySlots[i].stackSize < item.maxStack){
                        stackableSlot = inventorySlots[i];
                        break;
                    }
                }
            }

            if(stackableSlot != null){
                // Vượt quá maxStack
                if(stackableSlot.stackSize + stackSize > item.maxStack){
                    int amountLeft = (stackableSlot.stackSize + stackSize) - item.maxStack;

                    stackableSlot.AddItem(item, item.maxStack);

                    for (int i = 0; i < inventorySlots.Length; i++){
                        if (inventorySlots[i].item == null){
                            inventorySlots[i].AddItem(item, amountLeft);
                            inventorySlots[i].UpdateSlot();
                            // Event
                            EventManager.ItemFetched(item, stackSize);

                            break;
                        }
                    }
                }else{
                    stackableSlot.AddStackAmount(stackSize);
                    // Event
                    EventManager.ItemFetched(item, stackSize);
                }

                stackableSlot.UpdateSlot();
            }else{
                InventorySlot emptySlot = null;

                // Tìm vị trí trống
                for (int i = 0; i < inventorySlots.Length; i++){
                    if (inventorySlots[i].item == null){
                        emptySlot = inventorySlots[i];
                        break;
                    }
                }

                // Nếu tìm thấy ==> Thêm vào
                if (emptySlot != null){
                    emptySlot.AddItem(item, stackSize);
                    emptySlot.UpdateSlot();

                    EventManager.ItemFetched(item, stackSize);
                }else{
                    DropItem(item, stackSize);
                }
            }
        }else{ // Vật phẩm chỉ có 1 stack
            InventorySlot emptySlot = null;

            // Tìm vị trí trống
            for (int i = 0; i < inventorySlots.Length; i++){
                if (inventorySlots[i].item == null){
                    emptySlot = inventorySlots[i];
                    break;
                }
            }

            // Nếu còn ==> thêm vào
            if (emptySlot != null){
                emptySlot.AddItem(item, stackSize);
                emptySlot.UpdateSlot();
                // Event
                EventManager.ItemFetched(item, stackSize);
            }else{
                DropItem(item, stackSize);
            }
        }
    }

    // Vứt vật phẩm "trong kho đồ" ra map
    public void DropItem(InventorySlot item){
        // Khởi tạo object tại vị trí 'dropPos' và gán thông tin cho nó
        PickUp pickUp = Instantiate(dropModel, dropPos).GetComponent<PickUp>();

        pickUp.transform.position = dropPos.position;
        pickUp.transform.SetParent(null);

        pickUp.item = item.item;
        pickUp.stackSize = item.stackSize;
        pickUp.SetSerialize();

        // Xóa vật phẩm hiện tại trong kho đồ
        item.DeleteItem();
    }

    // Vứt vật phẩm với stack ra map
    public void DropItem(Item item, int stackSize){
        PickUp pickUp = Instantiate(dropModel, dropPos).GetComponent<PickUp>();

        pickUp.transform.position = dropPos.position;
        pickUp.transform.SetParent(null);

        pickUp.item = item;
        pickUp.stackSize = stackSize;
        pickUp.SetSerialize();
    }
    
    // Đổi chỗ vật phẩm
    public void SwapItems(InventorySlot from, InventorySlot to){
        // Sự kiện khi nhặt vật phẩm trong rương (Kéo từ rương vào kho đồ)
        if(from.itemType == ItemType.Chest && to.itemType == ItemType.None){
            // Event
            EventManager.ItemFetched(from.item, from.stackSize);
        }

        // Cập nhật trang bị hoặc vũ khí
        if(from.itemType != ItemType.Chest && to.itemType != ItemType.Chest){
            // Kéo vật phẩm từ kho đồ vào trang bị 
            if(IsEquipmentItem(to) && !IsEquipmentItem(from)){
                playerEquipment.UpdateEquipment(from.item, to.equipType, from.stackSize, to.itemType);
            }
            // Kéo từ trang bị về kho đồ
            else if(!IsEquipmentItem(to) && IsEquipmentItem(from)){
                playerEquipment.UpdateEquipment(to.item, from.equipType, to.stackSize, from.itemType);
            }
            // Đổi chỗ 2 vũ khí trái và phải
            else{
                playerEquipment.UpdateEquipment(to.item, from.equipType);
                playerEquipment.UpdateEquipment(from.item, to.equipType);
            }
        }

        // Swap 2 vật phẩm khác nhau
        if(from.item != to.item){
            Item _item = from.item;
            int _stackSize = from.stackSize;

            from.item = to.item;
            from.stackSize = to.stackSize;

            to.item = _item;
            to.stackSize = _stackSize;
        }
        // Swap 2 vật phẩm giống nhau
        else{
            // Vật phẩm có thể stack ==> Cộng dồn stack vào
            if(from.item.maxStack > 1){
                if(from.stackSize + to.stackSize > from.item.maxStack){
                    int amountLeft = (from.stackSize + to.stackSize) - from.item.maxStack;

                    to.stackSize = amountLeft;
                    from.stackSize = from.item.maxStack;
                }else{
                    to.stackSize += from.stackSize;
                    from.stackSize = 0;
                    from.item = null;
                }
            }
            // Không thể stack ==> Đổi chỗ
            else{
                Item _item = from.item;
                int _stackSize = from.stackSize;

                from.item = to.item;
                from.stackSize = to.stackSize;

                to.item = _item;
                to.stackSize = _stackSize;
            }
        }

        // Cập nhật lại 2 slot tương ứng
        from.UpdateSlot();
        to.UpdateSlot();

    }

    bool IsEquipmentItem(InventorySlot slot){
        return slot.itemType != ItemType.None && slot.itemType != ItemType.Chest;
    } 

    // Mở kho đồ ==> Khởi tạo trang bị
    public void OpenInventory(){
        GenerateEquipment();

        inventoryUI.SetActive(true);
        hudUI.SetActive(false);

        equipmentUI.SetActive(true);
        attributePointsUI.SetActive(false);
    }

    // Đóng kho đó
    public void CloseInventory(){
        inventoryUI.SetActive(false);
        hudUI.SetActive(true);

        // Fix lỗi nếu đã hiển thị thông tin vật phẩm mà đóng luôn thì vẫn hiện trên màn hình
        DestroyItemInfo();
    }

    // Vứt tất cả vật phẩm trong kho đồ khi chết
    public void DropAllItems(){
        // Drop items
        for (int i = 0; i < inventorySlots.Length; i++){
            if(inventorySlots[i].item != null){
                DropItem(inventorySlots[i]);
            }
        }

        // Drop equipment
        for (int i = 0; i < equipmentSlots.Length; i++){
            if(equipmentSlots[i].item != null){
                DropItem(equipmentSlots[i]);
            }
        }

        // Equipment
        playerEquipment.DeleteAllEquipments();
    }

    // Hiển thị thông tin vật phẩm khi di chuột vào
    public void DisplayItemInfo(Item item, Vector2 buttonPosition, float offsetX, float offsetY){
        if(currentItemInfo != null){
            Destroy(currentItemInfo.gameObject);
        }

        // Vị trí hiển thị
        buttonPosition.x += offsetX;
        buttonPosition.y += offsetY;

        currentItemInfo = Instantiate(itemInfoPrefab, buttonPosition, Quaternion.identity, canvas);
        currentItemInfo.GetComponent<ItemInfo>().Initialize(item);
    }

    // Ẩn thông tin vật phẩm
    public void DestroyItemInfo(){
        if(currentItemInfo != null){
            Destroy(currentItemInfo.gameObject);
        }
    }

    // Kiểm tra trong kho đồ có chìa khóa nhà giam không?
    // Áp dụng với cửa cần chìa khóa để mở
    public bool CheckPrisonKey(string ID){
        for (int i = 0; i < inventorySlots.Length; i++){
            if(inventorySlots[i].item != null && inventorySlots[i].item.ID == ID){
                return true;
            }
        }

        return false;
    }

    public void OpenAttributePointsUI(){
        equipmentUI.SetActive(false);
        attributePointsUI.SetActive(true);
    }

    public void CloseAttributePointsUI(){
        equipmentUI.SetActive(true);
        attributePointsUI.SetActive(false);

        GeneratePlayerInfo();
    }
}
