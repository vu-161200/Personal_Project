using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class RewardItem
{
    public Item item;
    public int stackSize = 1;
    public float rate = 100;
}

public class EnemyStats : CharacterStats
{
    EnemyAnimator enemyAnimator;
    EnemyManager enemyManager;
    BossManager bossManager;

    [Header("Settings")]
    public bool isBoss;
    public string enemyName = "";

    [Header("UI")]
    public Slider healthSlider;
    public TMP_Text enemyNameText;

    [Header("Drop Item")]
    public GameObject dropModel;
    public Transform dropPos;

    [Header("Reward")]
    public int coinBonus = 10;
    public int expBonus = 100;
    public RewardItem[] listDropItems;

    void Awake(){
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyManager = GetComponent<EnemyManager>();
        bossManager = GetComponent<BossManager>();
    }

    // Start is called before the first frame update
    void Start(){
        teamID = 0;
        
        maxHealth = GetHealthByLevel();
        currentHealth = maxHealth;

        if(!isBoss){
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }else{
            healthSlider.gameObject.SetActive(false);
        }

        if(enemyName != ""){
            enemyNameText.gameObject.SetActive(true);
            enemyNameText.text = enemyName;
        }else{
            enemyNameText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update(){
        if(!isBoss){
            if(currentHealth == maxHealth || currentHealth == 0f){
                healthSlider.gameObject.SetActive(false);
            }else{
                healthSlider.gameObject.SetActive(true);
            }
        }
    }

    public float GetHealthByLevel(){
        maxHealth = level * healthByLevel * 10;
        return maxHealth;
    }

    public override void TakeDame(float damage){
        if(isDead) return;

        currentHealth -= damage;
        
        // Animation Get Dame
        if(!isBoss || (isBoss && !bossManager.bossCombatStanceState.hasPhaseShifted)) enemyAnimator.PlayAnimation("Hit", true);

        // Death
        if(currentHealth <= 0){
            HandleDeath();
        }

        if(isBoss && bossManager != null){
            bossManager.UpdateHealth(currentHealth, maxHealth);
        }else{
            healthSlider.value = currentHealth;
        }
    }

    void HandleDeath(){
        isDead = true;
        currentHealth = 0;

        GetComponent<CapsuleCollider>().enabled = false;

        // 
        EventManager.EnemyDied(this);

        // Animation Death
        enemyAnimator.PlayAnimation("Fall1", true);

        if(isBoss && bossManager != null){
            bossManager.BossHasBeenDefeated();
        }

        // Tiền + Điểm kinh nghiệm
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if(playerStats != null){
            playerStats.AddCoins(coinBonus);
            playerStats.AddEXP(expBonus);
        }

        StartCoroutine(DestroyEnemy());
    }

    IEnumerator DestroyEnemy(){
        enemyManager.HandleCharacterDead();
        
        yield return new WaitForSeconds(1.5f);

        // Rơi vật phẩm
        if(listDropItems.Length > 0){
            for(int i = 0; i < listDropItems.Length; i++){
                if(Random.Range(0, 100) <= listDropItems[i].rate){
                    PickUp pickUp = Instantiate(dropModel, dropPos).GetComponent<PickUp>();

                    pickUp.transform.position = dropPos.position;
                    pickUp.transform.SetParent(null);

                    pickUp.item = listDropItems[i].item;
                    pickUp.stackSize = listDropItems[i].stackSize;
                    pickUp.SetSerialize();
                }
            }
        }

        Destroy(gameObject);
    }

}
