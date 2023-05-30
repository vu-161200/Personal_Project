using UnityEngine;

// Hành động block (giơ khiên lên để chặn damage)
[CreateAssetMenu(menuName = "Item Actions/Blocking Action")]
public class BlockingAction : ItemAction{
    public override void PerformAction(PlayerManager playerManager){
        if(playerManager.isInteracting || playerManager.isBlocking || playerManager.playerStats.currentStamina <= 0) return;

        // Khởi tạo thông tin của weapon
        playerManager.playerCombat.SetBlockingAbsorptionFromWeapon();

        playerManager.isBlocking = true;
    }
}
