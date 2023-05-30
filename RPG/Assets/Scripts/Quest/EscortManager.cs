using UnityEngine;
using UnityEngine.AI;

public class EscortManager : MonoBehaviour
{
    [Header("Refs")]
    public Transform standPosition;
    public PlayerManager target;
    public NPC npc;

    NavMeshAgent nav;
    Animator anim;

    public bool isMovingToStandPosition = false;
    private void Awake() {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        npc = GetComponent<NPC>();
    }

    private void Start() {
        nav.enabled = false;
    }

    private void Update() {
        anim.SetBool("isMoving", nav.velocity.magnitude > 0.01f);

        if(target != null){
            nav.SetDestination(target.transform.position);
        }
        if(isMovingToStandPosition){
            nav.enabled = true;
            nav.SetDestination(standPosition.position);

            if(Vector3.Distance(standPosition.position, transform.position) <= 0.05f){
                nav.enabled = false;
                isMovingToStandPosition = false;

                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    0,
                    transform.eulerAngles.z
                );
            }
        }
    }

    public void StartEscort(PlayerManager _target){
        nav.enabled = true;
        npc.isEscorting = true;
        target = _target;
    }

    public void CompleteEscort(){
        EventManager.EscortCompleted(npc);

        nav.enabled = false;
        npc.isEscorting = false;
        target = null;
    }
    public void MoveToStandPoint(){
        isMovingToStandPosition = true;
    }
}
