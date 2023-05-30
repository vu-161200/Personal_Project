using UnityEngine;
using static Enums;

[CreateAssetMenu(menuName = "Item Actions/Heavy Attack Action")]
public class HeavyAttackAction : ItemAction{
    public override void PerformAction(PlayerManager playerManager){
        float staminaCost = playerManager.playerCombat.GetStaminaCost();

        if(playerManager.playerStats.currentStamina < staminaCost || playerManager.isInteracting) return;       

        HandleHeavyAttack(playerManager);

        playerManager.playerCombat.currentAttackType = AttackType.Heavy;
        playerManager.playerCombat.DrainStaminaBasedOnAttack(staminaCost);
    }

    void HandleHeavyAttack(PlayerManager playerManager){     
        if(playerManager.isUsingLeftHand){
            playerManager.playerAnimator.PlayAnimation(playerManager.playerCombat.oh_heavyAttack, true, false, true);
            
            // FX
            playerManager.playerEffects.PlayWeaponFX(true);
            playerManager.characterSound.PlaySound("attack");
        }else if(playerManager.isUsingRightHand){
            playerManager.playerAnimator.PlayAnimation(playerManager.playerCombat.oh_heavyAttack, true);
            
            // FX
            playerManager.playerEffects.PlayWeaponFX(false);
            playerManager.characterSound.PlaySound("attack");
        }
    }

}
