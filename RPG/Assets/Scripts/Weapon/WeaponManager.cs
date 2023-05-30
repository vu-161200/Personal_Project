using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private WeaponData equippedWeapon;

    private GameObject currentWeapon;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void EquipWeapon(WeaponData weapon)
    {
        equippedWeapon = weapon;

        if(currentWeapon != null){
            Destroy(currentWeapon);
        }

        currentWeapon = Instantiate(weapon.weaponPrefab);
        currentWeapon.transform.SetParent(weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}