using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public EventSystem eventSystem;

    private void Awake() {
        eventSystem = GetComponentInChildren<EventSystem>();
    }

    private void Start() {
        eventSystem.enabled = true;
        
        // StartGame();
    }

    public void QuitGame(){
        Debug.Log("Good Bye!");
        Application.Quit();
    }

    public void StartGame(){
        eventSystem.enabled = false;

        SceneController.instance.LoadScene(1);
    }
}
