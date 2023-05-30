using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Enums;


public class QuestGiver : NPC
{
    [Header("Refs")]
    public PlayerStats playerStats;
    public InventoryManager inventoryManager;
    public QuestManager questManager;

    [Header("Quest Info")]
    public QuestStatus questStatus; 
    public int currentQuest = -1; 
    public Quest preReceivedQuest; 
    public bool questLocked; 
    public Quest[] quests;

    [Header("Quest Marker")] 
    public Image questMarker;
    public Sprite unavailable;
    public Sprite available;
    public Sprite accepted;
    public Sprite completed;

    protected override void Awake() {
        base.Awake();

        currentQuest = -1;
        questStatus = QuestStatus.Unavailable;
        questLocked = preReceivedQuest != null;
        
        foreach (Quest quest in quests){
            quest.ResetToDefault();
        }

        
        EventManager.OnQuestReceived -= QuestReceived;
        EventManager.OnQuestReceived += QuestReceived; 

        CheckQuest();
    }


    void QuestReceived(Quest pre){
        if(preReceivedQuest == pre){
            questLocked = false;

            EventManager.OnQuestReceived -= QuestReceived;

            Debug.Log("Mở khóa danh sách nhiệm vụ của " + npcName);

            CheckQuest(); 
        }
    }

    public void CheckQuest(){
    
        if(questLocked){
            UpdateMarker();
            return;
        } 

        currentQuest = -1;
        questStatus = QuestStatus.Unavailable;

        for(int i = 0; i < quests.Length; i++){
            if(quests[i].questProgress == QuestProgress.Inactive){
                currentQuest = i;
                questStatus = QuestStatus.Available;

                break;
            }
        }

        UpdateMarker(); 
    } 

    public void UpdateMarker(){
        if(questMarker == null) return;

        if(questStatus == QuestStatus.Available){
            questMarker.sprite = available;
        }
        else if(questStatus == QuestStatus.Accepted){
            questMarker.sprite = accepted;
        }
        else if(questStatus == QuestStatus.Completed){
            questMarker.sprite = completed;
        }else{
            questMarker.sprite = unavailable;
        }
    }

    public override void Interact(PlayerInteract playerInteract){
        base.Interact(playerInteract);

        EventManager.NPCInteraction(this);

        questManager = playerInteract.GetComponent<QuestManager>();
        playerStats = playerInteract.GetComponent<PlayerStats>();
        inventoryManager = playerInteract.GetComponent<InventoryManager>();

        if(questStatus == QuestStatus.Unavailable){
            InitializeDialogue(baseDialogues);
        }
        
        else if(questStatus == QuestStatus.Available){
            InitializeDialogue(quests[currentQuest].availableDialogues, () => AssignQuest());
        }
      
        else if(questStatus == QuestStatus.Accepted){
            InitializeDialogue(quests[currentQuest].acceptedDialogues);
        }
        
        else if(questStatus == QuestStatus.Completed){
       
            InitializeDialogue(quests[currentQuest].completedDialogues, () => GiveRewards());
        }
    }

   
    void AssignQuest(){
        questStatus = QuestStatus.Accepted;
        UpdateMarker();

        quests[currentQuest].questProgress = QuestProgress.Active;
        quests[currentQuest].InitializeQuest(this);

        questManager.TakeQuest(quests[currentQuest]);

        Debug.Log("Assigned Quest");

        if(npcName == "Tyeis" && quests[currentQuest].questName == "Escort Tyeis"){
            GetComponent<EscortManager>().StartEscort(playerManager);

            questMarker.sprite = unavailable;
        }
    }

    
    void GiveRewards(){
        questStatus = QuestStatus.Unavailable;

        quests[currentQuest].questProgress = QuestProgress.Complete;
        CheckQuestCompleted();

        questManager.CompleteQuest(quests[currentQuest]);
        if(quests[currentQuest].expReward != 0){
            playerStats.AddEXP(quests[currentQuest].expReward);
        }

        if(quests[currentQuest].goldReward != 0){
            playerStats.AddCoins(quests[currentQuest].goldReward);
        }

        if(quests[currentQuest].itemsReward.Length != 0){
            for (int i = 0; i < quests[currentQuest].itemsReward.Length; i++){
                inventoryManager.AddItem(quests[currentQuest].itemsReward[i].item, quests[currentQuest].itemsReward[i].stackSize);
            }
        }

        if(npcName == "Dawkins" && quests[currentQuest].questName == "Conquer Dungeon"){
            StartCoroutine(Win()); 
        }

       
        List<Quest> _list = quests.ToList();
        _list.RemoveAt(currentQuest);
        quests = _list.ToArray();

        CheckQuest(); 
    }

    IEnumerator Win(){
        yield return new WaitForSeconds(1f);

        playerStats.GetComponentInChildren<WinScreenManager>().ShowWinScreen(playerStats.playerManager.playerInput);
    }

   
    void CheckQuestCompleted(){
    
        if(npcName == "Tyeis" && quests[currentQuest].questName == "Escort Tyeis"){
            GetComponent<EscortManager>().MoveToStandPoint();

            for (int i = 0; i < questManager.currentQuests.Count; i++){
                if(questManager.currentQuests[i].name == "Scout Monsters"){
                    questManager.currentQuests[i].goals[0].currentAmount += 1;
                    questManager.currentQuests[i].goals[0].Evaluate();
                }
            }
        }
    
        else if(npcName == "Dennis" && quests[currentQuest].questName == "Clean Monsters"){
            for (int i = 0; i < questManager.currentQuests.Count; i++){
                if(questManager.currentQuests[i].name == "Rescue Moongarden"){
                    questManager.currentQuests[i].goals[1].currentAmount += 1;
                    questManager.currentQuests[i].goals[1].Evaluate();
                }
            }
        }
      
        else if(npcName == "Keegan" && quests[currentQuest].questName == "Explore Dungeon"){
            for (int i = 0; i < questManager.currentQuests.Count; i++){
                if(questManager.currentQuests[i].name == "Conquer Dungeon"){
                    questManager.currentQuests[i].goals[1].currentAmount += 1;
                    questManager.currentQuests[i].goals[1].Evaluate();
                }
            }
        }
        
    }
}
