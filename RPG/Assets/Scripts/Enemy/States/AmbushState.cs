using UnityEngine;

public class AmbushState : State
{
    public bool isRelaxing;
    public float detectionRadius = 2f;
    public string relaxAnimation = "Sleep";
    public string wakeAnimation = "Sleep End";
    public LayerMask dectectionLayer;
    public LayerMask layersBlockSight;

    public PursueTargetState pursueTargetState;

    private void Awake() {
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator){
        // Animation khởi đầu
        if(isRelaxing && enemyManager.isInteracting == false){
            enemyAnimator.PlayAnimation(relaxAnimation, true);
        }

        // Phát hiện mục tiêu
        if(enemyManager.target != null){
            isRelaxing = false;
            enemyAnimator.PlayAnimation(wakeAnimation, true);
            return pursueTargetState;
        }

        // Tìm kiếm mục tiêu bên trong bán kính phát hiện
        Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, dectectionLayer);

        for (int i = 0; i < colliders.Length; i++){
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

            // Phát hiện mục tiêu ==> Kiểm tra teamID của mục tiêu
            if(characterStats != null && characterStats.teamID != enemyStats.teamID){
                Vector3 targetDirection = characterStats.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                // Mục tiêu phải đứng trước mặt của Enemy và thỏa mãn góc nhìn
                if(viewableAngle > enemyManager.minAngle && viewableAngle < enemyManager.maxAngle){
                    // Không có vật chắn ở giữa enemy với mục tiêu
                    if(Physics.Linecast(enemyManager.transform.position, characterStats.transform.position, layersBlockSight)){
                        return this;
                    }
                    else{
                        enemyManager.target = characterStats;
                        isRelaxing = false;
                        enemyAnimator.PlayAnimation(wakeAnimation, true);
                    }
                }
            }
        }

        if(enemyManager.target != null) return pursueTargetState;
        else return this;
    }
}
