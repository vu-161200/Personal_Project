using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUIManager : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text bossName;

    private void Awake() {
        DeactivateUI();
    }

    public void ActivateUI(){
        healthBar.gameObject.transform.parent.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
        bossName.gameObject.SetActive(true);
    }

    public void DeactivateUI(){
        healthBar.gameObject.transform.parent.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        bossName.gameObject.SetActive(false);
    }

    public void InitializeUI(string name, float maxHealth){
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        bossName.text = name;
    }

    public void UpdateHealth(float currentHealth){
        healthBar.value = currentHealth;
    }

}
