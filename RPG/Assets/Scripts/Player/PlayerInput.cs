using UnityEngine;

// Các nút bấm của player
public class PlayerInput : MonoBehaviour
{
   
    [Header("Settings")]
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    [Header("Inputs")]
    public bool roll_Input; // Ctrl
    public bool sprint_Input; // Left Shift
    public bool jump_Input; // Space
    public bool lm_Input; // Left Mouse
    public bool rm_Input; // Right Mouse
    public bool mm_Input; // Middle Mouse
    public bool interact_Input; // F
    public bool inventory_Input; // E
    public bool block_Input; // Q
    public bool drink_Input; // X
    public bool hold_rm_Input;
    public bool pause_Input; // ESC

    [Header("Flags")]
    public bool rollFlag;
    public bool sprintFlag;
    public bool comboFlag;
    public bool fireFlag;
    public bool inventoryFlag;
    public bool pauseFlag;
    public bool shopFlag;
    public bool dialogueFlag;
    public bool chestFlag;

    [Header("Refs")]
    public CameraHandler cameraHandler;
    PlayerController playerController;
    PlayerCombat playerCombat;
    PlayerEquipment playerEquipment;
    public PlayerManager playerManager;
    PlayerEffects playerEffects;
    PlayerAnimator playerAnimator;
    WeaponSlotManager weaponSlotManager;
    public InventoryManager inventoryManager;


    Vector2 movement;
    Vector2 cameraInput;

    void Awake(){
        cameraHandler = FindObjectOfType<CameraHandler>();

        playerAnimator = GetComponent<PlayerAnimator>();
        playerCombat = GetComponent<PlayerCombat>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        playerEffects = GetComponent<PlayerEffects>();
        playerEquipment = GetComponent<PlayerEquipment>();
        playerManager = GetComponent<PlayerManager>();
        inventoryManager = GetComponent<InventoryManager>();
    }

    public void OnEnable(){
        if(playerController == null){
            playerController = new PlayerController();
            playerController.PlayerMovement.Movement.performed += playerController => movement = playerController.ReadValue<Vector2>();
            playerController.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerController.PlayerActions.HoldRM.performed += i => hold_rm_Input = true;
            playerController.PlayerActions.HoldRM.canceled += i => hold_rm_Input = false;
            playerController.PlayerActions.HoldRM.canceled += i => fireFlag = true;

            playerController.PlayerActions.LM.performed += i => lm_Input = true;
            playerController.PlayerActions.RM.performed += i => rm_Input = true;
            playerController.PlayerActions.MM.performed += i => mm_Input = true;

            playerController.PlayerActions.Jump.performed += i => jump_Input = true;

            playerController.PlayerActions.Block.performed += i => block_Input = true;
            playerController.PlayerActions.Block.canceled += i => block_Input = false;

            playerController.PlayerActions.Interact.performed += i => interact_Input = true;

            playerController.PlayerActions.Inventory.performed += i => inventory_Input = true;

            playerController.PlayerActions.Drink.performed += i => drink_Input = true;

            playerController.PlayerActions.Pause.performed += i => pause_Input = true;
        } 

        playerController.Enable();
    }

    public void OnDisable(){
        playerController.Disable();
    }

    public void CheckInput(){
        if(!pauseFlag && !inventoryFlag && !dialogueFlag && !shopFlag && !chestFlag){
            MoveInput();
            RollInput();
            SprintInput();
            UseConsumableInput();
            
            LMInput();
            RMInput();
            MMInput();

            HoldRMInput();
            FireArrowInput();
        }

        InventoryInput();
    }

    private void MoveInput(){
        if(playerManager.isHoldingArrow){
            horizontal = movement.x;
            vertical = movement.y;
            moveAmount = Mathf.Clamp01((Mathf.Abs(horizontal) + Mathf.Abs(vertical)));
            
            if(moveAmount > 0.5f) moveAmount = 0.5f;

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        else{
            horizontal = movement.x;
            vertical = movement.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    }

    private void RollInput(){
        roll_Input = playerController.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

        if(roll_Input){
            rollFlag = true;
        }else{
            rollFlag = false;
        }
    }

    private void SprintInput(){
        sprint_Input = playerController.PlayerActions.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

        if(sprint_Input && !rollFlag){
            sprintFlag = true;
        }else{
            sprintFlag = false;
        }
    }

    // LightAttack + Tấn công bằng vũ khí tay phải
    private void LMInput(){
        // Left Mouse
        if(lm_Input){
            if(playerEquipment.rightWeapon.tap_LM != null){
                playerManager.SetHandIsUsing(true);
                playerEquipment.currentItemBeingUsed = playerEquipment.rightWeapon;
                playerEquipment.rightWeapon.tap_LM.PerformAction(playerManager);
            }
        }
    }

    // LightAttack + Tấn công bằng vũ khí tay trái
    private void RMInput(){
        if(hold_rm_Input) return;

        // Right Mouse
        if(rm_Input){
            if(playerEquipment.leftWeapon.tap_RM != null){
                playerManager.SetHandIsUsing(false);
                playerEquipment.currentItemBeingUsed = playerEquipment.leftWeapon;
                playerEquipment.leftWeapon.tap_RM.PerformAction(playerManager);
            }
        }
    }

    // HeavyAttack + Tấn công bằng vũ khí tay phải
    private void MMInput(){
        // Middle Mouse
        if(mm_Input){
            if(playerEquipment.rightWeapon.tap_MM != null){
                playerManager.SetHandIsUsing(true);
                playerEquipment.currentItemBeingUsed = playerEquipment.rightWeapon;
                playerEquipment.rightWeapon.tap_MM.PerformAction(playerManager); 
            }
        }
    }

    // Ngắm cung / Khiên
    void HoldRMInput(){
        if(playerManager.isInAir || playerManager.isSprinting){
            hold_rm_Input = false;
            return;
        }

        if(hold_rm_Input){
            if(playerEquipment.leftWeapon.hold_RM != null){
                playerManager.SetHandIsUsing(false);
                playerEquipment.currentItemBeingUsed = playerEquipment.leftWeapon;
                playerEquipment.leftWeapon.hold_RM.PerformAction(playerManager);
            }
        }else{
            if(playerManager.isBlocking)
                playerManager.isBlocking = false;
        }
    }

    private void InventoryInput(){
        if(pauseFlag || chestFlag || dialogueFlag || shopFlag) return;

        if(inventory_Input){
            cameraHandler.ChangeCursor(inventoryFlag, true);
            inventoryFlag = !inventoryFlag;

            if(inventoryFlag){
                inventoryManager.OpenInventory();
            }else{
                inventoryManager.CloseInventory();
            }
        } 
    }

    private void UseConsumableInput(){
        if(drink_Input){
            drink_Input = false;
            
            if(playerEquipment.consumable != null && playerEquipment.consumableStack > 0){
                playerEquipment.consumable.AttemptToConsumableItem(playerAnimator, weaponSlotManager, playerEffects);
                playerEquipment.consumableStack -= 1;
                playerEquipment.equipmentManager.UpdateConsumable(playerEquipment.consumable, playerEquipment.consumableStack);
            }
        }
    }

    void FireArrowInput(){
        if(fireFlag){
            if(playerManager.isHoldingArrow) {
                fireFlag = false;
                playerEquipment.leftWeapon.action.PerformAction(playerManager);
            }else fireFlag = false;
        }
    }

}
