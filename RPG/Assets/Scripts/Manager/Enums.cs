using UnityEngine;

public class Enums : MonoBehaviour{
    public enum ItemType{
        Weapon,
        Equipment,
        Consumable,
        Ammo,
        Chest,
        None,
    }

    public enum WeaponType{
        Unarmed,
        Sword,
        Shield,
        Bow,
    }

    public enum EquipType{
        Hotbar,
        RightHand,
        LeftHand,
        Hand,
        Helmet,
        Armor,
        Boots,
        None,
        Legs,
        Gloves,
        Cape
    }

    public enum QuestStatus{
        Unavailable,
        Available,
        Accepted,
        Completed
    }

    public enum QuestProgress{
        Inactive,
        Active,
        Complete,
    }

    public enum GoalType{
        Default,
        Kill,
        Fetch, // Thu thập
        Contact, // Liên lạc
        Explore, // Khám phá
        Escort // Hộ tống
    }

    public enum TeleportDestination{
        Ivorystars,
        Moongarden,
        Dungeon
    }

    public enum AttackType{
        Light,
        Heavy
    }
}