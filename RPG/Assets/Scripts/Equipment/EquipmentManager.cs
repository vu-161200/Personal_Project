using UnityEngine;
using static Enums;

public class EquipmentManager : MonoBehaviour
{
    [Header("Refs")]
    PlayerEquipment playerEquipment;
    PlayerStats playerStats;

    [Header("Equipment Model Changers")]
    ArmorModelChanger armorModelChanger;
    BootsModelChanger bootsModelChanger;
    CapeModelChanger capeModelChanger;
    GlovesModelChanger glovesModelChanger;
    HelmetModelChanger helmetModelChanger;
    LegsModelChanger legsModelChanger;

    [Header("Bow Item")]
    public InventorySlot arrowSlot;
    public GameObject quiverObj;

    [Header("Consumable Item")]
    public InventorySlot consumableSlot;

    private void Awake() {
        // blockingCollider = GetComponentInChildren<BlockingCollider>();
        playerEquipment = GetComponent<PlayerEquipment>();
        playerStats = GetComponent<PlayerStats>();

        armorModelChanger = GetComponentInChildren<ArmorModelChanger>();
        bootsModelChanger = GetComponentInChildren<BootsModelChanger>();
        capeModelChanger = GetComponentInChildren<CapeModelChanger>();
        glovesModelChanger = GetComponentInChildren<GlovesModelChanger>();
        helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
        legsModelChanger = GetComponentInChildren<LegsModelChanger>();
    }

    // Mặc model
    public void EquipModel(string modelName, EquipType modelType){
        switch (modelType){
            case EquipType.Helmet:
                helmetModelChanger.UnEquipAllModels();
                helmetModelChanger.EquipModelByName(modelName);
                playerStats.damageAbsorptionHelmet = modelName == "" ? 0 : playerEquipment.helmet.def;
                break;
            case EquipType.Armor:
                armorModelChanger.UnEquipAllModels();
                armorModelChanger.EquipModelByName(modelName);
                playerStats.damageAbsorptionArmor = modelName == "" ? 0 : playerEquipment.armor.def;
                break;
            case EquipType.Boots:
                bootsModelChanger.UnEquipAllModels();
                bootsModelChanger.EquipModelByName(modelName);
                playerStats.damageAbsorptionBoots = modelName == "" ? 0 : playerEquipment.boots.def;
                break;
            case EquipType.Legs:
                legsModelChanger.UnEquipAllModels();
                legsModelChanger.EquipModelByName(modelName);
                playerStats.damageAbsorptionLegs = modelName == "" ? 0 : playerEquipment.legs.def;
                break;
            case EquipType.Gloves:
                glovesModelChanger.UnEquipAllModels();
                glovesModelChanger.EquipModelByName(modelName);
                playerStats.damageAbsorptionGloves = modelName == "" ? 0 : playerEquipment.gloves.def;
                playerStats.atkGloves = modelName == "" ? 0 : playerEquipment.gloves.atk;
                break;
            case EquipType.Cape:
                capeModelChanger.UnEquipAllModels();
                capeModelChanger.EquipModelByName(modelName);
                playerStats.damageAbsorptionCape = modelName == "" ? 0 : playerEquipment.cape.def;
                break;
            default:
                break;
        }
    }

    // Cập nhật arrow và stack
    public void UpdateArrow(ArrowData arrow, int arrowStack){
        if(arrow != null && arrowStack > 0){
            quiverObj.SetActive(true);
            arrowSlot.item = arrow;
            arrowSlot.stackSize = arrowStack;
            arrowSlot.UpdateSlot();
        }else{
            quiverObj.SetActive(false);
            arrowSlot.DeleteItem();
        }
    }

    // Cập nhật comsumable (HP, EXP, MP) và stack
    public void UpdateConsumable(Consumable consumable, int consumableStack){
        if(consumable != null && consumableStack > 0){
            consumableSlot.item = consumable;
            consumableSlot.stackSize = consumableStack;
            consumableSlot.UpdateSlot();
        }else{
            consumableSlot.DeleteItem();
        }
    }
}
