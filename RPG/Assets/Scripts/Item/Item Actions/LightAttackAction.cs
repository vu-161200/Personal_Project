using UnityEngine;
using static Enums;

[CreateAssetMenu(menuName = "Item Actions/Light Attack Action")]
public class LightAttackAction : ItemAction
{
    public override void PerformAction(PlayerManager playerManager){
        float staminaCost = playerManager.playerCombat.GetStaminaCost();
        if(playerManager.playerStats.currentStamina < staminaCost) return;        

        if(playerManager.canCombo){
            playerManager.playerInput.comboFlag = true;
            HandleLightAttackCombo(playerManager);
            playerManager.playerInput.comboFlag = false;
        }else{
            if(playerManager.isInteracting || playerManager.canCombo) return;

            HandleLightAttack(playerManager);
        }

        playerManager.playerCombat.currentAttackType = AttackType.Light;
        playerManager.playerCombat.DrainStaminaBasedOnAttack(staminaCost);
    }

    
    void HandleLightAttackCombo(PlayerManager playerManager){
        if(!playerManager.playerInput.comboFlag || playerManager.playerCombat.oh_lightAttack2 == "") return;

        playerManager.playerAnimator.anim.SetBool("canCombo", false);

        if(playerManager.isUsingLeftHand){
            if(playerManager.playerCombat.lastAttack == playerManager.playerCombat.oh_lightAttack1){
                playerManager.playerAnimator.PlayAnimation(playerManager.playerCombat.oh_lightAttack2, true, false, true);
                playerManager.playerCombat.lastAttack = playerManager.playerCombat.oh_lightAttack2;
                
                // FX
                playerManager.characterSound.PlaySound("attack");
                playerManager.playerEffects.PlayWeaponFX(true);
            }
        }else{
            if(playerManager.playerCombat.lastAttack == playerManager.playerCombat.oh_lightAttack1){
                playerManager.playerAnimator.PlayAnimation(playerManager.playerCombat.oh_lightAttack2, true);
                playerManager.playerCombat.lastAttack = playerManager.playerCombat.oh_lightAttack2;

                // FX
                playerManager.playerEffects.PlayWeaponFX(false);
                playerManager.characterSound.PlaySound("attack");
            }
        }
    }

    void HandleLightAttack(PlayerManager playerManager){
        if(playerManager.isUsingLeftHand){
            playerManager.playerAnimator.PlayAnimation(playerManager.playerCombat.oh_lightAttack1, true, false, true);
            playerManager.playerCombat.lastAttack = playerManager.playerCombat.oh_lightAttack1;

            // FX
            playerManager.playerEffects.PlayWeaponFX(true);
            playerManager.characterSound.PlaySound("attack");
        }else if(playerManager.isUsingRightHand){
            playerManager.playerAnimator.PlayAnimation(playerManager.playerCombat.oh_lightAttack1, true);
            playerManager.playerCombat.lastAttack = playerManager.playerCombat.oh_lightAttack1;

            // FX
            playerManager.playerEffects.PlayWeaponFX(false);
            playerManager.characterSound.PlaySound("attack");
        }
    }

}
