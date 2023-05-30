using UnityEngine;
using static Enums;

public class WeaponSlotManager : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerEquipment playerEquipment;
    PlayerInput playerInput;
    PlayerAnimator playerAnimator;
    PlayerEffects playerEffects;
    PlayerStats playerStats;

    public WeaponSlotHolder leftHand;
    public WeaponSlotHolder rightHand;
    public WeaponData unarmedWeapon;
    WeaponSlotHolder backSlot;

    DamageCollider leftHandDamage;
    DamageCollider rightHandDamage;

    [Header("Animation")]
    public AnimatorOverrideController bowAndSword;
    public AnimatorOverrideController bowAndUnarmed;
    public AnimatorOverrideController shieldAndSword;
    public AnimatorOverrideController shieldAndUnarmed;
    public AnimatorOverrideController sword;
    public AnimatorOverrideController unarmed;

    void Awake(){
        playerManager = GetComponent<PlayerManager>();
        playerEquipment = GetComponent<PlayerEquipment>();
        playerInput = GetComponent<PlayerInput>();
        playerEffects = GetComponent<PlayerEffects>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerStats = GetComponent<PlayerStats>();

        WeaponSlotHolder[] weaponHolderSlots = GetComponentsInChildren<WeaponSlotHolder>();
        foreach (WeaponSlotHolder item in weaponHolderSlots)
        {
            if(item.isLeftHand) leftHand = item;
            else if(item.isRightHand) rightHand = item;
            else if(item.isBackSlot) backSlot = item;
        }
    }

    public void LoadAllWeapons(){
        LoadWeaponOnSlot(playerEquipment.rightWeapon, false);
        LoadWeaponOnSlot(playerEquipment.leftWeapon, true);
    }

    public void LoadWeaponOnSlot(WeaponData weapon, bool isLeftHand){
        if(weapon != null){
            if(isLeftHand){
                leftHand.currentWeapon = weapon;
                leftHand.LoadWeapon(weapon);

                if(leftHand.currentWeapon.weaponType != WeaponType.Bow){
                    LoadWeaponDamage(true, weapon.baseDamage);
                }
            }else{
                rightHand.currentWeapon = weapon;
                rightHand.LoadWeapon(weapon);
                LoadWeaponDamage(false, weapon.baseDamage);

                // playerAnimator.anim.runtimeAnimatorController = weapon.weaponController;
            }
        }else{
            weapon = unarmedWeapon;

            if(isLeftHand){
                playerEquipment.leftWeapon = unarmedWeapon;
                leftHand.currentWeapon = weapon;
                leftHand.LoadWeapon(weapon);
                LoadWeaponDamage(true, unarmedWeapon.baseDamage);
            }else{
                playerEquipment.rightWeapon = unarmedWeapon;
                rightHand.currentWeapon = weapon;
                rightHand.LoadWeapon(weapon);
                LoadWeaponDamage(false, unarmedWeapon.baseDamage);
                // playerAnimator.anim.runtimeAnimatorController = weapon.weaponController;
            }
        }

        AssignAnimation();
    }

    void AssignAnimation(){
        // Bow + Sword
        if(rightHand.currentWeapon != null && rightHand.currentWeapon.weaponType == WeaponType.Sword && leftHand.currentWeapon != null && leftHand.currentWeapon.weaponType == WeaponType.Bow){
            playerAnimator.anim.runtimeAnimatorController = bowAndSword;
        }
        // Bow + Unarmed
        else if(rightHand.currentWeapon != null && rightHand.currentWeapon.weaponType == WeaponType.Unarmed && leftHand.currentWeapon != null && leftHand.currentWeapon.weaponType == WeaponType.Bow){
            playerAnimator.anim.runtimeAnimatorController = bowAndUnarmed;
        }
        // Shield + Sword
        else if(rightHand.currentWeapon != null && rightHand.currentWeapon.weaponType == WeaponType.Sword && leftHand.currentWeapon != null && leftHand.currentWeapon.weaponType == WeaponType.Shield){
            playerAnimator.anim.runtimeAnimatorController = shieldAndSword;
        }
        // Shield + Unarmed
        else if(rightHand.currentWeapon != null && rightHand.currentWeapon.weaponType == WeaponType.Unarmed && leftHand.currentWeapon != null && leftHand.currentWeapon.weaponType == WeaponType.Shield){
            playerAnimator.anim.runtimeAnimatorController = shieldAndUnarmed;
        }
        // Unarmed + Sword
        else if(rightHand.currentWeapon != null && rightHand.currentWeapon.weaponType == WeaponType.Sword && leftHand.currentWeapon != null && leftHand.currentWeapon.weaponType == WeaponType.Unarmed){
            playerAnimator.anim.runtimeAnimatorController = sword;
        }
        // Sword + Unarmed
        else if(rightHand.currentWeapon != null && rightHand.currentWeapon.weaponType == WeaponType.Unarmed && leftHand.currentWeapon != null && leftHand.currentWeapon.weaponType == WeaponType.Sword){
            playerAnimator.anim.runtimeAnimatorController = unarmed;
        }
        // Sword + Sword
        else if(rightHand.currentWeapon != null && rightHand.currentWeapon.weaponType == WeaponType.Sword && leftHand.currentWeapon != null && leftHand.currentWeapon.weaponType == WeaponType.Sword){
            playerAnimator.anim.runtimeAnimatorController = sword;
        }
        // Unarmed + Unarmed
        else if(rightHand.currentWeapon != null && rightHand.currentWeapon.weaponType == WeaponType.Unarmed && leftHand.currentWeapon != null && leftHand.currentWeapon.weaponType == WeaponType.Unarmed){
            playerAnimator.anim.runtimeAnimatorController = unarmed;
        }

    }

    void LoadWeaponDamage(bool isLeft, float damage = 5f){
        if(isLeft){
            leftHandDamage = leftHand.weaponObject.GetComponentInChildren<DamageCollider>();

            leftHandDamage.currentWeaponDamage = damage;
            leftHandDamage.characterManager = GetComponent<CharacterManager>();
            leftHandDamage.teamID = playerStats.teamID;

            playerEffects.leftWeaponFX = leftHand.weaponObject.GetComponentInChildren<WeaponFX>();
        } 
        else{
            rightHandDamage = rightHand.weaponObject.GetComponentInChildren<DamageCollider>();
            
            rightHandDamage.currentWeaponDamage = damage;
            rightHandDamage.characterManager = GetComponent<CharacterManager>();
            rightHandDamage.teamID = playerStats.teamID;

            playerEffects.rightWeaponFX = rightHand.weaponObject.GetComponentInChildren<WeaponFX>();
        } 
    }

    public void EnableWeaponDamage(){
        if(playerManager.isUsingRightHand) rightHandDamage.EnableCollider();
        else {
            if(leftHandDamage != null) leftHandDamage.EnableCollider();
        }
    }

    public void DisableWeaponDamage(){
        if(playerManager.isUsingRightHand) rightHandDamage.DisableCollider();
        else{
            if(leftHandDamage != null) leftHandDamage.DisableCollider();
        } 
    }

}
