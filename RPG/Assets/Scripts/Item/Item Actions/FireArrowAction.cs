using UnityEngine;

// Bắn tên khi nhả chuột phải khi đang trong trạng thái ngắm bắn
[CreateAssetMenu(menuName = "Item Actions/Fire Arrow Action")]
public class FireArrowAction : ItemAction
{
    public override void PerformAction(PlayerManager playerManager){
        // Vị trí arrow
        ArrowInstantiationLocation arrowInstantiationLocation = playerManager.weaponSlotManager.leftHand.GetComponentInChildren<ArrowInstantiationLocation>();

        // Animation bắn
        Animator bowAnim = playerManager.weaponSlotManager.leftHand.GetComponentInChildren<Animator>();
        bowAnim.SetBool("isDrawn", false);
        bowAnim.Play("Fire");

        // Cập nhật flag
        playerManager.playerAnimator.PlayAnimation("Shoot", true);
        playerManager.playerAnimator.anim.SetBool("isHoldingArrow", false);

        // Destroy arrow đã tạo ở cung
        Destroy(playerManager.playerEffects.currentRangeFX);

        // Tạo và bắn tên
        GameObject liveArrow = Instantiate(playerManager.playerEquipment.arrow.liveItemModel, arrowInstantiationLocation.transform.position, playerManager.transform.rotation);

        // Tạo một tia xuyên qua 1 điểm ở trung tâm màn hình từ camera 
        Ray ray = playerManager.cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitPoint;

        // Nếu tia đó tiếp xúc với bất kì đói tượng nào đó trong phạm vi <= 100f
        if(Physics.Raycast(ray, out hitPoint, 100f)){
            // Mũi tên luôn hướng vào đối tượng đó
            liveArrow.transform.LookAt(hitPoint.point);
        }else{
            // Gán rotation của mũi tên theo camera
            liveArrow.transform.rotation = Quaternion.Euler(playerManager.cameraObject.transform.localEulerAngles.x, playerManager.cameraObject.transform.eulerAngles.y, 0);
        }

        // Gán vận tốc
        Rigidbody rig = liveArrow.GetComponentInChildren<Rigidbody>();

        rig.AddForce(liveArrow.transform.forward * playerManager.playerEquipment.arrow.forwardVelocity);
        rig.AddForce(liveArrow.transform.up * playerManager.playerEquipment.arrow.upwardVelocity);
        rig.useGravity = playerManager.playerEquipment.arrow.useGravity;
        rig.mass = playerManager.playerEquipment.arrow.arrowMass;

        liveArrow.transform.parent = null;

        // Set damage của mũi tên
        RangedProjectileDamageCollider damageCollider = liveArrow.GetComponentInChildren<RangedProjectileDamageCollider>();
        damageCollider.arrow = playerManager.playerEquipment.arrow;
        damageCollider.currentWeaponDamage = playerManager.playerEquipment.arrow.damage + playerManager.playerEquipment.leftWeapon.baseDamage;
        damageCollider.characterManager = playerManager;
        damageCollider.teamID = playerManager.playerStats.teamID;

        playerManager.isAiming = false;
        // Cập nhật số lượng mũi tên
        playerManager.playerEquipment.arrowStack--;
        playerManager.playerEquipment.equipmentManager.UpdateArrow(playerManager.playerEquipment.arrow, playerManager.playerEquipment.arrowStack);
    }
}
