using System.Collections.Generic;
using UnityEngine;

public class LegsModelChanger : MonoBehaviour
{
    public List<GameObject> legsModels;

    private void Awake() {
        GetAllModels();
    }

    void GetAllModels(){
        for (int i = 0; i < transform.childCount; i++){
            legsModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnEquipAllModels(){
        foreach (GameObject legsModel in legsModels){
            legsModel.SetActive(false);
        }
    } 

    public void EquipModelByName(string modelName){
        if(modelName == ""){
            legsModels[0].SetActive(true);
        }else{
            for (int i = 0; i < legsModels.Count; i++){
                if(legsModels[i].name == modelName){
                    legsModels[i].SetActive(true);
                }
            }
        }
    }
}
