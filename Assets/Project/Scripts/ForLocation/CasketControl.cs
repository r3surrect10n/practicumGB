using UnityEngine;

public class CasketControl : MonoBehaviour
{
    [SerializeField] private HingeJoint casketLid;
    [SerializeField] private GameObject note;
    [SerializeField] private string nameMiniGame;
    [SerializeField] private float speedUp = 3f;

    private bool isNoteMove = false;
    private float maxDeltaY = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuestStatus statusGame = GameManager.Instance.currentPlayer.listMiniGames.GetMiniGamesStatus(nameMiniGame);
        if (statusGame == QuestStatus.isSuccess)
        {
            isNoteMove = true;
            casketLid.useMotor = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isNoteMove)
        {
            if (note.transform.position.y < maxDeltaY)
            {
                Vector3 pos = note.transform.position;
                pos.y += Time.deltaTime * speedUp;
                note.transform.position = pos;
            }
            else isNoteMove = false;
        }
    }
}
