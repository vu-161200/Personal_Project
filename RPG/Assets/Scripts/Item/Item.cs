using UnityEngine;
using static Enums;

// Thông tin vật phẩm chung
public class Item : ScriptableObject
{
    [Header("Item Information")]
    public string ID = "ITEM";
    public ItemType itemType;
    public EquipType equipType;
    public Sprite itemIcon;
    public string itemName;
    [TextArea(4, 4)]
    public string itemDescription = "";

    [Space]
    public int price = 1;
    public int maxStack = 1;
}
