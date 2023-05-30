using UnityEngine;

// Lưu trữ hiệu ứng/vật phẩm hiện tại
public class PlayerEffects : CharacterEffects
{
    public GameObject currentRangeFX;
    PlayerStats playerStats;
    WeaponSlotManager weaponSlotManager;

    public GameObject currentParticleFX;
    public GameObject instantiateFX;

    public int amount;
    public string type;

    private void Awake() {
        playerStats = GetComponent<PlayerStats>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }

    // Khi uống thuốc HP/MP/EXP
    public void BuffPlayerFromEffect(){
        // Thêm vào 
        if(type == "EXP"){
            playerStats.AddEXP(amount);
        }else if(type == "MP"){
            playerStats.AddStamina(amount);
        }else if(type == "HP"){
            playerStats.HealHP(amount);
        }

        // Hủy bình uống
        Destroy(instantiateFX.gameObject);

        // Cập nhật lại vũ khí ở tay đó (vũ khí trước khi hủy để tạo bình uống)
        weaponSlotManager.LoadAllWeapons();
    }
    
}
