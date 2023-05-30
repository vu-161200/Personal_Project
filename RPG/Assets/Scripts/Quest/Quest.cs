using System.Linq;
using UnityEngine;
using static Enums;


[CreateAssetMenu(fileName = "QuestData", menuName = "Quests")] 
public class Quest : ScriptableObject
{
    [Header("Available Quest Dialogue")]
    public string[] availableDialogues; 

    [Header("Accepted Quest Dialogue")]
    public string[] acceptedDialogues; /

    [Header("Completed Quest Dialogue")]
    public string[] completedDialogues; 

    [Header("Quest Info")]
    public string questName; 
    public QuestProgress questProgress; 
    public Goal[] goals; 

    [Header("Quest Reward")]
    public int expReward;
    public int goldReward;
    public ChestItem[] itemsReward;

    QuestGiver parent; 

    void OnValidate() {
        foreach(ChestItem item in itemsReward) {
            if(item.item != null) {
                if(item.stackSize > item.item.maxStack){
                    item.stackSize = item.item.maxStack;
                }

                if(item.stackSize < 1){
                    item.stackSize = 1;
                }

                if(expReward < 0){
                    expReward = 0;
                }

                if(goldReward < 0){
                    goldReward = 0;
                }
            }
        }
    }

 
    public void InitializeQuest(QuestGiver questGiver) {
        parent = questGiver;

        InitializeGoals();
    }


    public void CheckProgress(Goal goal) {
        if(goals.All(goal => goal.completed)){
            questProgress = QuestProgress.Complete; 
            parent.questStatus = QuestStatus.Completed; 
            parent.UpdateMarker(); 
            parent.questManager.UpdateQuest(this, goal, true); 
        }
    }

    public void UpdateGoalStatus(Goal goal){
        parent.questManager.UpdateQuest(this, goal);
    }

    void InitializeGoals(){
        for (int i = 0; i < goals.Length; i++) {
            goals[i].InitializeGoal(this);
        }
    }

    
    public void ResetToDefault(){
        questProgress = QuestProgress.Inactive;

        for (int i = 0; i < goals.Length; i++){
            goals[i].completed = false;
            goals[i].currentAmount = 0;
        }
    }

}

// NAME
// Male: Kahn, Quint, O’brouwer, D’romain, Lemanneville, Nathraichean, Vann
// Female: Keegan, Mcbostrom, Lavail, Dennis, Leroy, Tyeis