using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static Enums;

// Một ô trong kho đồ
public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Popup Settings")]
    public float offsetX = 135f;
    public float offsetY = 0f;

    [Header("Item Information")]
    public ItemType itemType = ItemType.None;
    public EquipType equipType = EquipType.None;
    public Image icon;
    public Item item;
    public int stackSize = 1;
    public TMP_Text stackText;

    [Header("Refs")]
    DragDropHandler dragDropHandler;
    InventoryManager inventoryManager;

    private void Start() {
        dragDropHandler = GetComponentInParent<DragDropHandler>();
        inventoryManager = GetComponentInParent<InventoryManager>();

        UpdateSlot();
    }

    public void UpdateSlot(){
        if(item != null && item.itemType == ItemType.Consumable && stackSize <= 0){
            item = null;
        }

        if(item == null){
            icon.sprite = null;
            icon.gameObject.SetActive(false);
            stackText.gameObject.SetActive(false);
        }else{
            icon.sprite = item.itemIcon;
            stackText.text = stackSize > 1 ? $"x{stackSize}" : "";

            icon.gameObject.SetActive(true);
            stackText.gameObject.SetActive(true);
        }
    }

    public void AddItem(Item _item, int _stackSze = 1){
        item = _item;
        stackSize = _stackSze;
    }

    public void AddStackAmount(int _stackSze = 1){
        stackSize += _stackSze;
    }

    public void DropItem(){
        GetComponentInParent<InventoryManager>().DropItem(this);
    }

    public void DeleteItem(){
        item = null;
        stackSize = 0;

        UpdateSlot();
    }

    // Kiểm tra có thể đặt vật phẩm vào hay không? (Cùng type)
    bool CanPlaceInSlot(InventorySlot _itemFrom, InventorySlot _itemTo){
        // Swap trong kho đồ hoặc trong rương
        if((_itemTo.itemType == ItemType.None || _itemTo.itemType == ItemType.Chest) && _itemTo.equipType == EquipType.None) return true;
        
        // Mặc trang bị/vũ khí/vật phẩm
        if(_itemFrom.item.itemType == _itemTo.itemType){
            // Hand = Right Hand = Left Hand
            if(_itemFrom.item.equipType == EquipType.Hand && (_itemTo.equipType == EquipType.RightHand || _itemTo.equipType == EquipType.LeftHand)) return true;
            else return _itemFrom.item.equipType == _itemTo.equipType;
        }

        return false;
    }

    // Sự kiện với chuột
    // Khi ấn chuột
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData){
        if(!dragDropHandler.isDragging){
            if(eventData.button == PointerEventData.InputButton.Left && item != null){
                // Xóa info item khi có
                inventoryManager.DestroyItemInfo();

                dragDropHandler.slotFrom = this;
                dragDropHandler.isDragging = true;
            }
        }
    }

    // Khi thả chuột
    // Cập nhật lại slotFrom và slotTo
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData){
        if(dragDropHandler.isDragging){
            // Drop
            if(dragDropHandler.slotTo == null){
                // Chỉ vứt được đồ vật đang nằm trong kho đồ
                if(dragDropHandler.slotFrom.itemType != ItemType.None){
                    dragDropHandler.slotTo = dragDropHandler.slotFrom;
                    dragDropHandler.isDragging = false;
                }else{
                    dragDropHandler.slotFrom.DropItem();
                    dragDropHandler.isDragging = false;
                }
            }
            // Drag & Drop
            else if(dragDropHandler.slotTo != null){
                if(CanPlaceInSlot(dragDropHandler.slotFrom, dragDropHandler.slotTo)){
                    inventoryManager.SwapItems(dragDropHandler.slotFrom, dragDropHandler.slotTo);
                    dragDropHandler.isDragging = false;
                }else{
                    dragDropHandler.slotTo = dragDropHandler.slotFrom;
                    dragDropHandler.isDragging = false;
                }
            }
        }
    }

    // Khi con trỏ chỉ vào 1 ô ==> Hiển thị thông tin của vật phẩm đấy
    // Cập nhật lại slotFrom và slotTo
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData){
        if(dragDropHandler.isDragging){
            dragDropHandler.slotTo = this;
        }else{
            if(item != null){
                inventoryManager.DisplayItemInfo(item, transform.position, offsetX, offsetY);
            }
        }
    }

    // Khi con trỏ rời khỏi nút đó ==> Ẩn thông tin nếu đã hiển thị
    // Cập nhật lại slotFrom và slotTo
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData){
        if(dragDropHandler.isDragging){
            dragDropHandler.slotTo = null;
        }else{
            dragDropHandler.slotTo = this;
            
            inventoryManager.DestroyItemInfo();
        }
    }
}
