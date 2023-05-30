using UnityEngine;
using static Enums;

// Thông tin vũ khí

// fileName: Tên tệp mặc định khi tạo mới loại này.
// menuName: Tên hiển thị cho loại này được hiển thị trong Assets/Create menu
[CreateAssetMenu(fileName = "WeaponData", menuName = "Items/Weapon")]
public class WeaponData : Item
{
    [Space]
    public GameObject weaponPrefab;
    public bool isUnarmed;

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Damage")]
    public float baseDamage = 25f;

    [Header("Damage Modifiers")]
    public float lightAttackDamageModifier = 1f;
    public float heavyAttackDamageModifier = 1.5f;

    [Header("Absorption")]
    public float damageAbsorption = 0;

    [Header("Stability")]
    public int stability = 67;

    [Header("Stamina Cost")]
    public int baseStaminaCost = 10;
    public float lightAttackStaminaMultiplier = 1f;
    public float heavyAttackStaminaMultiplier = 1.5f;
    public float guardBreakModifier = 1f;

    [Header("Item Actions")]
    public ItemAction tap_LM; // Nhấn chuột trái
    public ItemAction action; // Giữ chuột trái
    public ItemAction tap_RM; // Nhấn chuột phải
    public ItemAction hold_RM; // Giữ chuột phải
    public ItemAction tap_MM; // Nhấn chuột giữa


    public void Awake(){
        itemType = ItemType.Weapon;

        if(itemName.Contains("Sword")){
            equipType = EquipType.Hand;
        }else if(itemName.Contains("Bow")){
            equipType = EquipType.LeftHand;
        }else if(itemName.Contains("Shield")){
            equipType = EquipType.LeftHand;
        }
    }
}
