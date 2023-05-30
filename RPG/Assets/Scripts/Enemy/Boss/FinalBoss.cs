using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : BossManager
{
    [Header("FX")]
    public GameObject leftNormalEffect;
    public GameObject leftSecondPhaseEffect;
    public GameObject rightNormalEffect;
    public GameObject rightSecondPhaseEffect;
    

    protected override void Awake() {    
        base.Awake();

        leftNormalEffect.SetActive(true);
        leftSecondPhaseEffect.SetActive(false);
        rightNormalEffect.SetActive(true);
        rightSecondPhaseEffect.SetActive(false);

        // Gán sự kiện
        EventManager.OnBossFight += BossFight;
    }

    private void LateUpdate() {
        if(playerStats != null && playerStats.isDead){
            enemyStats.currentHealth = enemyStats.maxHealth;
            UpdateHealth(enemyStats.currentHealth, enemyStats.maxHealth);
            bossUIManager.DeactivateUI();
            
            isActive = false;
            leftNormalEffect.SetActive(true);
            leftSecondPhaseEffect.SetActive(false);
            rightNormalEffect.SetActive(true);
            rightSecondPhaseEffect.SetActive(false);

            // Mở cửa lại
            foreach (var wall in bossFightCollider.walls){
                wall.SetActive(false);
            }
        }
    }

    protected override void BossFight(BossFightCollider _collider, CharacterStats _playerStats){
        base.BossFight(_collider, _playerStats);
    }

    protected override void ActivateBossFight(){
        base.ActivateBossFight();

        // Đóng cửa lại
        foreach (var wall in bossFightCollider.walls){
            wall.SetActive(true);
        }
    }

    public override void BossHasBeenDefeated(){
        base.BossHasBeenDefeated();

        // Mở cửa lại
        foreach (var wall in bossFightCollider.walls){
            wall.SetActive(false);
        }
    }

    public override void UpdateHealth(float currentHealth, float maxHealth){
        base.UpdateHealth(currentHealth, maxHealth);
    }

    protected override void ActivateSecondPhase(){
        base.ActivateSecondPhase();

        if(enemyManager.enemyWeaponManager.leftWeapon != null){
            leftNormalEffect.SetActive(false);
            leftSecondPhaseEffect.SetActive(true);

            enemyManager.enemyWeaponManager.leftHandDamage.currentWeaponDamage *= 2;
        }

        if(enemyManager.enemyWeaponManager.rightWeapon != null){
            rightNormalEffect.SetActive(false);
            rightSecondPhaseEffect.SetActive(true);

            enemyManager.enemyWeaponManager.rightHandDamage.currentWeaponDamage *= 2;
        }
    }
}
