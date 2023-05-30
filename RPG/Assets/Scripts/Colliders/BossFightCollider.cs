using UnityEngine;

// Sự kiện khi bắt đầu đánh BOSS
public class BossFightCollider : MonoBehaviour
{
    [Header("Small Boss")]
    public bool isSmallBoss = false;
    public Door door;

    [Header("Final Boss")]
    public GameObject[] walls;

    private void Awake() {
    }

    private void Start() {
        if(isSmallBoss){
            
        }else{
            foreach (var wall in walls) {
                wall.SetActive(false);
            }
        }
    }

    // Gán sự kiện khi đi qua
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if(playerStats != null){
                if(isSmallBoss){
                    EventManager.SmallBossFight(this, playerStats);
                }else{
                    EventManager.BossFight(this, playerStats);
                }
            }
        }
    }
}
