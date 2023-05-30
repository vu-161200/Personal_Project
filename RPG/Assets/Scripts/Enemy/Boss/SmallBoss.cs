public class SmallBoss : BossManager
{
    protected override void Awake() {    
        base.Awake();

        // Gán sự kiện
        EventManager.OnSmallBossFight += BossFight;
    }

    private void LateUpdate() {
        if(playerStats != null && playerStats.isDead){
            enemyStats.currentHealth = enemyStats.maxHealth;
            UpdateHealth(enemyStats.currentHealth, enemyStats.maxHealth);
            bossUIManager.DeactivateUI();
            
            isActive = false;

            // Có thể mở cửa
            bossFightCollider.door.UnlockDoor();
        }
    }

    protected override void BossFight(BossFightCollider _collider, CharacterStats _playerStats){
        base.BossFight(_collider, _playerStats);
    }

    protected override void ActivateBossFight(){
        base.ActivateBossFight();

        // Khóa cửa
        bossFightCollider.door.LockDoor();

    }

    public override void BossHasBeenDefeated(){
        base.BossHasBeenDefeated();

        // Mở cửa
        bossFightCollider.door.UnlockDoor();
    }

    public override void UpdateHealth(float currentHealth, float maxHealth){
        base.UpdateHealth(currentHealth, maxHealth);
    }

    protected override void ActivateSecondPhase(){
        base.ActivateSecondPhase();

        if(enemyManager.enemyWeaponManager.leftWeapon != null){
            enemyManager.enemyWeaponManager.leftHandDamage.currentWeaponDamage *= 2;
        }

        if(enemyManager.enemyWeaponManager.rightWeapon != null){
            enemyManager.enemyWeaponManager.rightHandDamage.currentWeaponDamage *= 2;
        }
    }
}
