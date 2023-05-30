using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    [Header("Weapon FX")]
    public WeaponFX rightWeaponFX;
    public WeaponFX leftWeaponFX;

    [Header("Blood FX")]
    public GameObject bloodSplatterFX;

    // Hiệu ứng khi chém
    public virtual void PlayWeaponFX(bool isLeft){
        if(isLeft){
            if(leftWeaponFX != null){
                leftWeaponFX.PlayWeaponFX();
            }
        }else{
            if(rightWeaponFX != null){
                rightWeaponFX.PlayWeaponFX();
            }
        }
    }

    // Hiệu ứng máu khi nhận damaeg
    public virtual void PlayBloodSplatterFX(Vector3 location){
        // Quaternion.identity: không biến đổi góc của đối tượng
        GameObject blood = Instantiate(bloodSplatterFX, location, Quaternion.identity);
    }
}
