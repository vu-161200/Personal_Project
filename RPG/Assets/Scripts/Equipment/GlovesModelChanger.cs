using System.Collections.Generic;
using UnityEngine;

public class GlovesModelChanger : MonoBehaviour
{
    public List<GameObject> glovesModels;

    private void Awake() {
        GetAllModels();
    }

    void GetAllModels(){
        for (int i = 0; i < transform.childCount; i++){
            glovesModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnEquipAllModels(){
        foreach (GameObject glovesModel in glovesModels){
            glovesModel.SetActive(false);
        }
    } 

    public void EquipModelByName(string modelName){
        if(modelName == ""){
            glovesModels[0].SetActive(true);
        }else{
            for (int i = 0; i < glovesModels.Count; i++){
                if(glovesModels[i].name == modelName){
                    glovesModels[i].SetActive(true);
                }
            }
        }
    }
}
