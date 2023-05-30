using UnityEngine;
using static Enums;

// Lưu trữ thông tin vật phẩm hiện tại của người dùng
public class PlayerEquipment : MonoBehaviour
{
    [Header("Refs")]
    WeaponSlotManager weaponSlotManager;
    public EquipmentManager equipmentManager;

    [Header("Current Action Item")]
    public Item currentItemBeingUsed;

    [Header("Consumable Items")]
    public Consumable consumable;
    public int consumableStack;

    [Header("Arrow")]
    public ArrowData arrow;
    public int arrowStack = 1;

    [Header("Weapon")]
    public WeaponData rightWeapon;
    public WeaponData leftWeapon;
    public WeaponData unarmedWeapon;

    [Header("Equipment")]
    public EquipmentData helmet;
    public EquipmentData armor;
    public EquipmentData boots;
    public EquipmentData legs;
    public EquipmentData gloves;
    public EquipmentData cape;

    void Awake(){
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        equipmentManager = GetComponent<EquipmentManager>();
    }

    void Start(){
        LoadStartEquipment();
    }

    // Set null tất cả ==> Khi chết
    public void DeleteAllEquipments(){
        arrow = null;
        rightWeapon = null;
        leftWeapon = null;
        helmet = null;
        armor = null;
        boots = null;
        legs = null;
        gloves = null;
        cape = null;

        LoadStartEquipment();
    }

    // Khởi tạo trang bị có sẵn của người dùng
    public void LoadStartEquipment(){
        // Weapon
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);

        // Helmet
        equipmentManager.EquipModel(helmet == null ? "" : helmet.modelName, EquipType.Helmet);

        // Armor
        equipmentManager.EquipModel(armor == null ? "" : armor.modelName, EquipType.Armor);

        // Boots
        equipmentManager.EquipModel(boots == null ? "" : boots.modelName, EquipType.Boots);

        // Legs
        equipmentManager.EquipModel(legs == null ? "" : legs.modelName, EquipType.Legs);

        // Gloves
        equipmentManager.EquipModel(gloves == null ? "" : gloves.modelName, EquipType.Gloves);

        // Cape
        equipmentManager.EquipModel(cape == null ? "" : cape.modelName, EquipType.Cape);

        // Arrow
        equipmentManager.UpdateArrow(arrow, arrowStack);

        // Consumable
        equipmentManager.UpdateConsumable(consumable, consumableStack);
    }

    // stack: dùng với arrow or consumable items
    // hotbarType để phân biệt giữa arrow và consumable
    // Còn lại k cần dùng
    public void UpdateEquipment(Item item, EquipType type, int stack = 0, ItemType hotbarType = ItemType.Consumable){
        switch (type)
        {
            case EquipType.LeftHand:
                leftWeapon = item == null ? null : (WeaponData) item;
                weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
                break;
            case EquipType.RightHand:
                rightWeapon = item == null ? null : (WeaponData) item;
                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
                break;
            case EquipType.Hotbar:
                if(hotbarType == ItemType.Consumable){
                    consumable = item == null ? null : (Consumable) item;
                    consumableStack = item == null ? 0 : stack;
                    equipmentManager.UpdateConsumable(consumable, consumableStack);
                }else if(hotbarType == ItemType.Ammo){
                    arrow = item == null ? null : (ArrowData) item;
                    arrowStack = item == null ? 0 : stack;
                    equipmentManager.UpdateArrow(arrow, arrowStack);
                }
                break;
            case EquipType.Armor:
                armor = item == null ? null : (EquipmentData) item;
                equipmentManager.EquipModel(armor == null ? "" : armor.modelName, EquipType.Armor);
                break;
            case EquipType.Boots:
                boots = item == null ? null : (EquipmentData) item;
                equipmentManager.EquipModel(boots == null ? "" : boots.modelName, EquipType.Boots);
                break;
            case EquipType.Cape:
                cape = item == null ? null : (EquipmentData) item;
                equipmentManager.EquipModel(cape == null ? "" : cape.modelName, EquipType.Cape);
                break;
            case EquipType.Gloves:
                gloves = item == null ? null : (EquipmentData) item;
                equipmentManager.EquipModel(gloves == null ? "" : gloves.modelName, EquipType.Gloves);
                break;
            case EquipType.Helmet:
                helmet = item == null ? null : (EquipmentData) item;
                equipmentManager.EquipModel(helmet == null ? "" : helmet.modelName, EquipType.Helmet);
                break;
            case EquipType.Legs:
                legs = item == null ? null : (EquipmentData) item;
                equipmentManager.EquipModel(legs == null ? "" : legs.modelName, EquipType.Legs);
                break;
            default:
                break;
        }
    }
}
