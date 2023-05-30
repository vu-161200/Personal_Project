using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Settings")]
    public bool isActive;
    public bool isAwakened;
    public bool isDefeated;

    [Header("Refs")]
    public BossUIManager bossUIManager;
    public EnemyAnimator enemyAnimator;
    public EnemyManager enemyManager;
    public EnemyStats enemyStats;
    public CharacterStats playerStats;
    public CharacterSound characterSound;
    public BossCombatStanceState bossCombatStanceState;
    public BossFightCollider bossFightCollider;

    protected virtual void Awake() {    
        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();

        enemyAnimator = GetComponent<EnemyAnimator>();
        characterSound = GetComponent<CharacterSound>();
        enemyManager = GetComponent<EnemyManager>();
        enemyStats = GetComponent<EnemyStats>();

        enemyStats.isBoss = true;
    }

    protected virtual void BossFight(BossFightCollider _collider, CharacterStats _playerStats){
        if(!isActive){
            playerStats = _playerStats;
            bossFightCollider = _collider;
            bossUIManager = playerStats.GetComponentInChildren<BossUIManager>();
            bossUIManager.InitializeUI(enemyStats.enemyName, enemyStats.GetHealthByLevel());

            ActivateBossFight();
        }
    }

    protected virtual void ActivateBossFight(){
        isActive = true;
        bossUIManager.ActivateUI();
        
        enemyStats.enemyNameText.gameObject.SetActive(false);
        enemyManager.target = playerStats;
    }

    public virtual void BossHasBeenDefeated(){
        bossUIManager.DeactivateUI();

        // Hủy sự kiện
        EventManager.OnBossFight -= BossFight;
    }

    public virtual void UpdateHealth(float currentHealth, float maxHealth){
        bossUIManager.UpdateHealth(currentHealth);

        if(currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted){
            ActivateSecondPhase();
        }
    }

    protected virtual void ActivateSecondPhase(){
        // Animation 
        enemyAnimator.PlayAnimation("Boost", true);
        characterSound.PlaySound("atkBuff");

        bossCombatStanceState.hasPhaseShifted = true;
    }
}
