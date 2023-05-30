using UnityEngine;

public class PlayerAnimator : AnimatorManager
{
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PlayerManager playerManager;

    int vertical;
    int horizontal;

    public void Initialize(){
        anim = GetComponent<Animator>();

        playerInput = GetComponent<PlayerInput>();
        playerManager = GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimator(float verticalMovement, float horizontalMovement, bool isSprinting){
        #region Vertical
        float v = 0;

        if(verticalMovement > 0 && verticalMovement < 0.55f) v = 0.5f;
        else if(verticalMovement > 0.55f) v = 1;
        else if(verticalMovement < 0 && verticalMovement > -0.55f) v = -0.5f;
        else if(verticalMovement < -0.55f) v = -1;
        else v = 0;
        #endregion

        #region Horizontal
        float h = 0;

        if(horizontalMovement > 0 && horizontalMovement < 0.55f) h = 0.5f;
        else if(horizontalMovement > 0.55f) h = 1;
        else if(horizontalMovement < 0 && horizontalMovement > -0.55f) h = -0.5f;
        else if(horizontalMovement < -0.55f) h = -1;
        else h = 0;
        #endregion

        if(isSprinting){
            v = 2;
            h = horizontalMovement;
        }

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void CanRotate(){
        anim.SetBool("canRotate", true);
    }

    public void StopRotate(){
        anim.SetBool("canRotate", false);
    }

    public void EnableInvulnerable(){
        anim.SetBool("isInvulnerable", true);
    }

    public void DisableInvulnerable(){
        anim.SetBool("isInvulnerable", false);
    }

    public void EnableParry(){
        anim.SetBool("isParrying", true);
    }

    public void DisableParry(){
        anim.SetBool("isParrying", false);
    }

    public void CanCombo(){
        anim.SetBool("canCombo", true);
    }

    public void StopCombo(){
        anim.SetBool("canCombo", false);
    }

    private void OnAnimatorMove(){
        if(playerManager.isInteracting == false || playerInput.pauseFlag || playerInput.inventoryFlag || playerInput.shopFlag || playerInput.chestFlag) return;

        playerMovement.rig.drag = 0;
        
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;

        Vector3 velocity = deltaPosition / Time.deltaTime;
        if (!float.IsNaN(velocity.x) && !float.IsNaN(velocity.y) && !float.IsNaN(velocity.z)){
            playerMovement.rig.velocity = velocity; 
        }
    }


}
