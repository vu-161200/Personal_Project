using System.Collections.Generic;
using UnityEngine;

// Thay đổi giày của nhân vật
public class BootsModelChanger : MonoBehaviour
{
    public List<GameObject> bootsModels;

    private void Awake() {
        GetAllModels();
    }

    // Lấy danh sách model trong children
    void GetAllModels(){
        for (int i = 0; i < transform.childCount; i++){
            bootsModels.Add(transform.GetChild(i).gameObject);
        }
    }

    // Tắt tất cả giày trong ds giày hiện tại
    public void UnEquipAllModels(){
        foreach (GameObject bootsModel in bootsModels){
            bootsModel.SetActive(false);
        }
    } 

    // Bật (Hiển thị) giày có tên modelName
    public void EquipModelByName(string modelName){
        if(modelName == ""){
            bootsModels[0].SetActive(true);
        }else{
            for (int i = 0; i < bootsModels.Count; i++){
                if(bootsModels[i].name == modelName){
                    bootsModels[i].SetActive(true);
                }
            }
        }
    }
}
