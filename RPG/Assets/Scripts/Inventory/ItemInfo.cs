using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Enums;

// Thông tin của vật phẩm đó
public class ItemInfo : MonoBehaviour
{
    public Image icon;
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public TMP_Text itemStats;
    public TMP_Text itemSellPrice;
    public TMP_Text itemBuyPrice;
    
    // Khởi tạo UI
    public void Initialize(Item item){
        icon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        itemDescription.text = item.itemDescription;


        switch (item.itemType){
            case ItemType.Weapon:
                WeaponData weaponData = item as WeaponData;
                
                if(weaponData.weaponType == WeaponType.Sword) itemStats.text = GetSwordStats(weaponData);
                else if(weaponData.weaponType == WeaponType.Shield) itemStats.text = GetShieldStats(weaponData);
                else if(weaponData.weaponType == WeaponType.Bow) itemStats.text = GetBowStats(weaponData);
                
                break;
            case ItemType.Ammo:
                itemStats.text = GetArrowStats(item as ArrowData);
                break;
            case ItemType.Consumable:
                itemStats.text = GetConsumableStats(item as Flask);
                break;
            case ItemType.Equipment:
                itemStats.text = GetEquipmentStats(item as EquipmentData);
                break;
            default:
                break;
        }

        itemBuyPrice.text = "BUY: " + item.price.ToString();
        itemSellPrice.text = "SELL: " + item.price.ToString();
    }

    string GetSwordStats(WeaponData _item) => 
$@"→ DAMAGE
   • Base Damage: {_item.baseDamage.ToString()}
   • Light Attack Damage: {(_item.lightAttackDamageModifier * _item.baseDamage).ToString()}
   • Heavy Attack Damage: {(_item.heavyAttackDamageModifier * _item.baseDamage).ToString()}
                                                                    
→ STAMINA
   • Base Stamina: {_item.baseStaminaCost.ToString()}
   • Light Attack Stamina: {(_item.lightAttackStaminaMultiplier * _item.baseStaminaCost).ToString()}
   • Heavy Attack Stamina: {(_item.heavyAttackStaminaMultiplier * _item.baseStaminaCost).ToString()}
";

    string GetShieldStats(WeaponData _item) => 
$@"→ ABSORPTION
   • Damage Absorption: {_item.damageAbsorption.ToString()}
   • Stability: {_item.stability.ToString()}
                                                                    
→ STAMINA
   • Base Stamina: {_item.baseStaminaCost.ToString()}
   • Guard Break Modifier: {_item.guardBreakModifier.ToString()}
";

    string GetBowStats(WeaponData _item) => 
$@"→ DAMAGE
   • Base Damage: {_item.baseDamage.ToString()}

→ ABSORPTION
   • Damage Absorption: {_item.damageAbsorption.ToString()}
   • Stability: {_item.stability.ToString()}
";

    string GetArrowStats(ArrowData _item) => 
$@"→ DAMAGE
   • Base Damage: {_item.damage.ToString()}

→ Velocity
   • Forward Velocity: {_item.forwardVelocity.ToString()}
   • Upward Velocity: {_item.upwardVelocity.ToString()}
   • Arrow Mass: {_item.arrowMass.ToString()}
";

    string GetConsumableStats(Flask _item) => _item == null ? "" :
$@"→ AMOUNT
   • Amount: {_item.amount.ToString()}
";

    string GetEquipmentStats(EquipmentData _item) => 
$@"→ STATS BUFF
   • DEF: {_item.def.ToString()}
   • ATK: {_item.atk.ToString()}
";

}
