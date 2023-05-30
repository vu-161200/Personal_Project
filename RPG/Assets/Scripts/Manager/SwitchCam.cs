using UnityEngine;
using Cinemachine;

// Zoom/Unzoom cam qua lại khi ngắm cung
public class SwitchCam : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject defaultCrosshair;
    public GameObject aimCrosshair;

    public CinemachineFramingTransposer cinemachineFramingTransposer;


    private void Awake() {
        playerManager = FindObjectOfType<PlayerManager>();

        cinemachineFramingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update() {
        if(playerManager.isAiming){
            cinemachineFramingTransposer.m_CameraDistance = 3;
            cinemachineFramingTransposer.m_TrackedObjectOffset.y = 1.25f;
            cinemachineFramingTransposer.m_ScreenX = 0.3f;
            cinemachineFramingTransposer.m_ScreenY = 0.4f;
            
            aimCrosshair.SetActive(true);
            defaultCrosshair.SetActive(false);
        }else{   
            cinemachineFramingTransposer.m_CameraDistance = 5;
            cinemachineFramingTransposer.m_TrackedObjectOffset.y = 1f;
            cinemachineFramingTransposer.m_ScreenX = 0.5f;
            cinemachineFramingTransposer.m_ScreenY = 0.5f;

            aimCrosshair.SetActive(false);
            defaultCrosshair.SetActive(true);
        }

    }
}