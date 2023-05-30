using UnityEngine;

public class WeaponSlotHolder : MonoBehaviour
{
    public Transform position;
    public WeaponData currentWeapon;
    public bool isLeftHand;
    public bool isRightHand;
    public bool isBackSlot;

    public GameObject weaponObject;

    public void LoadWeapon(WeaponData weaponData){
        UnloadWeaponAndDestroy();
        
        if(weaponData == null){
            UnloadWeapon();
            return;
        }

        GameObject model = Instantiate(weaponData.weaponPrefab) as GameObject;
        if(model != null){
            if(position != null) model.transform.parent = position;
            else model.transform.parent = transform;

            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        weaponObject = model;
    }

    public void UnloadWeapon(){
        if(weaponObject != null) weaponObject.SetActive(false);
    }

    public void UnloadWeaponAndDestroy(){
        if(weaponObject != null) Destroy(weaponObject);
    }
}
