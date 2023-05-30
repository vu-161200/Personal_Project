using UnityEngine;
using UnityEngine.EventSystems;

public class DeadScreenManager : MonoBehaviour
{
    public GameObject deadScreen;
    public GameObject hudUI;

    PlayerStats playerStats;
    PlayerInput playerInput;

    private void Awake() {
        deadScreen.SetActive(false);
    }

    public void ShowDeadScreen(PlayerStats playerStats) {
        this.playerStats = playerStats;
        this.playerInput = playerStats.GetComponent<PlayerInput>();

        playerInput.cameraHandler.ChangeCursor(false, true);

        deadScreen.SetActive(true);
        hudUI.SetActive(false);
    }

    public void HideDeadScreen() {
        playerInput.cameraHandler.ChangeCursor(true, true);

        deadScreen.SetActive(false);
        hudUI.SetActive(true);
    }

    public void Revival(){
        playerStats.Revival();

        HideDeadScreen();
    }

    public void Quit(){
        GetComponentInChildren<EventSystem>().enabled = false;
        SceneController.instance.LoadScene(0);
    }
}
