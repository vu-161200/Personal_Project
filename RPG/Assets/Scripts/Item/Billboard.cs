using UnityEngine;

// Đối tượng sẽ hướng vào camera của người dùng
public class Billboard : MonoBehaviour
{
    Camera _camera;
    
    private void Start() {
        _camera = Camera.main;
    }

    void LateUpdate(){
        // transform.LookAt(_camera.transform);

        // transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        transform.forward = _camera.transform.forward;
    }
}
