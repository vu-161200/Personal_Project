using UnityEngine;
using UnityEngine.EventSystems;

public class WinScreenManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject hudUI;

    PlayerInput playerInput;

    private void Awake() {
        winScreen.SetActive(false);
    }

    public void ShowWinScreen(PlayerInput playerInput) {
        this.playerInput = playerInput;

        playerInput.cameraHandler.ChangeCursor(false, true);

        winScreen.SetActive(true);
        hudUI.SetActive(false);
    }

    public void HideWinScreen() {
        playerInput.cameraHandler.ChangeCursor(true, true);

        winScreen.SetActive(false);
        hudUI.SetActive(true);
    }

    public void Continue(){
        HideWinScreen();
    }

    public void Quit(){
        GetComponentInChildren<EventSystem>().enabled = false;
        SceneController.instance.LoadScene(0);
    }
}
