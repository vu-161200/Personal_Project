using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    private void Awake() {
        MakeSingleton();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += MapFinishedLoading;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= MapFinishedLoading;
    }

    void MakeSingleton(){
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void MapFinishedLoading(Scene scene, LoadSceneMode mode){
        if(scene.name != "Main Menu"){

            GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point");

            if(spawnPoint != null){
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                
                if(playerObject != null){
                    playerObject.GetComponentInChildren<EventSystem>().enabled = true;

                    PauseMenuManager pmm = playerObject.GetComponentInChildren<PauseMenuManager>();
                    if(pmm.isPaused) pmm.ResumeGame();

                    playerObject.transform.position = spawnPoint.transform.position;
                    playerObject.transform.eulerAngles = spawnPoint.transform.eulerAngles;
                }
            }
        }
    }
}
