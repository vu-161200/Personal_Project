using UnityEngine;

// Reset tay của nhân vật đang sử dụng mỗi khi animation chạy
public class ResetHandUsing : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
       PlayerManager playerManager = animator.GetComponent<PlayerManager>();

       playerManager.isUsingLeftHand = false;
       playerManager.isUsingRightHand = false;
    }
}
