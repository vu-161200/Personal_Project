using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetModelChanger : MonoBehaviour
{
    public List<GameObject> helmetModels;

    private void Awake() {
        GetAllModels();
    }

    void GetAllModels(){
        for (int i = 0; i < transform.childCount; i++){
            helmetModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnEquipAllModels(){
        foreach (GameObject helmetModel in helmetModels){
            helmetModel.SetActive(false);
        }
    } 

    public void EquipModelByName(string modelName){
        if(modelName == ""){
            helmetModels[0].SetActive(true);
        }else{
            for (int i = 0; i < helmetModels.Count; i++){
                if(helmetModels[i].name == modelName){
                    helmetModels[i].SetActive(true);
                }
            }
        }
    }
}
