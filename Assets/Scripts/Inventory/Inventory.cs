﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    //Singleton Pattern
    #region Singleton
    public static Inventory instance;

    void Awake(){
        if(instance != null){
            Debug.LogWarning("More than one instance");
            return;
        }
        instance = this;
    }
    #endregion 

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    [SerializeField]
    private EventManager eventManager = null;


    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add (Item item){
        if(!item.isDefaultItem){

            if(items.Count >= space){
                Debug.Log("Not enough room");
                return false;
            }

            items.Add(item);
            if(onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
            eventManager.OnEquipmentPickup();
        }
        return true;
    }
    public void Remove(Item item){
        items.Remove(item);
        if(onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
