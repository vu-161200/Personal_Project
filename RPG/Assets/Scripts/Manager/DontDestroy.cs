using UnityEngine;

// Không phá hủy đối tượng khi load scene
public class DontDestroy : MonoBehaviour
{
    public string objectID;

    void Awake() {
        objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }

    void Start(){
        DontDestroy[] dontDestroys = FindObjectsOfType<DontDestroy>();
        for (int i = 0; i < dontDestroys.Length; i++){
            if(dontDestroys[i] != this && dontDestroys[i].objectID == this.objectID){
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
