using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System;

public class SceneController : MonoBehaviour
{
    [Header("Singleton")]
    public static SceneController instance;

    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public TMP_Text progressText;

    private void Awake() {
        loadingScreen.SetActive(false);

        MakeSingleton();
    }

    void MakeSingleton(){
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(int sceneIndex){
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress /.9f);

            loadingSlider.value = progress;
            progressText.text = String.Format("{0:0.00}", progress * 100f) + " %";

            yield return null;
        }

        loadingScreen.SetActive(false);
    }


}
