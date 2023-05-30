using UnityEngine;

// Hành động kéo cung lên khi ngắm
[CreateAssetMenu(menuName = "Item Actions/Draw Arrow Action")]
public class DrawArrowAction : ItemAction
{
    public override void PerformAction(PlayerManager playerManager){
        if(playerManager.isInteracting || playerManager.isHoldingArrow || playerManager.playerEquipment.arrowStack <= 0) return;

        // Set aim
        Aim(playerManager);

        // Animation của player ==> Đổi animation di chuyển
        playerManager.playerAnimator.anim.SetBool("isHoldingArrow", true);
        playerManager.playerAnimator.PlayAnimation("Get Arrow", true);

        // Tạo arrow
        GameObject loadedArrow = Instantiate(playerManager.playerEquipment.arrow.loadedItemModel, playerManager.weaponSlotManager.rightHand.transform);
        playerManager.playerEffects.currentRangeFX = loadedArrow;

        // Bow Animation: draw -> aim
        Animator bowAnim = playerManager.weaponSlotManager.leftHand.GetComponentInChildren<Animator>();
        bowAnim.SetBool("isDrawn", true);
        bowAnim.Play("Draw");
    }

    void Aim(PlayerManager playerManager){
        if(playerManager.isAiming) return;
        
        playerManager.isAiming = true;
    }
}
