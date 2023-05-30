using UnityEngine;

public class IdleState : State
{
    public LayerMask detectionLayer;
    public LayerMask layersBlockSight;

    public PursueTargetState pursueTargetState;

    private void Awake() {
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator){
        // Tìm kiếm mục tiêu bên trong bán kính phát hiện 
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.radius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++){
            CharacterStats character = colliders[i].transform.GetComponent<CharacterStats>();

            // Phát hiện mục tiêu ==> Kiểm tra teamID của mục tiêu
            if(character != null && character.teamID != enemyStats.teamID){
                Vector3 direction = character.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(direction, transform.forward);

                // Mục tiêu phải đứng trước mặt của Enemy và thỏa mãn góc nhìn
                if(viewableAngle > enemyManager.minAngle && viewableAngle < enemyManager.maxAngle){
                    // Không có vật chắn ở giữa enemy với mục tiêu
                    if(Physics.Linecast(enemyManager.transform.position, character.transform.position, layersBlockSight)){
                        return this;
                    }
                    else enemyManager.target = character;
                }
            }
        }

        // Chuyển sang trạng thái "Pursue Target" nếu tìm thấy
        if(enemyManager.target != null) return pursueTargetState;
        else return this;
    }
}
