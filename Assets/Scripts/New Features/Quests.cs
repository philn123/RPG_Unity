using UnityEngine;
using UnityEngine.Assertions;

public enum QuestType
{
    None = 0,
    Combat,
    Fetch
}

public class Quest
{
    public string Title { get; }
    public string Description { get; }
    public bool Completed { get; }
    public bool Accepted { get; set; }
    public QuestType QuestType { get; }
    public string ProgressText { get; }

    public int ActionsCompleted { get; private set; }

    public int ActionsNeeded { get; private set; }

    public Quest(string title, string description, string progressTest, QuestType questType, int actionsNeeded)
    {
        Assert.IsTrue(questType != QuestType.None);

        Title = title;
        Description = description;
        ProgressText = progressTest;
        Completed = false;
        Accepted = false;
        QuestType = questType;
        ActionsNeeded = actionsNeeded;

        ActionsCompleted = 0;
    }

    public bool OnActionCompleted()
    {
        ActionsCompleted++;

        return (ActionsCompleted >= ActionsNeeded);
    }
}