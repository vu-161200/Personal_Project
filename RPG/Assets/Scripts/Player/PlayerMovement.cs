using System.Collections;
using UnityEngine;

// Nhân vật di chuyển
public class PlayerMovement : MonoBehaviour
{
    [Header("Refs")]
    PlayerManager playerManager;
    Transform cameraTransform; // cameraObject
    PlayerInput playerInput;
    public Vector3 direction; // moveDirection

    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisionBlockerCollider;

    [HideInInspector] public Transform myTransform;
    [HideInInspector] public PlayerAnimator playerAnimator;

    public Rigidbody rig;
    // public GameObject cameraObject; // normalCamera

    [Header("Ground & Fall Stats")]
    [SerializeField] private float groundStartPoint = -0.5f; // Thay đổi phải chỉnh lại HandleFalling
    [SerializeField] private float minimumDistanceToFall = 1f; // Thay đổi phải chỉnh lại HandleFalling
    [SerializeField] private float groundCheckDistance = 0.2f; // Thay đổi phải chỉnh lại HandleFalling
    public LayerMask groundCheck;
    public float inAirTimer;

    [Header("Movement Stats")]
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float walkingSpeed = 5f;
    [SerializeField] float sprintSpeed = 12f;
    [SerializeField] float rotationSpeed = 8f;
    [SerializeField] float fallingSpeed = 550f;
    [SerializeField] float jumpForce = 550f;
    public bool jumpForceApplied;

    [Header("Stamina Cost")]
    public float rollCost = 15;
    public float backStepCost = 5;
    public float sprintCost = 0.5f;

    // Start is called before the first frame update
    void Awake(){
        playerManager = GetComponent<PlayerManager>();
        rig = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<PlayerAnimator>();

        cameraTransform = Camera.main.transform;
        myTransform = transform;
        playerAnimator.Initialize();

        playerManager.isGrounded = true;

        Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
    }

    void FixedUpdate(){
        if (jumpForceApplied){
            StartCoroutine(DisableForce());
            rig.AddForce(transform.up * jumpForce);
        }
    }

    // Tắt áp dụng lực sau x(s)
    IEnumerator DisableForce(){
        yield return new WaitForSeconds(0.3f);
        jumpForceApplied = false;
    }

    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;

    public void HandleMovement(){
        if(playerInput.rollFlag || playerManager.isInteracting || !playerManager.isGrounded) return;

        direction = cameraTransform.forward * playerInput.vertical + cameraTransform.right * playerInput.horizontal;
        // Chuẩn hóa Vector (tổng bình phương các phần tử của nó bằng 1)
        direction.Normalize();
        direction.y = 0; // Cập nhật y = 0 <=> tiếp xúc với mặt đất

        float speed = movementSpeed;

        // Trường hợp chạy ==> Kiểm tra stamina đủ không ==> Update speed
        if(playerInput.sprintFlag && playerInput.moveAmount > 0.5f){
            if(playerManager.playerStats.currentStamina >= sprintCost){
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                direction *= speed;

                playerManager.playerStats.DeductStamina(sprintCost);
            }else{
                playerInput.sprintFlag = false;
                playerManager.isSprinting = false;
                direction *= walkingSpeed;
            }
        }else{
            if(playerInput.moveAmount <= 0.5f){
                direction *= walkingSpeed;
                playerManager.isSprinting = false;
            } 
            else{
                direction *= speed;
                playerManager.isSprinting = false;
            } 
        }

        // Tính toán projection (tính toán phần của một vector gắn với một mặt phẳng, bỏ qua phần dọc trục của mặt phẳng) của 'direction' trên mặt phẳng 'normalVector'
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(direction, normalVector);
        rig.velocity = projectedVelocity; // Cập nhật tốc độ theo 3 trục của vector3 'projectedVelocity'

        playerAnimator.UpdateAnimator(playerInput.moveAmount, 0, playerManager.isSprinting);
    }

    public void HandleRolling(){
        if(playerAnimator.anim.GetBool("isInteracting") || playerManager.playerStats.currentStamina < backStepCost) return;

        if(playerInput.rollFlag){
            direction = cameraTransform.forward * playerInput.vertical + cameraTransform.right * playerInput.horizontal;

            if(playerInput.moveAmount > 0 && playerManager.playerStats.currentStamina >= rollCost){
                playerManager.playerStats.DeductStamina(rollCost);

                playerAnimator.PlayAnimation("Rolling", true);
                direction.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(direction);
                myTransform.rotation = rollRotation;
            }else{
                playerAnimator.PlayAnimation("Backstep", true);

                playerManager.playerStats.DeductStamina(backStepCost);
            }
        }
    }

    public void HandleFalling(Vector3 direction){
        playerManager.isGrounded = false;
        RaycastHit hit;

        Vector3 origin = myTransform.position;
        origin.y += groundStartPoint;

        if(Physics.Raycast(origin, myTransform.forward, out hit, 0.4f)){
            direction = Vector3.zero;
        }

        if(playerManager.isInAir){
            rig.AddForce(-Vector3.up * fallingSpeed);
            rig.AddForce(direction * fallingSpeed / 10f);
        }

        Vector3 dir = direction;
        dir.Normalize();
        origin += dir * groundCheckDistance;

        targetPosition = myTransform.position;

        Debug.DrawRay(origin, -Vector3.up * minimumDistanceToFall, Color.red, 0.1f, false);
        if(Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceToFall, groundCheck)){
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y + 1; // +1 ==> Vi tri cua nhan vat (y) la 1

            if(playerManager.isInAir){ 
                if(inAirTimer > 0.5f){
                    playerAnimator.PlayAnimation("Land", true);
                    playerManager.characterSound.PlaySound("land");
                    inAirTimer = 0;
                }else{
                    playerAnimator.PlayAnimation("Empty", false);
                    inAirTimer = 0;
                }

                playerManager.isInAir = false;
            }
        }else{
            if(playerManager.isGrounded){
                playerManager.isGrounded = false;
            }

            if(playerManager.isInAir == false){
                if(playerManager.isInteracting == false){
                    playerAnimator.PlayAnimation("Falling", true);
                }

                Vector3 vel = rig.velocity;
                vel.Normalize();
                rig.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;
            }
        }

        if(playerManager.isInteracting || playerInput.moveAmount > 0){
            myTransform.position  = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
        }else{
            myTransform.position  = targetPosition;
        }
    }

    public void HandleJumping(){
        if(playerManager.isInteracting) return;

        if(playerInput.jump_Input){
            if(playerInput.moveAmount > 0){
                direction = cameraTransform.forward * playerInput.vertical + cameraTransform.right * playerInput.horizontal;

                playerAnimator.PlayAnimation("Jump", true);
                playerManager.characterSound.PlaySound("jump");

                jumpForceApplied = true;

                direction.y = 0;
                Quaternion jumpRotation = Quaternion.LookRotation(direction);
                myTransform.rotation = jumpRotation;
            }else{
                playerAnimator.PlayAnimation("Jump", true);
                playerManager.characterSound.PlaySound("jump");
            }
            
        }
    }

    public void HandleRotation(){
        if(playerAnimator.canRotate){
            if(playerManager.isAiming){
                Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = playerRotation;
            }else{
                Vector3 targetDir = Vector3.zero;
                targetDir = cameraTransform.forward * playerInput.vertical + cameraTransform.right * playerInput.horizontal;
                targetDir.Normalize();
                targetDir.y = 0;

                if(targetDir == Vector3.zero) targetDir = myTransform.forward;

                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rotationSpeed * Time.deltaTime);

                myTransform.rotation = targetRotation;
            }
        }
    }

    #endregion

}
