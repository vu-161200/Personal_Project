using UnityEngine;

public class BossCombatStanceState : CombatStanceState
{
    public EnemyAttackData[] secondPhaseAttacks;
    public bool hasPhaseShifted;

    protected override void GetAttack(EnemyManager enemyManager)
    {
        if(hasPhaseShifted){
            // Random loại tấn công nếu có nhiều hơn 2 loại
            int maxScore = 0;
            for (int i = 0; i < secondPhaseAttacks.Length; i++){
                EnemyAttackData data = secondPhaseAttacks[i];

                if(enemyManager.distanceFromTarget <= data.maxDistanceToAttack && enemyManager.distanceFromTarget >= data.minDistanceToAttack){
                    if(enemyManager.viewableAngle <= data.maxAttackAngle && enemyManager.viewableAngle >= data.minAttackAngle){
                        maxScore += data.attackScore;
                    }
                }
            }

            int rand = Random.Range(0, maxScore);
            int temporaryScore = 0;
            for (int i = 0; i < secondPhaseAttacks.Length; i++){
                EnemyAttackData data = secondPhaseAttacks[i];

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
        }else{
            base.GetAttack(enemyManager);
        }
    }
}
