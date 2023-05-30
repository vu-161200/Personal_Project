using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Cinemachine.CinemachineInputProvider[] cinemachineInputProviders;

    void Start(){
        cinemachineInputProviders = GetComponentsInChildren<Cinemachine.CinemachineInputProvider>();

        ChangeCursor(true);
    }

    // Hiển thị/Ẩn chuột trong game
    public void ChangeCursor(bool hidden, bool isStop = false){
        if(hidden){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            ActiveCinemachineInputProvider();

            if(isStop){
                Time.timeScale = 1f;
            }
        }else{
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            DisableCinemachineInputProvider();
            
            if(isStop){
                Time.timeScale = 0f;
            }
        }
    }

    
    void ActiveCinemachineInputProvider(){
        foreach(Cinemachine.CinemachineInputProvider inputProvider in cinemachineInputProviders){
            inputProvider.enabled = true;
        }
    }

    void DisableCinemachineInputProvider(){
        foreach(Cinemachine.CinemachineInputProvider inputProvider in cinemachineInputProviders){
            inputProvider.enabled = false;
        }
    }
}
