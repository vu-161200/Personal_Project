using UnityEngine;
using static Enums;

public class PlayerCombat : MonoBehaviour
{
    public string lastAttack;

    public AttackType currentAttackType;

    PlayerManager playerManager;

    [Header("Animation One Hand")]
    public string oh_heavyAttack = "1H Attack 0";
    public string oh_lightAttack1 = "1H Attack 1";
    public string oh_lightAttack2 = "1H Attack 2";

    void Awake(){
        playerManager = GetComponent<PlayerManager>();
    }

    // Lấy stamina khi sử dụng của vũ khí đang sử dụng hiện tại
    public float GetStaminaCost(){
        if(playerManager.isUsingRightHand){
            if(currentAttackType == AttackType.Light){
                return playerManager.playerEquipment.rightWeapon.baseStaminaCost * playerManager.playerEquipment.rightWeapon.lightAttackStaminaMultiplier;
            }else if(currentAttackType == AttackType.Heavy){
                return playerManager.playerEquipment.rightWeapon.baseStaminaCost * playerManager.playerEquipment.rightWeapon.heavyAttackStaminaMultiplier;
            }
        }else if(playerManager.isUsingLeftHand){
            if(currentAttackType == AttackType.Light){
                return playerManager.playerEquipment.leftWeapon.baseStaminaCost * playerManager.playerEquipment.leftWeapon.lightAttackStaminaMultiplier;
            }else if(currentAttackType == AttackType.Heavy){
                return playerManager.playerEquipment.leftWeapon.baseStaminaCost * playerManager.playerEquipment.leftWeapon.heavyAttackStaminaMultiplier;
            }
        }

        return 0;
    }

    // Giảm stamina của player
    public void DrainStaminaBasedOnAttack(float cost){
        playerManager.playerStats.DeductStamina(cost);
    }

    // Cập nhật thông tin của vũ khí thực hiện block (%damage giảm, % stamina tốn)
    public void SetBlockingAbsorptionFromWeapon(){
        if(playerManager.isUsingRightHand){
            playerManager.playerStats.blockingDamageAbsorption = playerManager.playerEquipment.rightWeapon.damageAbsorption;
            playerManager.playerStats.blockingStabilityRating = playerManager.playerEquipment.rightWeapon.stability;
        }else if(playerManager.isUsingLeftHand){
            playerManager.playerStats.blockingDamageAbsorption = playerManager.playerEquipment.leftWeapon.damageAbsorption;
            playerManager.playerStats.blockingStabilityRating = playerManager.playerEquipment.leftWeapon.stability;
        }
    }

    // Thực hiện block
    public void AttemptBlock(DamageCollider attackingWeapon, float damage){
        // Tính toán lượng stamina cần để chặn damage đó
        float staminaDamageAbsorption = damage * playerManager.playerStats.blockingStabilityRating / 100;

        float staminaDamage = damage - staminaDamageAbsorption;

        // Cập nhật lại stamina
        DrainStaminaBasedOnAttack(staminaDamage);

        // Trường hợp không đủ stamina để chặn ==> Break shield ==> Mất máu
        if(playerManager.playerStats.currentStamina <= 0){
            playerManager.isBlocking = false;
            // Guard Break 
            playerManager.playerStats.TakeDameAfterBlock(damage, "Guard Break");
        }
        // Đủ ==> Chặn đc ==> aniamtion khiên khi bị damage + không mất damage
        else{
            // Block Animation
            playerManager.playerStats.TakeDameAfterBlock(0);
        }
    }

}
