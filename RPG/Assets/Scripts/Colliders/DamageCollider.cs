using UnityEngine;
using static Enums;

// Collider của vũ khí (gây sát thương)
public class DamageCollider : MonoBehaviour
{
    public int teamID = 0;
    public float currentWeaponDamage = 25;
    public bool enableColliderOnStartUp = false;

    public CharacterManager characterManager;

    Collider damageCollider;
    protected bool parried = false;
    protected bool blocked = false;

    void Awake(){
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = enableColliderOnStartUp;
    }

    public void EnableCollider(){
        damageCollider.enabled = true;
    }

    public void DisableCollider(){
        damageCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other){
        // Player bị tấn công
        if(other.tag == "Player"){
            parried = false;
            blocked = false;

            PlayerManager playerManager = other.GetComponent<PlayerManager>();

            if(playerManager != null){
                if(playerManager.isInvulnerable) return;

                // Parrying
                CheckParry(playerManager);
                
                // Blocking
                CheckBlock(playerManager);
            }

            if(playerManager.playerStats != null){
                if(playerManager.playerStats.teamID == teamID || parried || blocked) return;

                // Lấy vị trí của điểm tiếp xúc (va chạm)
                Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                // Chạy hiệu ứng mất máu
                playerManager.playerEffects.PlayBloodSplatterFX(contactPoint);

                // Sát thương dựa theo loại tấn công
                playerManager.playerStats.TakeDame(currentWeaponDamage + (characterManager as EnemyManager).enemyStats.GetCurrentDamage());
                // Tắt luôn collider khi đã gây sát thương (fix trường hợp khi đánh gây nhiêu lần sát thương)
                DisableCollider();

                // Trường hợp player chết ==> target của quái vật = null
                if(playerManager.playerStats.isDead){
                    EnemyManager enemyManager = gameObject.GetComponentInParent<EnemyManager>();
                    if(enemyManager != null) enemyManager.HandleCharacterDead();
                }
            }
        }
        // Quái vật bị tấn công
        if(other.tag == "Enemy"){
            EnemyManager enemyManager = other.GetComponent<EnemyManager>();
            
            if(enemyManager != null){
                // Đang trong giai đoạn bất tử ==> không nhận sát thương
                if(enemyManager.isInvulnerable) return;
                // Cùng teamID thì không nhận sát thương
                if(enemyManager.enemyStats.teamID == teamID) return;
       
                // Lấy vị trí va chạm
                Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                // Chạy hiệu ứng mất máu
                enemyManager.enemyEffects.PlayBloodSplatterFX(contactPoint);
              
                // Tính tổng lượng sát thương gây ra cho quái vật
                DealDamage(characterManager as PlayerManager, enemyManager.enemyStats);
                // Tắt luôn collider khi đã gây sát thương (fix trường hợp khi đánh gây nhiêu lần sát thương)
                DisableCollider();

                // Trường hợp quái vật chưa tìm được mục tiêu (trạng thái rảnh hoặc ngủ, ngồi ...), nếu bị tấn công thì gán mục tiêu luôn là người tấn công
                if(enemyManager.target == null){
                    enemyManager.target = gameObject.GetComponentInParent<CharacterStats>();
                }
            }
        }
    }

    // Kiểm tra xem có đang parry không
    protected virtual void CheckParry(PlayerManager playerManager){
        if(playerManager.isParrying){
            (characterManager as EnemyManager).enemyWeaponManager.DisableWeaponDamage();
            // Quái bị choáng + Không nhận sát thương
            characterManager.GetComponent<AnimatorManager>().PlayAnimation("Stun", true);

            // Gán parried để bỏ qua phần dưới (nhận dame + hiệu ứng máu)
            parried = true;
        }
    }

    // Kiểm tra player có đang thực hiện block không?
    protected virtual void CheckBlock(PlayerManager playerManager){
        // Vector vị trí từ player tới quái vật tấn công mình
        Vector3 directionFromPlayerToEnemy = characterManager.transform.position - playerManager.transform.position;
        // Tính giá độ lệch giữa giữa Vector 'directionFromPlayerToEnemy' và hướng của player
        float dotValueFromPlayerToEnemy = Vector3.Dot(directionFromPlayerToEnemy, playerManager.transform.forward);
        
        if(playerManager.isBlocking && dotValueFromPlayerToEnemy > 0.3f){
            blocked = true;

            // Tính toán lượng dame sau khi block
            float damageAfterBlock = currentWeaponDamage - (currentWeaponDamage * playerManager.playerStats.blockingDamageAbsorption / 100);

            // Kiểm tra điều kiện và chạy animation (Chặn thành công / Khiên vỡ) tương ứng
            playerManager.playerCombat.AttemptBlock(this, damageAfterBlock);
            DisableCollider();

            // Nếu chết ==> reset target của quái vật
            if(playerManager.playerStats.isDead){
                EnemyManager enemyManager = gameObject.GetComponentInParent<EnemyManager>();
                if(enemyManager != null) enemyManager.HandleCharacterDead();
            }
        }
    }

    // Tính toán lượng sát thương gây ra dựa theo loại tấn công và hệ số sát thương của loại tấn công đó
    protected virtual void DealDamage(PlayerManager playerManager, EnemyStats enemyStats){
        float finalDamage = currentWeaponDamage + playerManager.playerStats.GetCurrentDamage();

        // Check tay nào đang thực hiện đánh
        if(playerManager.isUsingRightHand){
            if(playerManager.playerCombat.currentAttackType == AttackType.Light){
                finalDamage *= playerManager.playerEquipment.rightWeapon.lightAttackDamageModifier;
            }else if(playerManager.playerCombat.currentAttackType == AttackType.Heavy){
                finalDamage *= playerManager.playerEquipment.rightWeapon.heavyAttackDamageModifier;
            }
        }else if(playerManager.isUsingLeftHand){
            if(playerManager.playerCombat.currentAttackType == AttackType.Light){
                finalDamage *= playerManager.playerEquipment.leftWeapon.lightAttackDamageModifier;
            }else if(playerManager.playerCombat.currentAttackType == AttackType.Heavy){
                finalDamage *= playerManager.playerEquipment.leftWeapon.heavyAttackDamageModifier;
            }
        }

        // Quái nhận sát thương
        enemyStats.TakeDame(finalDamage);
    }
}