using UnityEngine;

// Kiểm tra điều kiện để hoàn thành nhiệm vụ hộ tống (đi qua collider này)
public class EscortDestinationCollider : MonoBehaviour
{
    
    private void OnTriggerExit(Collider other) {
        if(other.tag == "NPC"){
            EscortManager em = other.GetComponent<EscortManager>();

            if(em != null){
                em.CompleteEscort();
            }
        }
    }
}
