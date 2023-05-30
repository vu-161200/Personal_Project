using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    EnemyMovement enemyMovement;
    EnemyAnimator enemyAnimator;
    public EnemyStats enemyStats;
    public EnemyEffects enemyEffects;
    public EnemyWeaponManager enemyWeaponManager;
    
    public Rigidbody rig;
    public NavMeshAgent nav;

    [Header("Detection Settings")]
    public float radius = 25;
    // Tương tự như tầm nhìn của mắt
    public float maxAngle = 60;
    public float minAngle = -60;
    
    [Header("Enemy Info")]
    public Vector3 basePosition;
    public float rotationSpeed = 15f;
    public float currentCooldownTime = 0;
    public float maxAttackRange = 1.75f;

    [Header("Enemy State")]
    public bool isPerformingAction;
    public bool baseStateIsIdle;
    public State baseState;
    public State currentState;

    [Header("Combo Settings")]
    public bool allowPerformCombos = true;
    public float comboLikelyHood = 100f;

    [Header("Flags")]
    public bool canRotate;
    public bool isRotatingWithRootMotion;

    [Header("Target Information")]
    public CharacterStats target;
    public float distanceFromTarget;
    public Vector3 targetDirection;
    public float viewableAngle;

    void Awake(){
        enemyWeaponManager = GetComponent<EnemyWeaponManager>();
        enemyEffects = GetComponent<EnemyEffects>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyStats = GetComponent<EnemyStats>();
        rig = GetComponent<Rigidbody>();
        nav = GetComponentInChildren<NavMeshAgent>();
        enemyAnimator = GetComponent<EnemyAnimator>();

        basePosition = transform.position;

        if(baseStateIsIdle){
            baseState = GetComponentInChildren<IdleState>();
        }else{
            baseState = GetComponentInChildren<AmbushState>();
        }

        currentState = baseState;
    }

    // Start is called before the first frame update
    void Start(){
        nav.enabled = false;
        rig.isKinematic = false;
    }

    void Update(){
        HandleCooldownTime();
        HandleState();

        isRotatingWithRootMotion = enemyAnimator.anim.GetBool("isRotatingWithRootMotion");
        isInteracting = enemyAnimator.anim.GetBool("isInteracting");
        isInvulnerable = enemyAnimator.anim.GetBool("isInvulnerable");
        canCombo = enemyAnimator.anim.GetBool("canCombo");
        canRotate = enemyAnimator.anim.GetBool("canRotate");
        
        enemyAnimator.anim.SetBool("isDead", enemyStats.isDead);

        if(target != null){
            distanceFromTarget = Vector3.Distance(target.transform.position, transform.position);
            targetDirection = target.transform.position - transform.position;
            viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        }
    }

    void LateUpdate(){
        // Cập nhật vị trí (y) của AI dựa theo nav mesh
        // Fix Trường hợp không đi được lên bậc thang hoặc xuống dốc
        transform.position = new Vector3(transform.position.x, nav.transform.position.y, transform.position.z);
        
        // Cập nhật trasform so với parent
        nav.transform.localPosition = Vector3.zero;
        nav.transform.localRotation = Quaternion.identity;
    }

    public void HandleState(){
        if(currentState != null){
            State nextState = currentState.Tick(this, enemyStats, enemyAnimator);

            if(nextState != null){
                currentState = nextState;
            }
        }
    }

    public void HandleCharacterDead(){
        currentState = null;
        target = null;
        nav.enabled = false;
    }

    void HandleCooldownTime(){
        if(currentCooldownTime > 0){
            currentCooldownTime -= Time.deltaTime;
        }

        if(isPerformingAction && currentCooldownTime <= 0){
            isPerformingAction = false;
        }
    }

}
