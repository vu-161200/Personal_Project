using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator anim;

    public bool canRotate;
    
    // Chạy aniamtion
    public void PlayAnimation(string targetAnimation, bool isInteracting, bool canRotate = false, bool isMirrored = false){
        anim.applyRootMotion = isInteracting;
        anim.SetBool("canRotate", canRotate);
        anim.SetBool("isInteracting", isInteracting);
        anim.SetBool("isMirrored", isMirrored);
        anim.CrossFade(targetAnimation, 0.1f);
    }

    public void PlayAnimationWithRootRotation(string targetAnimation, bool isInteracting){
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isRotatingWithRootMotion", true);
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnimation, 0.2f);
    }
}
