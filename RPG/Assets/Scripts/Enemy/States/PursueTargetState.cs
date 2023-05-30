using UnityEngine;

public class PursueTargetState : State
{
    public CombatStanceState combatStanceState;
    
    private void Awake() {
        combatStanceState = GetComponent<CombatStanceState>();
    }

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator){
        // Hướng tới mục tiêu (Nhìn vào mục tiêu)
        HandleRotateTowardsTarget(enemyManager);

        if(enemyManager.isInteracting) return this;

        // Nếu đang hành động => return
        if(enemyManager.isPerformingAction){
            enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }

        if(enemyManager.distanceFromTarget > enemyManager.maxAttackRange){
            enemyAnimator.anim.SetFloat("Vertical", 0.5f, 0.1f, Time.deltaTime);
        } 

        // Nếu nằm đủ tầm đánh => Chuyển sang trạng thái "Combat Stance"
        // Nếu ngoài tầm đánh, return và đuổi theo mục tiêu
        if(enemyManager.distanceFromTarget <= enemyManager.maxAttackRange) return combatStanceState;
        else return this;
    }

    void HandleRotateTowardsTarget(EnemyManager enemyManager){
        // Trường hợp bình thường
        if(enemyManager.isPerformingAction){
            Vector3 direction = enemyManager.target.transform.position - enemyManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero){
                direction = enemyManager.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
        // Trường hợp tìm đường di chuyển tới mục tiêu với Nav Mesh Agent
        else{
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.nav.desiredVelocity);
            Vector3 targetVelocity = enemyManager.rig.velocity;

            enemyManager.nav.enabled = true;
            enemyManager.nav.SetDestination(enemyManager.target.transform.position);
            enemyManager.rig.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.nav.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }

}
