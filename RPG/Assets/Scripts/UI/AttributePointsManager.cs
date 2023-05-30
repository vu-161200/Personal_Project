using UnityEngine;
using TMPro;
using System.Globalization;

public class AttributePointsManager : MonoBehaviour
{
    [Header("Attribute Points")]
    public TMP_Text attributePointsText;

    [Header("HP")]
    public TMP_Text currentHPText;
    public TMP_Text projectedHPText;

    [Header("STA")]
    public TMP_Text currentSTAText;
    public TMP_Text projectedSTAText;

    [Header("ATK")]
    public TMP_Text currentATKText;
    public TMP_Text projectedATKText;

    [Header("DEF")]
    public TMP_Text currentDEFText;
    public TMP_Text projectedDEFText;

    PlayerStats playerStats;
    InventoryManager inventoryManager;
    int attributePoints;

    private void Awake() {
        playerStats = GetComponentInParent<PlayerStats>();
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    void OnEnable(){
        attributePoints = playerStats.attributePoints;
        attributePointsText.text = attributePoints.ToString();

        currentHPText.text = playerStats.baseHP.ToString();
        currentSTAText.text = playerStats.baseSTA.ToString();
        currentATKText.text = playerStats.baseATK.ToString();
        currentDEFText.text = playerStats.baseDEF.ToString();

        projectedHPText.text = playerStats.baseHP.ToString();
        projectedSTAText.text = playerStats.baseSTA.ToString();
        projectedATKText.text = playerStats.baseATK.ToString();
        projectedDEFText.text = playerStats.baseDEF.ToString();
    }

    public void Confirm(){
        playerStats.attributePoints = attributePoints;

        playerStats.baseHP = float.Parse(projectedHPText.text, CultureInfo.InvariantCulture.NumberFormat);
        playerStats.baseSTA = float.Parse(projectedSTAText.text, CultureInfo.InvariantCulture.NumberFormat);
        playerStats.baseATK = float.Parse(projectedATKText.text, CultureInfo.InvariantCulture.NumberFormat);
        playerStats.baseDEF = float.Parse(projectedDEFText.text, CultureInfo.InvariantCulture.NumberFormat);

        playerStats.ConfirmAtrributePoints();
        inventoryManager.CloseAttributePointsUI();
    }

    public void AddHP(){
        if(attributePoints <= 0) return;

        float currentProjectedHP = float.Parse(projectedHPText.text, CultureInfo.InvariantCulture.NumberFormat);
        projectedHPText.text = (currentProjectedHP + playerStats.hpByPoint).ToString();

        attributePointsText.text = (--attributePoints).ToString();
    }

    public void AddSTA(){
        if(attributePoints <= 0) return;
        
        float currentProjectedSTA = float.Parse(projectedSTAText.text, CultureInfo.InvariantCulture.NumberFormat);
        projectedSTAText.text = (currentProjectedSTA + playerStats.staByPoint).ToString();

        attributePointsText.text = (--attributePoints).ToString();
    }

    public void AddATK(){
        if(attributePoints <= 0) return;
        
        float currentProjectedATK = float.Parse(projectedATKText.text, CultureInfo.InvariantCulture.NumberFormat);
        projectedATKText.text = (currentProjectedATK + playerStats.atkByPoint).ToString();

        attributePointsText.text = (--attributePoints).ToString();
    }

    public void AddDEF(){
        if(attributePoints <= 0) return;
        
        float currentProjectedDEF = float.Parse(projectedDEFText.text, CultureInfo.InvariantCulture.NumberFormat);
        projectedDEFText.text = (currentProjectedDEF + playerStats.defByPoint).ToString();

        attributePointsText.text = (--attributePoints).ToString();
    }
}
