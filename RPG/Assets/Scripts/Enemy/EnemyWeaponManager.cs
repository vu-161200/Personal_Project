using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour
{
    public GameObject leftWeapon;
    public GameObject rightWeapon;

    EnemyEffects enemyEffects;
    EnemyStats enemyStats;

    public DamageCollider leftHandDamage;
    public DamageCollider rightHandDamage;

    void Start(){
        enemyEffects = GetComponent<EnemyEffects>();
        enemyStats = GetComponent<EnemyStats>();

        if(leftWeapon != null){
            leftHandDamage = leftWeapon.GetComponent<DamageCollider>();

            // Damage
            leftHandDamage.characterManager = GetComponent<CharacterManager>(); 
            leftHandDamage.teamID = enemyStats.teamID;

            enemyEffects.leftWeaponFX = leftWeapon.GetComponentInChildren<WeaponFX>();
        }

        if(rightWeapon != null){
            rightHandDamage = rightWeapon.GetComponent<DamageCollider>();

            rightHandDamage.characterManager = GetComponent<CharacterManager>(); 
            rightHandDamage.teamID = enemyStats.teamID;

            enemyEffects.rightWeaponFX = rightWeapon.GetComponentInChildren<WeaponFX>();
        }
    }

    public void EnableLeftWeaponDamage(){
        if(leftHandDamage != null) leftHandDamage.EnableCollider();
    }

    public void EnableRightWeaponDamage(){
        if(rightHandDamage != null) rightHandDamage.EnableCollider();
    }

    public void EnableAllWeaponDamage(){
        EnableRightWeaponDamage();
        EnableLeftWeaponDamage();
    }

    public void DisableWeaponDamage(){
        if(rightHandDamage != null) rightHandDamage.DisableCollider();
        if(leftHandDamage != null) leftHandDamage.DisableCollider();
    }

}
