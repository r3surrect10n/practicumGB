using UnityEngine;

public class QuestSequence : MonoBehaviour
{
    [SerializeField] private QuestObject[] quests;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (quests.Length > 0)
        {
            quests[0].ChangeStatus(QuestStatus.isAccessible);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
