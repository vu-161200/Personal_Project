using System.Collections.Generic;
using UnityEngine;

public class CapeModelChanger : MonoBehaviour
{
    public List<GameObject> capeModels;

    private void Awake() {
        GetAllModels();
    }

    void GetAllModels(){
        for (int i = 0; i < transform.childCount; i++){
            capeModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnEquipAllModels(){
        foreach (GameObject capeModel in capeModels){
            capeModel.SetActive(false);
        }
    } 

    public void EquipModelByName(string modelName){
        if(modelName == ""){
            capeModels[0].SetActive(true);
        }else{
            for (int i = 0; i < capeModels.Count; i++){
                if(capeModels[i].name == modelName){
                    capeModels[i].SetActive(true);
                }
            }
        }
    }
}
