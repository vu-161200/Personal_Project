
using UnityEngine;

public class AttackState : State
{
    public PursueTargetState pursueTargetState;
    public RotateTowardsTargetState rotateTowardsTargetState;
    
    [Header("Attack")]
    public EnemyAttackData currentAttack;

    public bool willComboNextAttack = false;
    public bool hasPerformedAttack = false;

    private void Awake() {
        pursueTargetState = GetComponent<PursueTargetState>();
        rotateTowardsTargetState = GetComponent<RotateTowardsTargetState>();
    }

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator){
        if(enemyManager.distanceFromTarget > currentAttack.maxDistanceToAttack){
            return pursueTargetState;
        }

        // RotateTowardsTargetWhilstAttacking(enemyManager);

        if(willComboNextAttack && enemyManager.canCombo){
            // Thực hiện combo
            PerformAttackWithCombo(enemyAnimator, enemyManager);
        }

        if(!hasPerformedAttack){
            // Thực hiện tấn công
            PerformAttack(enemyAnimator, enemyManager);

            // Random tỉ lệ thực hiện combo
            RollForComboChange(enemyManager);
        }

        if(willComboNextAttack && hasPerformedAttack){
            return this; // Quay về thực hiện attack
        }

        return rotateTowardsTargetState;
    }

    void PerformAttack(EnemyAnimator enemyAnimator, EnemyManager enemyManager){
        enemyAnimator.PlayAnimation(currentAttack.actionAnimation, true);
        RotateTowardsTargetWhilstAttacking(enemyManager);
        enemyAnimator.PlayWeaponTrailFX();
        
        // Đặt Cooldown
        enemyManager.currentCooldownTime = currentAttack.cooldown;
        
        hasPerformedAttack = true;
    }

    void PerformAttackWithCombo(EnemyAnimator enemyAnimator, EnemyManager enemyManager){
        willComboNextAttack = false;
        enemyAnimator.PlayAnimation(currentAttack.comboAction.actionAnimation, true);
        RotateTowardsTargetWhilstAttacking(enemyManager);
        enemyAnimator.PlayWeaponTrailFX(enemyManager.enemyStats.isBoss ? true : false);

        // Đặt Cooldown
        enemyManager.currentCooldownTime = currentAttack.cooldown;

        currentAttack = null;
        hasPerformedAttack = true;
    }

    void RotateTowardsTargetWhilstAttacking(EnemyManager enemyManager){
        if(enemyManager.isInteracting){
            Vector3 direction = enemyManager.target.transform.position - enemyManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero){
                direction = enemyManager.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
        
    }

    void RollForComboChange(EnemyManager enemyManager){
        float comboChance = Random.Range(0, 100);

        if(enemyManager.allowPerformCombos && comboChance <= enemyManager.comboLikelyHood){
            if(currentAttack.comboAction != null){
                willComboNextAttack = true;
            }else{
                willComboNextAttack = false;
                currentAttack = null;
            }
        }
    }
}
