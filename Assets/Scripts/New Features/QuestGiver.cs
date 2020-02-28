using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;
using System.Linq;

public class QuestGiver : Interactable
{
    private const string progressFormat = "{0}/{1}";

    [SerializeField]
    private Canvas canvas = null;
    //each quest giver needs a list of quests
    //need to open a panel that loads those quests

    [Header("UI References")]
    [SerializeField]
    private TextMeshProUGUI titleText = null;
    [SerializeField]
    private TextMeshProUGUI descriptionText = null;
    [SerializeField]
    private Button acceptButton = null;
    [SerializeField]
    private Button switchButton = null;
    [SerializeField]
    private TextMeshProUGUI progressText = null;

    private List<Quest> quests = null;

    private Quest currentQuest = null;

    private int indexCurrentQuest = 0;

    public override void Interact()
    {
        base.Interact();
        canvas.enabled = true;
        acceptButton.gameObject.SetActive(true);

        titleText.SetText(quests[indexCurrentQuest].Title);
        descriptionText.SetText(quests[indexCurrentQuest].Description);
    }

    private void Awake()
    {
        Assert.IsNotNull(canvas);
        canvas.enabled = false;

        quests = new List<Quest>();

        quests.Add(new Quest(
                title: "Kill a Skeleton",
                description: "Go to a bridge and kill a skeleton",
                progressTest: "0/1",
                questType: QuestType.Combat,
                actionsNeeded: 1));

        quests.Add(new Quest(
          title: "Pick up Equipment",
          description: "Pick up Helmet, Plate Armor, Plate Leg, Sword, and Shield",
          progressTest: "0/5",
          questType: QuestType.Fetch,
          actionsNeeded: 5));


        acceptButton.onClick.AddListener(() => OnAccept());
        switchButton.onClick.AddListener(() => OnSwitch());

        progressText.SetText("");

    }

    private void OnAccept()
    {
        currentQuest = quests[indexCurrentQuest];

        currentQuest.Accepted = true;

        PlayerJournal.instance.Add(currentQuest);

        //progressText.SetText(string.Format(
        //    progressFormat,
        //    currentQuest.ActionsCompleted,
        //    currentQuest.ActionsNeeded));

        //acceptButton.gameObject.SetActive(false);
        //switchButton.gameObject.SetActive(false);

        //remove quest
        quests.RemoveAt(indexCurrentQuest);
        indexCurrentQuest = 0;

        if(quests.Count == 0)
        {
            canvas.enabled = false;
        }
        else
        {
            OnSwitch();
        }

    }

    private void OnSwitch()
    {
        indexCurrentQuest = (indexCurrentQuest + 1) % quests.Count();
        currentQuest = quests[indexCurrentQuest];
        titleText.SetText(quests[indexCurrentQuest].Title);
        descriptionText.SetText(quests[indexCurrentQuest].Description);

    }
 }
