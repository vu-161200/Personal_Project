using UnityEngine;

public class CombatStanceState : State
{
    public EnemyAttackData[] enemyAttacks;
    public AttackState attackState;
    public PursueTargetState pursueTargetState;

    protected bool randomDestinationSet = false;
    protected float verticalMovementValue = 0;
    protected float horizontalMovementValue = 0;

    private void Awake() {
        attackState = GetComponent<AttackState>();
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator){
        // Kiểm tra tầm đánh
        enemyAnimator.anim.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
        enemyAnimator.anim.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
        attackState.hasPerformedAttack = false;
        attackState.willComboNextAttack = false;

        if(enemyManager.isInteracting){
            enemyAnimator.anim.SetFloat("Vertical", 0);
            enemyAnimator.anim.SetFloat("Horizontal", 0);
            return this;
        };

        if(enemyManager.distanceFromTarget > enemyManager.maxAttackRange) return pursueTargetState;

        if(!randomDestinationSet){
            randomDestinationSet = true;

            // Đi xung quanh mục tiêu
            WalkAroundTarget(enemyAnimator);
        }

        // Nhìn vào mục tiêu
        HandleRotateTowardsTarget(enemyManager);
        
        // Nếu trong tầm đánh => Chuyển sang trạng thái "Attack"
        // Nếu mục tiêu ngoài tầm => Chuyển sang trạng thái "Purse Target"
        // Nếu trong cooldown time => return và đi xung quanh mục tiêu
        if(enemyManager.currentCooldownTime <= 0 && attackState.currentAttack != null) 
        {
            randomDestinationSet = false;
            return attackState;
        }
        else
        {
            GetAttack(enemyManager);
        } 

        return this;
    }

    protected void WalkAroundTarget(EnemyAnimator enemyAnimator){
        verticalMovementValue = 0.5f; // Chỉ chuyển động về phía trước ==> Luôn đi hướng về mục tiêu

        horizontalMovementValue = Random.Range(-1, 1);

        if(horizontalMovementValue <= 1 && horizontalMovementValue >= 0){
            horizontalMovementValue = 0.5f;
        }else if(horizontalMovementValue >= -1 && horizontalMovementValue < 0){
            horizontalMovementValue = -0.5f;
        }
    }

    protected virtual void GetAttack(EnemyManager enemyManager){
        // Random loại tấn công nếu có nhiều hơn 2 loại
        int maxScore = 0;
        for (int i = 0; i < enemyAttacks.Length; i++){
            EnemyAttackData data = enemyAttacks[i];

            if(enemyManager.distanceFromTarget <= data.maxDistanceToAttack && enemyManager.distanceFromTarget >= data.minDistanceToAttack){
                if(enemyManager.viewableAngle <= data.maxAttackAngle && enemyManager.viewableAngle >= data.minAttackAngle){
                    maxScore += data.attackScore;
                }
            }
        }

        int rand = Random.Range(0, maxScore);
        int temporaryScore = 0;
        for (int i = 0; i < enemyAttacks.Length; i++){
            EnemyAttackData data = enemyAttacks[i];

            if(enemyManager.distanceFromTarget <= data.maxDistanceToAttack && enemyManager.distanceFromTarget >= data.minDistanceToAttack){
                if(enemyManager.viewableAngle <= data.maxAttackAngle && enemyManager.viewableAngle >= data.minAttackAngle){
                    if(attackState.currentAttack != null) return;

                    temporaryScore += data.attackScore;
                    if(temporaryScore > rand){
                        attackState.currentAttack = data;
                    }
                }
            }
        }

    }

    protected void HandleRotateTowardsTarget(EnemyManager enemyManager){
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
