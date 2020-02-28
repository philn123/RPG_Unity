using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //[SerializeField]
    //private QuestGiver questGiver = null;

    [SerializeField]
    private PlayerJournal playerJournal = null;

    public void OnEnemyDeath() =>
        playerJournal.OnActionCompleted(QuestType.Combat);


    public void OnEquipmentPickup() =>
        playerJournal.OnActionCompleted(QuestType.Fetch);
}
