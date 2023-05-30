
using UnityEngine;

public class EnemyAnimator : AnimatorManager
{
    EnemyManager enemyManager;
    EnemyEffects enemyEffects;

    void Awake(){
        anim = GetComponent<Animator>();
        enemyManager = GetComponent<EnemyManager>();
        enemyEffects = GetComponent<EnemyEffects>();
    }

    public void PlayWeaponTrailFX(bool isLeft = false){
        enemyEffects.PlayWeaponFX(isLeft);
    }

    void OnAnimatorMove(){
        enemyManager.rig.drag = 0;

        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;

        Vector3 velocity = deltaPosition / Time.deltaTime;
        if (!float.IsNaN(velocity.x) && !float.IsNaN(velocity.y) && !float.IsNaN(velocity.z)){
            enemyManager.rig.velocity = velocity; 
        }

        if(enemyManager.isRotatingWithRootMotion){
            enemyManager.transform.rotation *= anim.deltaRotation;
        }
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

    public void CanCombo(){
        anim.SetBool("canCombo", true);
    }

    public void StopCombo(){
        anim.SetBool("canCombo", false);
    }

}
