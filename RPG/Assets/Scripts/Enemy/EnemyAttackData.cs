using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttack", menuName = "Enemy/Ations/Attack Action")]
public class EnemyAttackData : EnemyAction
{
    public bool canCombo;

    public EnemyAttackData comboAction;

    public int attackScore = 3; // Càng cao thì càng nhiều khả năng xảy ra (vs trường hợp Enemy có nhiều loại tấn công)
    public float cooldown = 2;

    public float minAttackAngle = -60;
    public float maxAttackAngle = 60;

    public float minDistanceToAttack = 0;
    public float maxDistanceToAttack = 2;
}
