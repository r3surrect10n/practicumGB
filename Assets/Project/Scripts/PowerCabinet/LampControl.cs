using UnityEngine;

public class LampControl : MonoBehaviour
{
    [SerializeField] private InterQuestObject powerCabinet;
    [SerializeField] private GameObject lightLamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuestStatus statusGame = GameManager.Instance.currentPlayer.listMiniGames.GetMiniGamesStatus("PowerCabinetScene");
        if (statusGame == QuestStatus.isSuccess)
        {
            lightLamp.SetActive(true);
            if (powerCabinet != null)
            {
                powerCabinet.IsGameItem = false;
                powerCabinet.BtnOkText = "";
                powerCabinet.HintText = "Исправный силовой щиток";
            }
        }
        else
        {
            lightLamp.SetActive(false);
        }
    }
}
