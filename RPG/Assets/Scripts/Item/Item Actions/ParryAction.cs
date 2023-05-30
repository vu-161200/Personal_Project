using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Parry Action")]
public class ParryAction : ItemAction
{
    public override void PerformAction(PlayerManager playerManager){
        if(playerManager.isInteracting) return;

        playerManager.playerAnimator.PlayAnimation("Parry", true);
    }
}
