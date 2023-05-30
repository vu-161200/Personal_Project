using UnityEngine;
using static Enums;

[System.Serializable] 
public class Goal{
    public GoalType goalType; 
    public bool completed;
    public string ID; 
    public string description; 
    public int currentAmount; 
    public int requiredAmount; 

    private Quest parent; 

    void OnValidate() {
        if(currentAmount < 0) currentAmount = 0;

        if(requiredAmount < currentAmount) requiredAmount = currentAmount + 1;
    }

    public void InitializeGoal(Quest quest){
        parent = quest;

        InitializeEvent();
    }

    void InitializeEvent(){
        if(goalType == GoalType.Fetch){
            EventManager.OnItemFetched += ItemFetched;
        }else if(goalType == GoalType.Kill){
            EventManager.OnEnemyDeath += EnemyDied;
        }else if(goalType == GoalType.Contact){
            EventManager.OnNPCInteraction += NPCInteracted;
        }else if(goalType == GoalType.Escort){
            EventManager.OnEscortCompleted += EscortCompleted;
        }else{
            return;
        }
    }

    void DeleteEvent(){
        if(goalType == GoalType.Fetch){
            EventManager.OnItemFetched -= ItemFetched;
        }else if(goalType == GoalType.Kill){
            EventManager.OnEnemyDeath -= EnemyDied;
        }else if(goalType == GoalType.Contact){
            EventManager.OnNPCInteraction -= NPCInteracted;
        }else if(goalType == GoalType.Escort){
            EventManager.OnEscortCompleted -= EscortCompleted;
        }else{
            return;
        }
    }

    public void Evaluate(){
        if(currentAmount >= requiredAmount){
            completed = true;
            parent.CheckProgress(this); 
            DeleteEvent();
        }

        parent.UpdateGoalStatus(this);
    }

    #region Events
    void EnemyDied(EnemyStats enemy){
        if (enemy.ID == this.ID)
        {
            Debug.Log("Detected enemy death: " + ID);
            this.currentAmount++;
            Debug.Log("Progress " + currentAmount + "/" + requiredAmount);
            Evaluate();
        }
    }

    void ItemFetched(Item item, int stack){
        if (item.ID == this.ID)
        {
            Debug.Log("Detected quest item: " + item.itemName);
            this.currentAmount += stack;
            Debug.Log("Progress " + currentAmount + "/" + requiredAmount);
            Evaluate();
        }
    }

    void NPCInteracted(NPC npc){
        if(npc.npcName == this.ID){
            Debug.Log("Detected target: " + this.ID);
            this.currentAmount += 1;
            Debug.Log("Progress " + currentAmount + "/" + requiredAmount);
            Evaluate();
        }
    }
    
    void EscortCompleted(NPC npc){
        if(npc.npcName == this.ID){
            this.currentAmount += 1;
            Evaluate();
        }
    }

    #endregion
}