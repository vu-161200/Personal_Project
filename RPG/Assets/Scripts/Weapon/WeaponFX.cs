using UnityEngine;

public class WeaponFX : MonoBehaviour
{
    public ParticleSystem weaponFX;

    private void Awake() {
        weaponFX.Stop();
    }

    public void PlayWeaponFX(){
        weaponFX.Stop();
        weaponFX.Play();
        // if(weaponFX.isStopped){
        //     weaponFX.Play();
        // }
    }
}
