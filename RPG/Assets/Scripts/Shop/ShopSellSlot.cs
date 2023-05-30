using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Enums;

public class ShopSellSlot : MonoBehaviour
{
    public ShopManager shopManager; 
    [Header("Item")]
    public Item item;
    public int stackSize;
    public int index;

    [Header("Shop Slot Refs")]
    public Image icon;
    public TMP_Text itemName;
    public TMP_Text itemStack;
    public TMP_Text itemDescription;
    public TMP_Text itemPrice;

    public void InitialSlot(Item _item, int _stackSize, int _index, ShopManager _shopManager){
        item = _item;
        index = _index;
        stackSize = _stackSize;
        shopManager = _shopManager;

        icon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        if(stackSize > 1){
            itemStack.text = "x" + stackSize.ToString();
        }else{
            itemStack.transform.parent.gameObject.SetActive(false);
        }
        itemDescription.text = item.itemDescription;
        itemPrice.text = item.price.ToString();
    }

    public void InitialItemInfomation(){
        shopManager.itemInfo.SetActive(true);
        shopManager.itemInforButtonText.text = "SELL";
        shopManager.currentSellItem = this;

        shopManager.itemInforIcon.sprite = item.itemIcon;
        shopManager.itemInforName.text = item.itemName;
        shopManager.itemInforDescription.text = item.itemDescription;
        shopManager.itemInforStats.text = GetAllItemStats();
        
        if(item.maxStack > 1){
            shopManager.itemInforStack.gameObject.SetActive(true);
            shopManager.itemInforStackText.transform.parent.gameObject.SetActive(true);

            shopManager.itemInforStack.minValue = 1;
            shopManager.itemInforStack.maxValue = stackSize;
            shopManager.itemInforStack.value = 1;
            shopManager.itemInforStackText.text = Mathf.RoundToInt(shopManager.itemInforStack.value).ToString();

            // Add Event
            shopManager.itemInforStack.onValueChanged.AddListener(delegate {ItemStackChange ();});
        }else{
            shopManager.itemInforStack.value = 1;
            shopManager.itemInforStack.gameObject.SetActive(false);
            shopManager.itemInforStackText.transform.parent.gameObject.SetActive(false);
        }
        shopManager.itemInforTotalPrice.text =  "TOTAL: " + item.price.ToString();
    }

    public void UpdateStackSize(int newStack){
        int preValue = Mathf.RoundToInt(shopManager.itemInforStack.value);
        stackSize = newStack;

        itemStack.text = "x" + stackSize.ToString();
        
        shopManager.itemInforStack.minValue = 1;
        shopManager.itemInforStack.maxValue = stackSize;
        if(preValue > stackSize){
            shopManager.itemInforStack.value = stackSize;
        }
        shopManager.itemInforStackText.text = Mathf.RoundToInt(shopManager.itemInforStack.value).ToString();
    }

    string GetAllItemStats(){
        switch (item.itemType){
            case ItemType.Weapon:
                WeaponData weaponData = item as WeaponData;
                if(weaponData.weaponType == WeaponType.Sword) return GetSwordStats(weaponData);
                else if(weaponData.weaponType == WeaponType.Shield) return GetShieldStats(weaponData);
                else if(weaponData.weaponType == WeaponType.Bow) return GetBowStats(weaponData);

                break;
            case ItemType.Ammo:
                return GetArrowStats(item as ArrowData);
            case ItemType.Consumable:
                return GetConsumableStats(item as Flask);
            case ItemType.Equipment:
                return GetEquipmentStats(item as EquipmentData);
            default:
               break;
        }

        return "";
    }

    void ItemStackChange(){
        int stack = Mathf.RoundToInt(shopManager.itemInforStack.value);
		shopManager.itemInforStackText.text = stack.ToString();

        int totalPrice = item.price * stack;
        shopManager.itemInforTotalPrice.text =  "TOTAL: " + totalPrice.ToString();
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

    string GetConsumableStats(Flask _item) => 
$@"→ AMOUNT
   • Amount: {_item.amount.ToString()}
";

    string GetEquipmentStats(EquipmentData _item) => 
$@"→ STATS BUFF
   • DEF: {_item.def.ToString()}
   • ATK: {_item.atk.ToString()}
";

}
