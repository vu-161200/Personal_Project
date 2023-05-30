using UnityEngine;

// Collider của mũi tên (gây sát thương)
public class RangedProjectileDamageCollider : DamageCollider
{
    public ArrowData arrow;
    protected bool hasAlreadyPenetratedASurface;
    protected GameObject penetratedProjectile;

    protected override void OnTriggerEnter(Collider other){
      
        // Mũi tên trúng player (tương tự damage collider)
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

                Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                playerManager.playerEffects.PlayBloodSplatterFX(contactPoint);

                playerManager.playerStats.TakeDame(currentWeaponDamage);

                if(playerManager.playerStats.isDead){
                    (characterManager as EnemyManager).HandleCharacterDead();
                }
            }
        }

        // Mũi tên trúng quái vật (tương tự damage collider)
        if(other.tag == "Enemy"){
            EnemyManager enemyManager = other.GetComponent<EnemyManager>();

            if(enemyManager != null){
                if(enemyManager.isInvulnerable) return;
                // Cùng teamID thì không nhận sát thương
                if(enemyManager.enemyStats.teamID == teamID) return;

                Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                enemyManager.enemyEffects.PlayBloodSplatterFX(contactPoint);

                enemyManager.enemyStats.TakeDame(currentWeaponDamage + (characterManager as PlayerManager).playerStats.GetCurrentDamage());

                if(enemyManager.target == null){
                    enemyManager.target = characterManager.GetComponent<CharacterStats>();
                }
            }
        }
    
        // Mũi tên gắn lên target
        if(!hasAlreadyPenetratedASurface && penetratedProjectile == null){
            hasAlreadyPenetratedASurface = true;
        
            // Lấy vị trí va chạm
            Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            // Khởi tạo bản sao của mũi tên ở vị trí đó với góc quay 0,0,0 (Quaternion.Euler)
            GameObject penetratedArrow = Instantiate(arrow.penetratedItemModel, contactPoint, Quaternion.Euler(0, 0, 0));
        
            // Gán thông tin
            penetratedProjectile = penetratedArrow;
            penetratedArrow.transform.parent = other.transform;
            penetratedArrow.transform.rotation = Quaternion.LookRotation(gameObject.transform.forward);
        }

        // Hủy mũi tên đang bay đi 
        Destroy(transform.root.gameObject);
    }
}
