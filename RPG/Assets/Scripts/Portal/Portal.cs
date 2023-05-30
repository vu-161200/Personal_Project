using UnityEngine;
using static Enums;

// Dịch chuyển
public class Portal : Interactable
{
    [Header("Settings")]
    public TeleportDestination destination;

    public override void Interact(PlayerInteract playerInteract){
        base.Interact(playerInteract);

        LoadMap();  
    }

    void LoadMap(){
        switch (destination)
        {
            case TeleportDestination.Ivorystars:
                SceneController.instance.LoadScene(1);
                break;
            case TeleportDestination.Dungeon:
                SceneController.instance.LoadScene(3);    
                break;
            case TeleportDestination.Moongarden:
                SceneController.instance.LoadScene(2);
                break;
            default:
                Debug.Log("Something went wrong!");
                break;
        }
    }

}
