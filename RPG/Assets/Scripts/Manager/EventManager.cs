using UnityEngine;

// Quản lý các sự kiện trong game
public class EventManager : MonoBehaviour
{
    // Sự kiện giết quái vật
    public delegate void EnemyEventHandler(EnemyStats enemy);
    public static event EnemyEventHandler OnEnemyDeath;

    public static void EnemyDied(EnemyStats enemy)
    {
        if (OnEnemyDeath != null)
            OnEnemyDeath(enemy);
    }

    // Sự kiện thu thập vật phẩm
    public delegate void ItemEventHandler(Item item, int stack);
    public static event ItemEventHandler OnItemFetched;

    public static void ItemFetched(Item item, int stack)
    {
        if (OnItemFetched != null)
            OnItemFetched(item, stack);
    }

    // Sự kiện trò chuyện với NPC
    public delegate void NPCEventHandler(QuestGiver npc);
    public static event NPCEventHandler OnNPCInteraction;

    public static void NPCInteraction(QuestGiver npc){
        if(OnNPCInteraction != null)
            OnNPCInteraction(npc);
    }

    // Sự kiện mở khóa nhiệm vụ tiếp theo khi nhận nhiệm vụ
    public delegate void QuestEventHandler(Quest preQuest);
    public static event QuestEventHandler OnQuestReceived;

    public static void QuestReceived(Quest preQuest){
        if(OnQuestReceived != null)
            OnQuestReceived(preQuest);
    }

    // Sự kiện khi bắt đầu vào đánh BOSS
    public delegate void BossFightEventHandler(BossFightCollider collider, CharacterStats playerStats);
    public static event BossFightEventHandler OnBossFight;

    public static void BossFight(BossFightCollider collider, CharacterStats playerStats){
        if(OnBossFight != null) OnBossFight(collider, playerStats);
    }

    // Sự kiện khi bắt đầu vào đánh BOSS nhỏ
    public delegate void SmallBossFightEventHandler(BossFightCollider collider, CharacterStats playerStats);
    public static event SmallBossFightEventHandler OnSmallBossFight;

    public static void SmallBossFight(BossFightCollider collider, CharacterStats playerStats){
        if(OnSmallBossFight != null) OnSmallBossFight(collider, playerStats);
    }

    // Sự kiện khi hoàn thành hộ tống
    public delegate void EscortCompletedEventHandler(NPC npc);
    public static event EscortCompletedEventHandler OnEscortCompleted;

    public static void EscortCompleted(NPC npc){
        if(OnEscortCompleted != null) OnEscortCompleted(npc);
    }

    
}
