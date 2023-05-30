using UnityEngine;

[CreateAssetMenu(menuName = "Items/Flask")]
public class Flask : Consumable
{
    [Header("Consumable Type")]
    public string type;

    [Header("Amount")]
    public int amount;

    [Header("")]
    public GameObject fx;

    public override void AttemptToConsumableItem(AnimatorManager animatorManager, WeaponSlotManager weaponSlotManager, PlayerEffects playerEffects)
    {
        base.AttemptToConsumableItem(animatorManager, weaponSlotManager, playerEffects);

        // Tạo đối tượng
        GameObject flask = Instantiate(itemModel, weaponSlotManager.leftHand.position);
        // Thêm vào playereffects
        playerEffects.currentParticleFX = fx;
        // Tạo flask trên tay + animation
        playerEffects.amount = amount;
        playerEffects.type = type;
        playerEffects.instantiateFX = flask;
        weaponSlotManager.leftHand.UnloadWeapon();
        // 
    }
}
