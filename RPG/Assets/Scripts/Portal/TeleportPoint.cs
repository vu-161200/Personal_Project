using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public Transform targetDestination;

    private void OnTriggerStay(Collider other) {
        other.transform.position = targetDestination.position;
        other.transform.eulerAngles = targetDestination.eulerAngles;
    }

}
