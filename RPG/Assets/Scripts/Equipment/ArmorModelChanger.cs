using System.Collections.Generic;
using UnityEngine;

// Thay đổi giáp hiện tại
public class ArmorModelChanger : MonoBehaviour
{
    public List<GameObject> armorModels;

    private void Awake() {
        GetAllModels();
    }

    // Lấy ds tất cả giáp ở children
    void GetAllModels(){
        for (int i = 0; i < transform.childCount; i++){
            armorModels.Add(transform.GetChild(i).gameObject);
        }
    }

    // Tắt tất cả giáp trong ds giáp
    public void UnEquipAllModels(){
        foreach (GameObject armorModel in armorModels){
            armorModel.SetActive(false);
        }
    } 

    // Bật (Hiển thị) giáp có tên modelName
    public void EquipModelByName(string modelName){
        if(modelName == ""){
            armorModels[0].SetActive(true);
        }else{
            for (int i = 0; i < armorModels.Count; i++){
                if(armorModels[i].name == modelName){
                    armorModels[i].SetActive(true);
                }
            }
        }
    }
}
