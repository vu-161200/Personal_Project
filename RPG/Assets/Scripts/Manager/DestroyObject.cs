using UnityEngine;

// Phát hủy đối tượng sau 1 khoảng thời gian
// Áp dụng với hiệu ứng máu khi nhận damage ...
public class DestroyObject : MonoBehaviour
{
    public float timeUntilDestroyed = 1.5f;
    
    private void Awake() {
        Destroy(gameObject, timeUntilDestroyed);
    }
}
