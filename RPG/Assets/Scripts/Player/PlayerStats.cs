using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerStats : CharacterStats
{
    [Header("UI")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    public Slider expSlider;
    public TMP_Text levelText;

    public Slider staminaSlider;

    PlayerAnimator playerAnimator;
    public PlayerManager playerManager;
    DeadScreenManager deadScreenManager;
    InventoryManager inventoryManager;

    public float staminaRegenTimer = 0f;
    public float staminaRegenerationAmount = 1f;
    public float staminaRegenerationAmountWhilstBlocking = 0.1f;

    void Awake(){
        teamID = -1;
        ID = "PLAYER";

        playerAnimator = GetComponent<PlayerAnimator>();
        playerManager = GetComponent<PlayerManager>();
        inventoryManager = GetComponent<InventoryManager>();

        deadScreenManager = GetComponentInChildren<DeadScreenManager>();
    }

    // Start is called before the first frame update
    void Start(){
        maxStamina = GetStaminaByLevel(level);
        currentStamina = maxStamina;
        staminaSlider.maxValue = 1;
        staminaSlider.value = 1;
        
        maxHealth = GetHealthByLevel(level);
        currentHealth = maxHealth;
        healthSlider.maxValue = 1;
        healthSlider.value = 1;

        levelEXP = GetEXPByLevel(level);
        currentEXP = 0;
        expSlider.maxValue = 1;
        expSlider.value = currentEXP/levelEXP;
        levelText.text = level.ToString();
    }

    void Update(){
        RegenerateStamina();
    }

    float GetHealthByLevel(int currentLevel){
        maxHealth = healthByLevel * 10 * currentLevel + baseHP;
        return maxHealth;
    }

    float GetEXPByLevel(int currentLevel){
        levelEXP = expByLevel * 10 * currentLevel;
        return levelEXP;
    }

    float GetStaminaByLevel(int currentLevel){
        maxStamina = staminaByLevel * 10 * currentLevel + baseSTA;
        return maxStamina;
    }

    public override void TakeDame(float damage){
        if(isDead) return;

        float totalDamageAbsorption = 1 - (1 - damageAbsorptionArmor / 100) * (1 - damageAbsorptionHelmet / 100) * (1 - damageAbsorptionLegs / 100) * (1 - damageAbsorptionBoots / 100) * (1 - damageAbsorptionGloves / 100) * (1 - damageAbsorptionCape / 100) * (1 - baseDEF / 100);

        damage = Mathf.RoundToInt(damage - (damage * totalDamageAbsorption));

        currentHealth -= damage;
        
        // Hurt Animation
        playerManager.characterSound.PlaySound("hit");
        playerAnimator.PlayAnimation("Hit", true);

        // Death
        if(currentHealth <= 0){
            currentHealth = 0;
            
            // Death Animation
            playerAnimator.PlayAnimation("Death", true);
            
            isDead = true;

            StartCoroutine(HandleDead());
        }
        
        healthSlider.value = currentHealth/maxHealth;
        healthText.text = currentHealth.ToString("F0") + " / " + maxHealth.ToString("F0"); 
    }

    public override void TakeDameAfterBlock(float damage, string animation = "Block Guard"){
        if(isDead) return;

        float totalDamageAbsorption = 1 - (1 - damageAbsorptionArmor / 100) * (1 - damageAbsorptionHelmet / 100) * (1 - damageAbsorptionLegs / 100) * (1 - damageAbsorptionBoots / 100) * (1 - damageAbsorptionGloves / 100) * (1 - damageAbsorptionCape / 100) * (1 - baseDEF / 100);

        damage = Mathf.RoundToInt(damage - (damage * totalDamageAbsorption));

        currentHealth -= damage;

        // Block Animation
        playerAnimator.PlayAnimation(animation, true);
        playerManager.characterSound.PlaySound("block");

        // Death
        if(currentHealth <= 0){
            currentHealth = 0;
            
            // Death Animation
            playerAnimator.PlayAnimation("Death", true);
            
            isDead = true;

            StartCoroutine(HandleDead());
        }
        
        healthSlider.value = currentHealth/maxHealth;
        healthText.text = currentHealth.ToString("F0") + " / " + maxHealth.ToString("F0"); 
    }

    public void HealHP(float health){
        currentHealth += health;
        playerManager.characterSound.PlaySound("heal");
        if(currentHealth > maxHealth) currentHealth = maxHealth;
        healthSlider.value = currentHealth/maxHealth;
        healthText.text = currentHealth.ToString("F0") + " / " + maxHealth.ToString("F0"); 
    }

    public void AddCoins(int coins){
        coin += coins;
    }

    public void AddEXP(int amount){
        currentEXP += amount;

        while(currentEXP >= levelEXP){
            currentEXP -= levelEXP;
            level += 1;
            attributePoints++;
            levelText.text = level.ToString();
            levelEXP = GetEXPByLevel(level);
            expSlider.value = currentEXP/levelEXP;

            maxHealth = GetHealthByLevel(level);
            currentHealth = maxHealth;

            healthSlider.value = currentHealth/maxHealth;
            healthText.text = currentHealth.ToString("F0") + " / " + maxHealth.ToString("F0"); 
        }

        expSlider.value = currentEXP/levelEXP;
    }

    public void AddStamina(int amount){
        currentStamina += amount;

        if(currentStamina > maxStamina) currentStamina = maxStamina;

        staminaSlider.value = currentStamina/maxStamina;
    }

    IEnumerator HandleDead(){
        yield return new WaitForSeconds(2f);

        deadScreenManager.ShowDeadScreen(this);
    }

    public void Revival(){
        // Drop item
        inventoryManager.DropAllItems();

        // Revival
        HealHP(maxHealth);
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point");
        gameObject.transform.position = spawnPoint.transform.position;
        gameObject.transform.eulerAngles = spawnPoint.transform.eulerAngles;

        isDead = false;
        playerAnimator.PlayAnimation("Empty", false);
    }

    void RegenerateStamina(){
        if(playerManager.isInteracting || playerManager.isSprinting){
            staminaRegenTimer = 0;
        }else{
            staminaRegenTimer += Time.deltaTime;

            if(currentStamina < maxStamina && staminaRegenTimer > 1f){
                currentStamina += (playerManager.isBlocking ? staminaRegenerationAmountWhilstBlocking : staminaRegenerationAmount) * Time.deltaTime;

                if(currentStamina > maxStamina) currentStamina = maxStamina;

                staminaSlider.value = currentStamina/maxStamina;
            }
        }   
    }

    public void DeductStamina(float staminaToDeduct){
        currentStamina -= staminaToDeduct;

        staminaSlider.value = currentStamina < 0 ? 0 : currentStamina/maxStamina;
    }

    public void ConfirmAtrributePoints(){
        maxStamina = GetStaminaByLevel(level);
        staminaSlider.value = currentStamina/maxStamina;
        
        maxHealth = GetHealthByLevel(level);
        currentHealth = maxHealth;
        healthSlider.value = 1;
        healthText.text = currentHealth.ToString("F0") + " / " + maxHealth.ToString("F0"); 
    }
}
