using UnityEngine;
using static Enums;

// Thông tin mũi tên
[CreateAssetMenu(menuName = "Items/Arrow")]
public class ArrowData : Item
{
    [Header("Arrow Velocity")]
    public float forwardVelocity = 550f;
    public float upwardVelocity = 0;
    public float arrowMass = 0;
    public bool useGravity = false;

    [Header("Arrow Damage")]
    public float damage = 20f;

    [Header("Arrow Model")]
    public GameObject loadedItemModel;
    public GameObject liveItemModel;
    public GameObject penetratedItemModel;

    public void Awake()
    {
        itemType = ItemType.Ammo;
        equipType = EquipType.Hotbar;
    }
}
