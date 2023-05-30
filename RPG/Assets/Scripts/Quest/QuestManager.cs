using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [Header("UI")]
    public Transform questContainer;
    public GameObject questPrefab;

    [Header("Quest")]
    public List<Quest> currentQuests;
    public List<GameObject> questObjects; 
    void UnlockedNextQuests(Quest pre){
        if(pre.questName == "Rescue Moongarden" || pre.questName == "Scout Monsters" || pre.questName == "Conquer Dungeon"){
            EventManager.QuestReceived(pre);
        }
    }

    
    public void TakeQuest(Quest quest){
        currentQuests.Add(quest); 

        UnlockedNextQuests(quest); 

        GameObject questObject = Instantiate(questPrefab.gameObject, questContainer);
        questObjects.Add(questObject);

        TMP_Text[] texts = questObject.GetComponentsInChildren<TMP_Text>();

        texts[0].text = $"- {quest.questName}";
        
        for (int i = 1; i < texts.Length; i++){
            if(i <= quest.goals.Length){
                texts[i].text = $"+ {quest.goals[i-1].description} {quest.goals[i-1].currentAmount}/{quest.goals[i-1].requiredAmount}";
            }else{
                texts[i].text = "";
            }
        }

        
    }

    public void UpdateQuest(Quest quest, Goal goal, bool questCompleted = false){
        for (int i = 0; i < currentQuests.Count; i++){
            if(currentQuests[i].questName == quest.questName){
                foreach (TMP_Text _goal in questObjects[i].GetComponentsInChildren<TMP_Text>()){
                    if(_goal.text.Contains(goal.description)){
                        _goal.text = $"+ {goal.description} {goal.currentAmount}/{goal.requiredAmount}";
                        
                        if(goal.currentAmount >= goal.requiredAmount){
                            _goal.fontStyle = (FontStyles)FontStyle.Italic;
                            _goal.color = Color.green;
                        }

                        break;
                    }
                }
                if(questCompleted){
                    TMP_Text title = questObjects[i].GetComponentInChildren<TMP_Text>();
                    title.fontStyle = (FontStyles)FontStyle.Italic;
                    title.color = Color.green;
                }

                return;
            }
        }
    }

    public void CompleteQuest(Quest quest){
        int index = -1;

        for (int i = 0; i < currentQuests.Count; i++){
            if(currentQuests[i].questName == quest.questName){
                index = i;

                break;
            }
        }

        if(index != -1){
            Destroy(questObjects[index]);
            questObjects.RemoveAt(index);
            currentQuests.RemoveAt(index);
        }
    }
}
