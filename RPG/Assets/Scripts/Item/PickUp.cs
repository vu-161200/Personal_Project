using UnityEditor;
using UnityEngine;

// Vật phẩm nhặt
public class PickUp : Interactable, ISerializationCallbackReceiver
{
    public Item item;
    public int stackSize = 1;

    public override void Interact(PlayerInteract playerInteract){
        base.Interact(playerInteract);

        PlayerMovement playerMovement = playerInteract.GetComponent<PlayerMovement>();
        PlayerAnimator playerAnimator = playerInteract.GetComponent<PlayerAnimator>();
        InventoryManager inventoryManager = playerInteract.GetComponent<InventoryManager>();

        playerMovement.rig.velocity = Vector3.zero; // Dừng di chuyển khi nhặt item

        // Animation
        playerAnimator.PlayAnimation("PickUp", true);

        // Thêm vào kho đồ
        inventoryManager.AddItem(this);
    }

    // 2D Icon ở trên map
    public void SetSerialize(){
        GetComponentInChildren<SpriteRenderer>().sprite = item.itemIcon;

        #if UNITY_EDITOR
            EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
        #endif
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize(){
        
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize(){
        SetSerialize();
    }
}
