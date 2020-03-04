using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJournal : MonoBehaviour
{

    private const string progressFormat = "{0}/{1}";

    [SerializeField]
    private GameObject playerJournal = null;
    // Start is called before the first frame update

    public List<Quest> PlayerQuests = null;

    [SerializeField]
    private GameObject panel = null;

    [SerializeField]
    private GameObject questItemPrefab = null;

    #region Singleton
    public static PlayerJournal instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        instance = this;
        PlayerQuests = new List<Quest>();

        playerJournal.SetActive(false);

        controllers = new Dictionary<QuestType, QuestItemController>()
        {
            { QuestType.Combat, null },
            { QuestType.Fetch, null },
        };
    }
    #endregion

    private Dictionary<QuestType, QuestItemController> controllers;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Journal"))
        {
            playerJournal.SetActive(!playerJournal.activeSelf);
        }
        
    }

    public void Add(Quest quest)
    {
        PlayerQuests.Add(quest);
        //Debug.Log(PlayerQuests[0].Title);
        GameObject newQuest = Instantiate(questItemPrefab) as GameObject;
        QuestItemController controller = newQuest.GetComponent<QuestItemController>();
        controllers[quest.QuestType] = newQuest.GetComponent<QuestItemController>(); ;
        controller.Title.text = quest.Title;
        controller.Description.text = quest.Description;
        controller.ProgressText.text = quest.ProgressText;
        newQuest.transform.parent = panel.transform;
        newQuest.transform.localScale = Vector3.one;
    }
    public void OnActionCompleted(QuestType questType)
    {
        foreach(Quest quest in PlayerQuests)
        {
            if(quest.QuestType == questType && quest.OnActionCompleted())
            {
                Debug.Log("Action Completed");
            }

            if (controllers[questType] && questType == quest.QuestType)
            {
                controllers[questType].ProgressText.SetText(string.Format(
                    progressFormat,
                    quest.ActionsCompleted,
                    quest.ActionsNeeded));

            }

        }

    }

}
