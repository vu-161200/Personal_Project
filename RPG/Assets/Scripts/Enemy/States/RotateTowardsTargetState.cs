public class RotateTowardsTargetState : State
{
    public CombatStanceState combatStanceState;

    private void Awake() {
        combatStanceState = GetComponent<CombatStanceState>();
    }

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
    {
        enemyAnimator.anim.SetFloat("Vertical", 0);
        enemyAnimator.anim.SetFloat("Horizontal", 0);

        // Khi chuyển trạng thái thì animation tấn công vẫn chạy <=> isInteracting = true ==> quay lại đến khi thực hiện xong 
        if(enemyManager.isInteracting){
            return this;
        }

        if(enemyManager.viewableAngle >= 100 && enemyManager.viewableAngle <= 180 && !enemyManager.isInteracting){
            enemyAnimator.PlayAnimationWithRootRotation("Turn Around", true);
            return combatStanceState;
        }else if(enemyManager.viewableAngle >= -180 && enemyManager.viewableAngle <= -101 && !enemyManager.isInteracting){
            enemyAnimator.PlayAnimationWithRootRotation("Turn Around", true);
            return combatStanceState;
        }else if(enemyManager.viewableAngle >= -100 && enemyManager.viewableAngle <= -45 && !enemyManager.isInteracting){
            enemyAnimator.PlayAnimationWithRootRotation("Turn Right", true);
            return combatStanceState;
        }else if(enemyManager.viewableAngle >= 45 && enemyManager.viewableAngle <= 100 && !enemyManager.isInteracting){
            enemyAnimator.PlayAnimationWithRootRotation("Turn Left", true);
            return combatStanceState;
        }

        return combatStanceState;
    }
}
