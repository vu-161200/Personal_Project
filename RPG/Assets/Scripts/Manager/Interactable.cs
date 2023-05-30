using UnityEngine;

// Đối tượng có thể tương tác
public class Interactable : MonoBehaviour
{
    float radius = 0.5f;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);    
    }

    public virtual void Interact(PlayerInteract playerInteract){  
        // Nhìn vào đối tượng
        Vector3 rotation = transform.position - playerInteract.transform.position;
        rotation.y = 0;
        rotation.Normalize();

        Quaternion tr = Quaternion.LookRotation(rotation);
        Quaternion targetRotation = Quaternion.Slerp(playerInteract.transform.rotation, tr, 300 * Time.deltaTime);

        playerInteract.transform.rotation = targetRotation;
    }
}
