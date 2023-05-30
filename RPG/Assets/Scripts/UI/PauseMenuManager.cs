using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Refs")]
    PlayerInput playerInput;
    DialogueManager dialogManager;

    [Header("UI")]
    public GameObject pauseScreen;

    public bool isPaused;

    void Awake() {
        playerInput = GetComponentInParent<PlayerInput>();
        dialogManager = GetComponent<DialogueManager>();

        pauseScreen.SetActive(false);
    }

    void Update() {
        if(playerInput.pause_Input){
            if(playerInput.shopFlag || playerInput.chestFlag){
                return;
            }
            
            // Skip dialogue
            if(playerInput.dialogueFlag){
                dialogManager.SkipDialogues();
                return;
            }

            // Đóng kho đồ khi ấn ESC
            if(playerInput.inventoryFlag){
                playerInput.cameraHandler.ChangeCursor(true, true);
                playerInput.inventoryFlag = false;
                playerInput.inventoryManager.CloseInventory();
                return;
            }

            if(playerInput.pauseFlag) ResumeGame();
            else PauseGame();
        
        }
    }

    public void PauseGame(){
        isPaused = true;
        playerInput.cameraHandler.ChangeCursor(playerInput.pauseFlag, true);
        pauseScreen.SetActive(true);
            
        playerInput.pauseFlag = true;
    }

    public void ResumeGame(){
        isPaused = false;
        playerInput.cameraHandler.ChangeCursor(playerInput.pauseFlag, true);
        pauseScreen.SetActive(false);
                
        playerInput.pauseFlag = false;
    }

    public void QuitGame(){
        GetComponentInChildren<EventSystem>().enabled = false;
        SceneController.instance.LoadScene(0);
    }
}
