using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item",fileName ="NewItem")]
public class ItemSO : ScriptableObject

{
   [SerializeField] string itemName = "Enter Object Name";
   [TextArea(2,6)]
   [SerializeField] string itemDescription = "Enter Scrolling Text";
   [SerializeField] bool isInteractable = true;



   public string GetDescription(){
    return itemDescription;
   }

   public string GetName(){
    return  itemName;
   }

   public bool GetIsInteractable(){
    return isInteractable;
   }
}
