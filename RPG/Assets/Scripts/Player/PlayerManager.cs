using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : CharacterManager
{
    [Header("Refs")]
    public Camera cameraObject;
    Animator anim;
    public PlayerInput playerInput;
    public PlayerEffects playerEffects;
    public PlayerEquipment playerEquipment;
    public PlayerCombat playerCombat;
    PlayerMovement playerMovement;
    public PlayerStats playerStats;
    public PlayerAnimator playerAnimator;
    public WeaponSlotManager weaponSlotManager;
    public CharacterSound characterSound;

    [Header("UI Refs")]
    public GameObject decorationsUI;
    public GameObject equipmentUI;
    public GameObject chestUI;
    public Transform chestSlotParent;

    [Header("Player Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isHoldingArrow;
    public bool isAiming;

    void Awake() {
        playerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();
        playerCombat = GetComponent<PlayerCombat>();
        playerEffects = GetComponent<PlayerEffects>();
        playerEquipment = GetComponent<PlayerEquipment>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        characterSound = GetComponent<CharacterSound>();
        anim = GetComponent<Animator>();

        cameraObject = Camera.main;

        GetComponentInChildren<EventSystem>().enabled = false;
    }

    // Update is called once per frame
    void Update(){
        isInteracting = anim.GetBool("isInteracting");
        isInvulnerable = anim.GetBool("isInvulnerable");
        isParrying = anim.GetBool("isParrying");
        isHoldingArrow = anim.GetBool("isHoldingArrow");
        canCombo = anim.GetBool("canCombo");

        anim.SetBool("isInAir", isInAir);
        anim.SetBool("isBlocking", isBlocking);
        anim.SetBool("isAiming", isAiming);

        playerInput.CheckInput();

        playerAnimator.canRotate = anim.GetBool("canRotate");

        playerMovement.HandleRotation();
        playerMovement.HandleRolling();
        playerMovement.HandleJumping();

    }

    // Nên thực hiện FixedUpdate khi có AddForce or MovePosition
    void FixedUpdate(){
        playerMovement.HandleMovement();
        playerMovement.HandleFalling(playerMovement.direction);
    }

    void LateUpdate(){
        isSprinting = playerInput.sprint_Input;
        playerInput.roll_Input = false;
        playerInput.sprint_Input = false;
        playerInput.lm_Input = false;
        playerInput.rm_Input = false;
        playerInput.mm_Input = false;
        playerInput.jump_Input = false;
        playerInput.interact_Input = false;
        playerInput.inventory_Input = false;
        playerInput.pause_Input = false;
        
        if(isInAir){
            playerMovement.inAirTimer = playerMovement.inAirTimer + Time.deltaTime;
        }

    }

    // Cập nhật tay khi sử dụng vũ khí
    public void SetHandIsUsing(bool usingRightHand){
        if(usingRightHand){
            isUsingRightHand = true;
            isUsingLeftHand = false;
        }else{
            isUsingLeftHand = true;
            isUsingRightHand = false;
        }
    }

}
